using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Vector3 endPoint;
    private float current,target;

    
    void Start()
    {
        target = 1;
    }
    void Update()
    {
        if(transform.position == endPoint || transform.position == Vector3.zero){
            target = target == 0 ? 1 : 0;
        }
        current = Mathf.MoveTowards(current,target,MoveSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(Vector3.zero, endPoint, current);
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("Masih bisa diklik");
        }
    }
}
