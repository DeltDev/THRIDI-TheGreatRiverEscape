using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainAntiDeterjen : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ObtainImmuneBleach();
            GameObject parent = transform.parent.gameObject;
            Destroy(parent);
        }
    }
}
