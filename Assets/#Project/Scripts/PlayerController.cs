using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveVector;
    public float movementSpeed;
    float verticalSpeed; // chaque update, elle va augmenter de vitesse pour simuler la gravité qui attire Player
    public CharacterController charactController;
    public float gravityMultiplier;
    public float jumping;



    public void ShootMcCree(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("PAN!");
        }
        else if(context.canceled)
        {
            Debug.Log("*bruit de balle qui tombe");
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && charactController.isGrounded)
        {   
            verticalSpeed += jumping;
           
        } 
    }
    void Update() 
    {
        
        if(charactController.isGrounded && verticalSpeed < 0)
        {   
            verticalSpeed = 0;
        }
        
        
        verticalSpeed += Physics.gravity.y * Time.deltaTime  * gravityMultiplier;  // accélération (m/s²) = vitesse (m/s) * temps(s)
        

        Vector3 movement = new Vector3(moveVector.x, 0 , moveVector.y) *movementSpeed;

        if(movement != Vector3.zero) // movement.magnitude >= 0 magnitude = grandeur (pas géométrique)
        {
            transform.forward = Vector3.Lerp(transform.forward,movement,0.02f);
        }


        movement.y = verticalSpeed;
        charactController.Move(movement * Time.deltaTime);
    }
}
