using Unity.Collections;

namespace InteractableOject
{
    using Unity.Entities;
    using Unity.Transforms;
    using Unity.Mathematics;
    using Unity.Burst;
    using UnityEngine;

    public partial struct RotationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RotationSpeed>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            RotationSystemJob job = new RotationSystemJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime
            };
            job.ScheduleParallel();
        }
        
        [BurstCompile]
        public partial struct RotationSystemJob : IJobEntity
        {
            public float DeltaTime;
        
            public void Execute(ref LocalTransform localTransform, ref RotationSpeed rotateSpeed)
            {
                localTransform = localTransform.RotateY(rotateSpeed.Value * DeltaTime);
            }
        }
    }
    
    public partial struct FloatingSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<FloatingData>();
        }
    
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            FloatingSystemJob job = new FloatingSystemJob
            {
                time = (float) SystemAPI.Time.ElapsedTime
            };
            job.ScheduleParallel();
        }
        
        [BurstCompile]
        public partial struct FloatingSystemJob : IJobEntity
        {
            public float time;
            public void Execute(ref LocalTransform localTransform, ref FloatingData floatingData)
            {
                float sin = math.sin(2 * math.PI * time * floatingData.Frequency);
                Debug.Log(sin);
                float newY = floatingData.InitialY + sin * floatingData.Amplitude;
                localTransform.Position.y = newY;
            }
        }
    }
    
    [BurstCompile]
    public partial class RadiusSystem : SystemBase
    {
        public GameObject player;
    
        protected override void OnCreate()
        {
            RequireForUpdate<RadiusData>();
            RequireForUpdate<ObjectData>();
            player = GameObject.FindWithTag("Player");
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var commandBuffer = new EntityCommandBuffer(Allocator.TempJob);
            float3 playerPosition = player.transform.position;
            Entities.WithAll<RadiusData>().ForEach(
                (Entity entity, in RadiusData radiusData, in LocalToWorld localToWorld) =>
                {
                    float3 position = localToWorld.Position;
                    float Distance = math.distance(playerPosition, position);
                    if (math.distance(playerPosition, position) < radiusData.Radius)
                    {
                        commandBuffer.AddComponent<MarkedForDestruction>(entity);
                    }
                }).Run();

            commandBuffer.Playback(EntityManager);
            commandBuffer.Dispose();
        }
    }
    
    [BurstCompile]
    public partial class HandleMarkedEntitiesSystem : SystemBase
    {
        public GameManager gameManager;
        protected override void OnCreate()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        
        protected override void OnUpdate()
        {
            var commandBuffer = new EntityCommandBuffer(Allocator.TempJob);
            Entities.WithoutBurst().WithAll<MarkedForDestruction, ObjectData>().ForEach(
                (Entity entity, in ObjectData objectData) =>
                {
                    gameManager.IncreaseHunger(objectData.Hunger);
                    gameManager.IncreaseToxicity(objectData.Toxicity);
                    commandBuffer.DestroyEntity(entity);
                }).Run();

            commandBuffer.Playback(EntityManager);
            commandBuffer.Dispose();
        }
    }
    
    public struct MarkedForDestruction : IComponentData { }
}