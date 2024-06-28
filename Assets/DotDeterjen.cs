using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDeterjen : MonoBehaviour
{
    public Bounds bounds;
    void Start()
    {
        bounds = GetComponent<BoxCollider>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsImmuneBleachObtained()) return;
        
        if (bounds.Contains(GameObject.FindWithTag("Player").transform.position))
        {
            GameManager.Instance.IncreaseToxicity(30f * Time.deltaTime);
        }
    }
}
