using Unity.Physics;

namespace InteractableOject
{
    using Unity.Entities;
    using Unity.Collections;

    public struct ObjectData : IComponentData
    {
        public FixedString64Bytes ObjectName;
        public float Hunger;
        public float Toxicity;
    }

    public struct RotationSpeed : IComponentData
    {
        public float Value;
    }
    
    public struct FloatingData : IComponentData
    {
        public float Amplitude;
        public float Frequency;
        public float InitialY;
    }
    
    public struct RadiusData : IComponentData
    {
        public float Radius;
        public CollisionFilter filter;
    }
}
