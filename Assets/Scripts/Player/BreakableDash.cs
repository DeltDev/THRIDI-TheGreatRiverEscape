using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDash : MonoBehaviour
{
    public GameObject breakPrefab;

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.Instance.IsDashObtained() && GameManager.Instance.IsDashing())
        {
            Debug.Log("Break on dash");
            BreakOnDash();
        }
    }

    private void BreakOnDash()
    {
        GameObject instantiated = Instantiate(breakPrefab, transform.position, Quaternion.identity, transform.parent);
        instantiated.transform.rotation = transform.rotation;
        Destroy(gameObject);

        foreach (Transform t in instantiated.transform)
        {
            var rb = t.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(1000, instantiated.transform.position, 10);
            }
            StartCoroutine(Shrink(t, 2f));
            Destroy(t.gameObject, 10);
        }
    }
    
    private IEnumerator Shrink(Transform t, float delay)
    {
        yield return new WaitForSeconds(delay);
        while (t.localScale.x > 0.1f)
        {
            t.localScale -= Vector3.one * Time.deltaTime;
        }
    }
}
