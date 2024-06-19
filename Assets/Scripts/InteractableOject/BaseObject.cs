using System;
using UnityEngine;

namespace InteractableOject
{
    public class BaseObject : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private new Collider collider;
        [SerializeField] private bool _canInteract;
        [SerializeField] protected ObjectData objectData;
    
        private void Start()
        {
            _canInteract = true;
        }
        
        public void SetCanInteract(bool canInteract)
        {
            _canInteract = canInteract;
        }

        public virtual void OnInteract()
        {
            throw new NotImplementedException("OnInteract method not implemented");
        }

        public bool CanInteract()
        {
            return _canInteract;
        }

        public bool IsColliding(Collider c)
        {
            return collider.bounds.Intersects(c.bounds);
        }
    }
}
