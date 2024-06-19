using UnityEngine;

namespace InteractableOject
{
    public class GoodObject : BaseObject
    {
        public override void OnInteract()
        { 
            GameManager.Instance.IncreaseHunger(objectData.valueOne);
            Destroy(gameObject);
        }
    }
}
