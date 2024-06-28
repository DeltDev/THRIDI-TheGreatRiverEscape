using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Night Power Up");
            GameManager.Instance.ObtainNightVision();
            Destroy(transform.parent.gameObject);
        }
    }
}
