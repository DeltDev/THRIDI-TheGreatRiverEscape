using Unity.Entities;
using UnityEngine;

namespace InteractableOject
{
    public class ObjectSpawnerAuthoring : MonoBehaviour
    {

        public GameObject prefab;

        private class ObjectSpawnerBaker : Baker<ObjectSpawnerAuthoring>
        {
            public override void Bake(ObjectSpawnerAuthoring authoring)
            {
                Entity e = GetEntity(TransformUsageFlags.None);
                AddComponent(e, new ObjectSpawnerData
                {
                    prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
        
    }
    
    public struct ObjectSpawnerData : IComponentData
    {
        public Entity prefab;
    }
}