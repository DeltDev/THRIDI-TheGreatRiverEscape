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
                FixedString64Bytes SetObjectType = authoring.scriptObject.objectType.ToString();
                AddComponent(e, new ObjectData
                {
                    ObjectName = authoring.scriptObject.objectName,
                    ObjectType = SetObjectType,
                    Hunger = authoring.scriptObject.hunger,
                    Toxicity = authoring.scriptObject.toxicity,
                    IsIncreasingHunger = authoring.scriptObject.isIncreasingHunger,
                    IsInstantKill = authoring.scriptObject.isInstaKill
                });
            }
        }
    }
}