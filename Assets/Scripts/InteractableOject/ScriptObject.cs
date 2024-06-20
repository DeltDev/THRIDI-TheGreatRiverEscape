using UnityEditor.Rendering;
using UnityEngine;

namespace InteractableOject
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Script Object", menuName = "Script Object")]
    public class ScriptObject : ScriptableObject
    {
        [Header("Object Name")]
        public string objectName;
        public ObjectType objectType;
        public float hunger;
        public float toxicity;

        [Header("Object settings for bad objects")]
        public bool isIncreasingHunger; // if true, the object will increase the player's hunger, if false, it will decrease it
        public bool isInstaKill; // if true, the object will kill the player instantly
        
    }
    
    public enum ObjectType
    {
        Good,
        Bad
    }
}