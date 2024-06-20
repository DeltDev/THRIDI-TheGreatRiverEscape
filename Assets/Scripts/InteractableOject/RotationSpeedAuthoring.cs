namespace InteractableOject
{
    using Unity.Entities;
    using UnityEngine;

    public class RotationSpeedAuthoring : MonoBehaviour
    {
        public float rotationSpeed;

        private class RotationSpeedBaker : Baker<RotationSpeedAuthoring>
        {
            public override void Bake(RotationSpeedAuthoring authoring)
            {
                Entity e = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(e, new RotationSpeed
                {
                    Value = authoring.rotationSpeed
                });
            }
        }
    }
}