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
    private Vector3 movementDirection;
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
            Vector3 verticalDirection = axisDirection.y * cameraTransform.forward;
            Vector3 horizontalDirection = axisDirection.x * cameraTransform.right;
            movementDirection = (verticalDirection + horizontalDirection).normalized;
            rb.AddForce(movementDirection * swimSpeed * Time.deltaTime, ForceMode.VelocityChange);
            Vector3 targetDirection = movementDirection;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime);
        } else {
            rb.velocity = Vector3.zero;
        }
    }
}
