using UnityEngine;

namespace InteractableOject
{
    public class BadObject : BaseObject
    {
        public override void OnInteract()
        {
            if (objectData.doesInstaKill)
            {
                GameManager.Instance.IncreaseToxicity(100);
            }
            else
            {
                GameManager.Instance.IncreaseToxicity(objectData.valueOne);
            }
            
            if (objectData.doesIncreaseHunger)
            {
                GameManager.Instance.IncreaseHunger(objectData.valueTwo);
            }
            Destroy(gameObject);
        }
    }
}
