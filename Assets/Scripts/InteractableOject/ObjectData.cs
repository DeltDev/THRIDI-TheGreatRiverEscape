using UnityEngine;

namespace InteractableOject
{
    [CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/ObjectData", order = 1)]
    public class ObjectData : ScriptableObject
    {
        [Header("Values")]
        public string objectName;
        public ObjectType objectType;
        public float valueOne;
        public float valueTwo;
        
        [Header("CheckBox For BadObject Only")]
        public bool doesIncreaseHunger;
        public bool doesInstaKill;
    }
    
    public enum ObjectType
    {
        Good,
        Bad
    }
}