using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float swimSpeed;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float rotationSmoothTime = 0.1f;
    [SerializeField] private Transform cameraTransform;
    private float rotationSmoothVelocity;
    private Rigidbody rb;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    private void HideAndLockCursor(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Start() {
        inputManager.OnMoveInput += Move;
    }

    private void OnDestroy() {
        inputManager.OnMoveInput -= Move;
    }

    private void Move(Vector2 axisDirection){
        if(axisDirection.magnitude >= 0.1){
            float clampedY = cameraTransform.eulerAngles.y;
            clampedY = Mathf.Clamp(clampedY,-60f,60f);
            float rotationAngle1 = Mathf.Atan2(axisDirection.x, axisDirection.y) * Mathf.Rad2Deg + clampedY;
            float smoothAngle1 = Mathf.SmoothDampAngle(transform.eulerAngles.y,rotationAngle1,ref rotationSmoothVelocity, rotationSmoothTime);
            float cameraPitch = cameraTransform.eulerAngles.x;
            if (cameraPitch > 180){
                cameraPitch -= 360;
            } 
            cameraPitch = Mathf.Clamp(cameraPitch, -60f, 60f);
            Quaternion targetRotation = Quaternion.Euler(cameraPitch, smoothAngle1, 0f);
            rb.MoveRotation(targetRotation);
            Vector3 movementDirection = Quaternion.Euler(cameraPitch,rotationAngle1,0f) * Vector3.forward;
            Debug.Log(movementDirection);
            rb.AddForce(swimSpeed * Time.deltaTime * movementDirection);
        } else {
            rb.velocity = Vector3.zero;
        }
    }
}
