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
        public float hunger;
        public float toxicity;
    }
}