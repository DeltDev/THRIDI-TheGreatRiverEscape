using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace InteractableOject
{
    public class RadiusInteractionAuthoring : MonoBehaviour
    {
        public float radius;

        private class RadiusInteractionBaker : Baker<RadiusInteractionAuthoring>
        {
            public override void Bake(RadiusInteractionAuthoring authoring)
            {
                Entity e = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(e, new RadiusData
                {
                    Radius = authoring.radius,
                    filter = CollisionFilter.Default
                });
            }
        }
    }
}