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

    public partial class DespawnExtractorSubBuildingsSystem : GameSystemBase
    {
        #region "Members"

        private EndFrameBarrier m_EndFrameBarrier;

        private EntityQuery m_ExtractorFacilityQuery;
        #endregion

        #region "Job"

#if BURST
        [BurstCompile]
#endif
        private struct DespawnExtractorFacilitiesJob : IJobChunk
        {
            [ReadOnly]
            public EntityTypeHandle m_EntityType;

            [ReadOnly]
            public ComponentTypeHandle<Owner> m_OwnerType;

            [ReadOnly]
            public ComponentLookup<PrefabRef> m_PrefabRefData;

            [ReadOnly]
            public ComponentLookup<ExtractorAreaData> m_ExtractorAreaData;

            public EntityCommandBuffer.ParallelWriter m_CommandBuffer;

            public bool m_AllowFarmBuildings, m_AllowForestBuildings, m_AllowOilBuildings, m_AllowOreBuildings, m_AllowFishingBuildings;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                var entities = chunk.GetNativeArray(m_EntityType);
                var owners = chunk.GetNativeArray(ref m_OwnerType);

                for (int i = 0; i < entities.Length; ++i)
                {
                    var entity = entities[i];
                    var owner = owners[i];

                    if (owner.m_Owner != Entity.Null && m_PrefabRefData.TryGetComponent(owner.m_Owner, out var prefabRef))
                    {
                        if (m_ExtractorAreaData.TryGetComponent(prefabRef.m_Prefab, out var areaData))
                        {
                            switch(areaData.m_MapFeature)
                            {
                                case MapFeature.FertileLand:
                                    if (!m_AllowFarmBuildings)
                                        break;
                                    else continue;
                                case MapFeature.Forest:
                                    if (!m_AllowForestBuildings)
                                        break;
                                    else continue;
                                case MapFeature.Oil:
                                    if (!m_AllowOilBuildings)
                                        break;
                                    else continue;
                                case MapFeature.Ore:
                                    if (!m_AllowOreBuildings)
                                        break;
                                    else continue;
                                case MapFeature.Fish:
                                    if (!m_AllowFishingBuildings)
                                        break;
                                    else continue;
                                default:
                                    continue;
                            }

                            m_CommandBuffer.AddComponent(unfilteredChunkIndex, entity, default(Deleted));
                        }
                    }
                }
            }
        }
        #endregion

        #region "System"

        protected override void OnCreate()
        {
            base.OnCreate();

            // This system is only explicitly enabled for one frame from the settings menu.
            Enabled = false;

            m_EndFrameBarrier = base.World.GetOrCreateSystemManaged<EndFrameBarrier>();
            m_ExtractorFacilityQuery = GetEntityQuery(ComponentType.ReadOnly<Game.Buildings.ExtractorFacility>(), ComponentType.ReadOnly<Owner>(), ComponentType.Exclude<Deleted>(), ComponentType.Exclude<Destroyed>(), ComponentType.Exclude<Temp>());
            
            RequireForUpdate(m_ExtractorFacilityQuery);
        }

        protected override void OnUpdate()
        {
            var job = new DespawnExtractorFacilitiesJob
            {
                m_EntityType = SystemAPI.GetEntityTypeHandle(),
                m_OwnerType = SystemAPI.GetComponentTypeHandle<Owner>(true),
                m_PrefabRefData = SystemAPI.GetComponentLookup<PrefabRef>(true),
                m_ExtractorAreaData = SystemAPI.GetComponentLookup<ExtractorAreaData>(true),
                m_CommandBuffer = m_EndFrameBarrier.CreateCommandBuffer().AsParallelWriter(),
                m_AllowFarmBuildings = ExtractorsBegone.Instance.Settings.FarmExtractorsSpawnFactor > 0.01f,
                m_AllowForestBuildings = ExtractorsBegone.Instance.Settings.ForestExtractorsSpawnFactor > 0.01f,
                m_AllowOreBuildings = ExtractorsBegone.Instance.Settings.OreExtractorsSpawnFactor > 0.01f,
                m_AllowOilBuildings = ExtractorsBegone.Instance.Settings.OilExtractorsSpawnFactor > 0.01f,
                m_AllowFishingBuildings = ExtractorsBegone.Instance.Settings.FishExtractorsSpawnFactor > 0.01f
            };
            
            var dependency = job.ScheduleParallel(m_ExtractorFacilityQuery, Dependency);
            m_EndFrameBarrier.AddJobHandleForProducer(dependency);
            Dependency = dependency;

            // Disable the system again.
            Enabled = false;
        }
        #endregion
    }
}
