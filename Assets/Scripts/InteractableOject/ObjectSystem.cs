using Unity.Burst;

namespace InteractableOject
{
    using Unity.Entities;
    using Unity.Transforms;
    using Unity.Mathematics;
    public partial struct RotationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RotationSpeed>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach ((RefRW<LocalTransform> localTransform, RefRO<RotationSpeed> rotateSpeed) in SystemAPI.Query< RefRW<LocalTransform>, RefRO<RotationSpeed>>())
            {
                localTransform.ValueRW = localTransform.ValueRW.RotateY(rotateSpeed.ValueRO.Value * SystemAPI.Time.DeltaTime);
            }
        }
    }
    
    public partial struct FloatingSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<FloatingData>();
        }
    
        // [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach ((RefRW<LocalTransform> localTransform, RefRO<FloatingData> floatingData) in SystemAPI.Query< RefRW<LocalTransform>, RefRO<FloatingData>>())
            {
                float3 position = localTransform.ValueRW.Position;
                position.y = (float)(floatingData.ValueRO.InitialY + math.sin(SystemAPI.Time.ElapsedTime * floatingData.ValueRO.Frequency) * floatingData.ValueRO.Amplitude);
                localTransform.ValueRW.Position = position;
            }
        }
    }
}
    
