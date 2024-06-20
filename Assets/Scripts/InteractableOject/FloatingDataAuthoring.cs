using Unity.Entities;
using UnityEngine;

namespace InteractableOject
{
    public class FloatingDataAuthoring : MonoBehaviour
    {
        public float amplitude;
        public float frequency;

        private class FloatingDataBaker : Baker<FloatingDataAuthoring>
        {
            public override void Bake(FloatingDataAuthoring authoring)
            {
                Entity e = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(e, new FloatingData
                {
                    Amplitude = authoring.amplitude,
                    Frequency = authoring.frequency,
                    InitialY = authoring.transform.position.y
                });
            }
        }
    }
}