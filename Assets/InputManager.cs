using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public Action<Vector2> OnMoveInput;
    public Action OnDashInput;
    private void Update() {
        CheckMovementInput();
        CheckDashInput();
    }

    private void CheckMovementInput(){//buat cek input movement
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        // Debug.Log("Vertical Axis = "+verticalAxis);
        // Debug.Log("Horizontal Axis = "+horizontalAxis);
        Vector2 inputAxis = new Vector2(horizontalAxis,verticalAxis);
        if(OnMoveInput != null && inputAxis != Vector2.zero){
            OnMoveInput(inputAxis);
        }
    }
    
    private void CheckDashInput(){
        if(Input.GetKeyDown(KeyCode.LeftShift) && OnDashInput != null){
            OnDashInput();
        }
    }
}
