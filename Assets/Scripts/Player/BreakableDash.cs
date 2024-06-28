using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDash : MonoBehaviour
{
    public GameObject breakPrefab;

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.IsDashObtained() && GameManager.Instance.IsDashing())
        {
            Debug.Log("Break on dash");
            BreakOnDash();
        }
    }

    private void BreakOnDash()
    {
        GameObject instantiated = Instantiate(breakPrefab, transform.position, Quaternion.identity);
        instantiated.transform.localScale = transform.localScale;
        Destroy(gameObject);

        foreach (Transform t in instantiated.transform)
        {
            var rb = t.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(10000, instantiated.transform.position, 10);
            }
            StartCoroutine(Shrink(t, 3f));
            Destroy(t.gameObject, 5);
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
