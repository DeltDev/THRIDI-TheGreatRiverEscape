using System.Data;
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
                float sin = math.sin((2 * math.PI * time * floatingData.Frequency) % (2 * math.PI));
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
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            var commandBuffer = new EntityCommandBuffer(Allocator.TempJob);
            float3 playerPosition = player.transform.position;
            Entities.WithAll<RadiusData>().ForEach(
                (Entity entity, in RadiusData radiusData, in LocalToWorld localToWorld) =>
                {
                    float3 position = localToWorld.Position;
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
    public struct GoodObject : IComponentData { }
    public struct BadObject : IComponentData { }


    [BurstCompile]
    public partial class ObjectSpawnerSystem : SystemBase
    {
        private EntityCommandBuffer commandBehavior;

        protected override void OnCreate()
        {
            RequireForUpdate<ObjectSpawnerData>();
        }

        protected override void OnUpdate()
        {
            commandBehavior = new EntityCommandBuffer(Allocator.Temp);
            Entities.WithoutBurst().ForEach((ref ObjectSpawnerData spawnerData) =>
            {
                spawnerData.Timer += SystemAPI.Time.DeltaTime;
                if (spawnerData.Timer >= spawnerData.SpawnRate)
                {
                    spawnerData.Timer = 0;
                    BatchRandomSpawn();
                }
            }).Run();
            commandBehavior.Playback(EntityManager);
        }
        
        private void BatchRandomSpawn()
        {
            var spawnerData = SystemAPI.GetSingleton<ObjectSpawnerData>();
            
            int goodObjectCount = 0;
            Entities.WithAll<GoodObject>().ForEach((Entity e) => { goodObjectCount++; }).Run();
            
            int badObjectCount = 0;
            Entities.WithAll<BadObject>().ForEach((Entity e) => { badObjectCount++; }).Run();
            
            for (int i = 0; i <= spawnerData.MaxSpawn-goodObjectCount; i++)
            {
                SpawnObject(spawnerData.pelet, true);
            }
        
            for (int i = 0; i <= spawnerData.MaxSpawn-badObjectCount; i++)
            {
                int random = UnityEngine.Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        SpawnObject(spawnerData.sampah1, false);
                        break;
                    case 1:
                        SpawnObject(spawnerData.sampah2, false);
                        break;
                    case 2:
                        SpawnObject(spawnerData.sampah3, false);
                        break;
                }
            }
        }
        //
        private void SpawnObject(Entity entity, bool isGood)
        {
            Vector3 position;
            bool positionIsValid;
            int attempts = 0;

            Terrain terrain = GameObject.FindWithTag("MainTerrain").GetComponent<Terrain>();
            Bounds bounds = terrain.terrainData.bounds;
            do
            {
                position = new Vector3(
                    UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                    0,
                    UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
                );

                // Set y to the height of the terrain at the position
                position.y = terrain.SampleHeight(position) + 10f;

                bool isInsideObject = Physics.CheckSphere(position, 5f);
                Vector3 viewportPosition = Camera.main.WorldToViewportPoint(position);
                bool isInRendererView =viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1 && viewportPosition.z > 0;
                bool isOnLand = position.y > bounds.max.y - 5f ;
                positionIsValid = !isInsideObject && !isInRendererView && !isOnLand;
                attempts++;
            }
            while (!positionIsValid && attempts < 100);

            if (!positionIsValid)
            {
                Debug.Log("Failed to find a valid position to spawn the object");
                return;
            }

            Entity spawnedEntity = EntityManager.Instantiate(entity);

            // Get the normal of the terrain at the position
            Vector3 normal = terrain.terrainData.GetInterpolatedNormal(position.x / terrain.terrainData.size.x, position.z / terrain.terrainData.size.z);

            // Calculate the rotation to align the object with the terrain
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normal);

            SystemAPI.SetComponent(spawnedEntity, new LocalTransform
            {
                Position = position,
                Rotation = rotation, // Use the calculated rotation
                Scale = 3f
            });

            SystemAPI.SetComponent(spawnedEntity, new FloatingData
            {
                Amplitude = CONST.AMPLITUDE,
                Frequency = CONST.FREQUENCY,
                InitialY = position.y
            });

            // If the object is good, add the GoodObject component
            if (isGood)
            {
                commandBehavior.AddComponent(spawnedEntity, new GoodObject());
            }
            else
            {
                commandBehavior.AddComponent(spawnedEntity, new BadObject());
            }
        }
    }
}