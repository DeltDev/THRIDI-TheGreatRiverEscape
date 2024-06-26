using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public Action<Vector2> OnMoveInput;
    private void Update() {
        CheckMovementInput();
    }

    private void CheckMovementInput(){//buat cek input movement
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        // Debug.Log("Vertical Axis = "+verticalAxis);
        // Debug.Log("Horizontal Axis = "+horizontalAxis);
        Vector2 inputAxis = new Vector2(horizontalAxis,verticalAxis);
        if(OnMoveInput != null){
            OnMoveInput(inputAxis);
        }
    }
}
