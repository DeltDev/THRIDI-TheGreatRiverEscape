namespace InteractableOject
{
    using Unity.Entities;
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
}
