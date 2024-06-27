using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0;

    [SerializeField] private float floatingSpeed = 5;

    [SerializeField] private float SprintBooster = 2;
    private Transform _camera;

    [SerializeField] private Vector2 rotationSpeed = new Vector2(20,20);
    [SerializeField] private float maxSpeed = 10; 
    private float x,y,z;

    private float rotX, rotY;

    void Start()
    {
        _camera = GetComponentInChildren<Camera>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        UpdateMovement();
    }

    private void MovementInput(){
        x = Input.GetAxis("Vertical");
        z = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.Space)){
            y = floatingSpeed;
        }

        
    }

    private void CameraMovement(){
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * rotationSpeed.x;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * rotationSpeed.y;

        rotY += mouseX;
        rotX -= mouseY;

        rotX = Mathf.Clamp(rotX, -90f, 90f); //Player binder

        _camera.rotation = Quaternion.Euler(rotX, rotY, 0); // Camera
        transform.rotation = Quaternion.Euler(0, rotY, 0); // Player

        
    }


    private void UpdateMovement(){
        MovementInput();
        float speedBoost = 1;
        if(Input.GetKey(KeyCode.LeftShift)){
            speedBoost = SprintBooster;
        }

        // Move the player in local space for X and Z directions
        Vector3 horizontalMove = transform.right * z + transform.forward * (x * speedBoost);
        horizontalMove = Vector3.ClampMagnitude(horizontalMove, maxSpeed) * (speed * Time.deltaTime);

        // Move the player in world space for Y direction
        Vector3 verticalMove = Vector3.up * (y * Time.deltaTime);

        // Combine horizontal and vertical movements
        Vector3 move = horizontalMove + verticalMove;

        transform.position += move;
        
        y = 0;
    }
}
