using System;
using UnityEngine;

public class BreakableDash : MonoBehaviour
{
    public GameObject breakPrefab;

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.Instance.IsDashObtained())
        {
            BreakOnDash();
        }
    }

    public void BreakOnDash()
    {
        GameObject instantiated = Instantiate(breakPrefab, transform.position, Quaternion.identity);
        instantiated.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }
}
