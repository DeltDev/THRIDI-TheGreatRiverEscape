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
    [SerializeField] private float dashForce;
    private float rotationSmoothVelocity;
    private Rigidbody rb;
    public bool IsDashing { get; private set; }
    
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
        inputManager.OnDashInput += Dash;
    }

    private void OnDestroy() {
        inputManager.OnMoveInput -= Move;
        inputManager.OnDashInput -= Dash;
    }

    private void Move(Vector2 axisDirection){
        if(axisDirection.magnitude >= 0.1){
            Vector3 verticalDirection = axisDirection.y * cameraTransform.forward;
            Vector3 horizontalDirection = axisDirection.x * cameraTransform.right;
            movementDirection = (verticalDirection + horizontalDirection).normalized;
            rb.AddForce(movementDirection * (swimSpeed * Time.deltaTime), ForceMode.VelocityChange);
            Vector3 targetDirection = movementDirection;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime);
        } else {
            rb.velocity = Vector3.zero;
        }
    }
    
    private void Dash()
    {
        if (IsDashing) return;
        rb.AddForce(dashForce * cameraTransform.forward);
        StartCoroutine(DashDuration());
    }
    
    private IEnumerator DashDuration()
    {
        IsDashing = true;
        yield return new WaitForSeconds(1); // Set this to the duration of the dash
        IsDashing = false;
    }
}
