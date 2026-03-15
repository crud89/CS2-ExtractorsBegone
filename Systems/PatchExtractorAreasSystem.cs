using Game;
using Game.Areas;
using Game.Common;
using Game.Prefabs;
using Game.Tools;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace crud89.ExtractorsBegone.Systems
{
    using crud89.ExtractorsBegone;

    public partial class PatchExtractorAreasSystem : GameSystemBase
    {
        #region "Members"

        private EndFrameBarrier m_EndFrameBarrier;

        private EntityQuery m_ExtractorAreaPrefabsQuery;
        #endregion

        #region "Job"

#if BURST
        [BurstCompile]
#endif
        private struct ResetExtractorAreaSpawnFactorJob : IJobChunk
        {
            [ReadOnly]
            public EntityTypeHandle m_EntityType;

            public ComponentTypeHandle<ExtractorAreaData> m_ExtractorAreaData;

            public float m_FarmSpawnFactor, m_ForestSpawnFactor, m_OreSpawnFactor, m_OilSpawnFactor, m_FishSpawnFactor;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var entities = chunk.GetNativeArray(m_EntityType);
                var extractorAreas = chunk.GetNativeArray(ref m_ExtractorAreaData);

                for (int i = 0; i < entities.Length; ++i)
                {
                    var entity = entities[i];
                    var extractorArea = extractorAreas[i];

                    switch (extractorArea.m_MapFeature)
                    {
                        case MapFeature.FertileLand:
                            extractorArea.m_ObjectSpawnFactor = m_FarmSpawnFactor;
                            break;
                        case MapFeature.Forest:
                            extractorArea.m_ObjectSpawnFactor = m_ForestSpawnFactor;
                            break;
                        case MapFeature.Oil:
                            extractorArea.m_ObjectSpawnFactor = m_OilSpawnFactor;
                            break;
                        case MapFeature.Ore:
                            extractorArea.m_ObjectSpawnFactor = m_OreSpawnFactor;
                            break;
                        case MapFeature.Fish:
                            extractorArea.m_ObjectSpawnFactor = m_FishSpawnFactor;
                            break;
                        default:
                            continue;
                    }

                    // Reset object spawn factor.
                    extractorAreas[i] = extractorArea;
                }
            }
        }
        #endregion

        #region "System"

        protected override void OnCreate()
        {
            base.OnCreate();

            m_EndFrameBarrier = base.World.GetOrCreateSystemManaged<EndFrameBarrier>();
            m_ExtractorAreaPrefabsQuery = GetEntityQuery(ComponentType.ReadWrite<ExtractorAreaData>(), ComponentType.Exclude<Deleted>(), ComponentType.Exclude<Destroyed>(), ComponentType.Exclude<Temp>());
            
            RequireForUpdate(m_ExtractorAreaPrefabsQuery);
        }

        protected override void OnUpdate()
        {
            var job = new ResetExtractorAreaSpawnFactorJob
            {
                m_EntityType = SystemAPI.GetEntityTypeHandle(),
                m_ExtractorAreaData = SystemAPI.GetComponentTypeHandle<ExtractorAreaData>(false),
                m_FarmSpawnFactor = ExtractorsBegone.Instance.Settings.FarmExtractorsSpawnFactor,
                m_ForestSpawnFactor = ExtractorsBegone.Instance.Settings.ForestExtractorsSpawnFactor,
                m_OreSpawnFactor = ExtractorsBegone.Instance.Settings.OreExtractorsSpawnFactor,
                m_OilSpawnFactor = ExtractorsBegone.Instance.Settings.OilExtractorsSpawnFactor,
                m_FishSpawnFactor = ExtractorsBegone.Instance.Settings.FishExtractorsSpawnFactor
            };
            
            var dependency = job.ScheduleParallel(m_ExtractorAreaPrefabsQuery, Dependency);
            m_EndFrameBarrier.AddJobHandleForProducer(dependency);
            Dependency = dependency;
        }
        #endregion
    }
}
