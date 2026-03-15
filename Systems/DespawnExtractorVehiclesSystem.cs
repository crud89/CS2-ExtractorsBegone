using Game;
using Game.Areas;
using Game.Common;
using Game.Objects;
using Game.Prefabs;
using Game.Tools;
using Game.Vehicles;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace crud89.ExtractorsBegone.Systems
{
    using crud89.ExtractorsBegone;

    public partial class DespawnExtractorVehiclesSystem : GameSystemBase
    {
        #region "Members"

        private EndFrameBarrier m_EndFrameBarrier;

        private EntityQuery m_VehicleQuery;
        #endregion

        #region "Job"

#if BURST
        [BurstCompile]
#endif
        private struct DespawnExtractorVehicleJob : IJobChunk
        {
            [ReadOnly]
            public EntityTypeHandle m_EntityType;

            [ReadOnly]
            public ComponentTypeHandle<PrefabRef> m_PrefabRefType;

            [ReadOnly]
            public BufferTypeHandle<LayoutElement> m_LayoutElementType;

            [ReadOnly]
            public ComponentLookup<WorkVehicleData> m_PrefabWorkVehicleData;

            public EntityCommandBuffer.ParallelWriter m_CommandBuffer;

            public bool m_AllowFarmVehicles, m_AllowForestVehicles, m_AllowOilVehicles, m_AllowOreVehicles, m_AllowFishingVehicles;

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                NativeArray<Entity> nativeArray = chunk.GetNativeArray(m_EntityType);
                NativeArray<PrefabRef> nativeArray4 = chunk.GetNativeArray(ref m_PrefabRefType);
                BufferAccessor<LayoutElement> bufferAccessor = chunk.GetBufferAccessor(ref m_LayoutElementType);

                for (int i = 0; i < nativeArray.Length; i++)
                {
                    Entity entity = nativeArray[i];
                    PrefabRef prefabRef = nativeArray4[i];
                    DynamicBuffer<LayoutElement> layout = default(DynamicBuffer<LayoutElement>);
                    var workVehicleData = m_PrefabWorkVehicleData[prefabRef.m_Prefab];

                    // If the work vehicle type for a map feature is disallowed, delete it below. Otherwise continue with the next entity.
                    switch(workVehicleData.m_MapFeature)
                    {
                        case MapFeature.FertileLand:
                            if (!m_AllowFarmVehicles)
                                break;
                            else continue;
                        case MapFeature.Forest:
                            if (!m_AllowForestVehicles)
                                break;
                            else continue;
                        case MapFeature.Oil:
                            if (!m_AllowOilVehicles)
                                break;
                            else continue;
                        case MapFeature.Ore:
                            if (!m_AllowOreVehicles)
                                break;
                            else continue;
                        case MapFeature.Fish:
                            if (!m_AllowFishingVehicles)
                                break;
                            else continue;
                        default:
                            continue;
                    }

                    if (bufferAccessor.Length != 0)
                        layout = bufferAccessor[i];

                    VehicleUtils.DeleteVehicle(m_CommandBuffer, unfilteredChunkIndex, entity, layout);
                }
            }
        }
        #endregion

        #region "System"

        protected override void OnCreate()
        {
            base.OnCreate();

            m_EndFrameBarrier = base.World.GetOrCreateSystemManaged<EndFrameBarrier>();
            m_VehicleQuery = GetEntityQuery(ComponentType.ReadWrite<Game.Vehicles.WorkVehicle>(), ComponentType.ReadOnly<PrefabRef>(), ComponentType.Exclude<Deleted>(), ComponentType.Exclude<Temp>(), ComponentType.Exclude<TripSource>(), ComponentType.Exclude<OutOfControl>());
            
            RequireForUpdate(m_VehicleQuery);
        }

        protected override void OnUpdate()
        {
            var job = new DespawnExtractorVehicleJob
            {
                m_EntityType = SystemAPI.GetEntityTypeHandle(),
                m_PrefabRefType = SystemAPI.GetComponentTypeHandle<PrefabRef>(true),
                m_LayoutElementType = SystemAPI.GetBufferTypeHandle<LayoutElement>(true),
                m_PrefabWorkVehicleData = SystemAPI.GetComponentLookup<WorkVehicleData>(true),
                m_CommandBuffer = m_EndFrameBarrier.CreateCommandBuffer().AsParallelWriter(),
                m_AllowFarmVehicles = ExtractorsBegone.Instance.Settings.AllowFarmVehicles,
                m_AllowForestVehicles = ExtractorsBegone.Instance.Settings.AllowForestVehicles,
                m_AllowOreVehicles = ExtractorsBegone.Instance.Settings.AllowOreVehicles,
                m_AllowOilVehicles = ExtractorsBegone.Instance.Settings.AllowOilVehicles,
                m_AllowFishingVehicles = ExtractorsBegone.Instance.Settings.AllowFishingBoats
            };
            
            var dependency = job.ScheduleParallel(m_VehicleQuery, Dependency);
            m_EndFrameBarrier.AddJobHandleForProducer(dependency);
            Dependency = dependency;
        }
        #endregion
    }
}
