using Unity.Entities;
using UnityEngine;

namespace InteractableOject
{
    public class ObjectSpawnerAuthoring : MonoBehaviour
    {
        public GameObject pelet;
        public GameObject sampah1;
        public GameObject sampah2;
        public GameObject sampah3;
        
        public float spawnRate;
        public int maxSpawn;

        private class ObjectSpawnerBaker : Baker<ObjectSpawnerAuthoring>
        {
            public override void Bake(ObjectSpawnerAuthoring authoring)
            {
                Entity e = GetEntity(TransformUsageFlags.None);
                AddComponent(e, new ObjectSpawnerData
                {
                    pelet = GetEntity(authoring.pelet, TransformUsageFlags.Dynamic),
                    sampah1 = GetEntity(authoring.sampah1, TransformUsageFlags.Dynamic),
                    sampah2 = GetEntity(authoring.sampah2, TransformUsageFlags.Dynamic),
                    sampah3 = GetEntity(authoring.sampah3, TransformUsageFlags.Dynamic),
                    Timer = authoring.spawnRate,
                    SpawnRate = authoring.spawnRate,
                    MaxSpawn = authoring.maxSpawn
                });
            }
        }
        
    }
    
    public struct ObjectSpawnerData : IComponentData
    {
        public Entity pelet;
        public Entity sampah1;
        public Entity sampah2;
        public Entity sampah3;
        
        public float Timer;
        public float SpawnRate;
        public int MaxSpawn;
    }
}