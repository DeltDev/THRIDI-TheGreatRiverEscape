using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace InteractableOject
{
    public class ObjectDataAuthoring : MonoBehaviour
    {
        public ScriptObject scriptObject;

        private class ObjectDataBaker : Baker<ObjectDataAuthoring>
        {
            public override void Bake(ObjectDataAuthoring authoring)
            {
                Entity e = GetEntity(TransformUsageFlags.None);
                AddComponent(e, new ObjectData
                {
                    Hunger = authoring.scriptObject.hunger,
                    Toxicity = authoring.scriptObject.toxicity,
                });
            }
        }
    }
}