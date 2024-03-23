using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class Controls : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset controlAsset;
    private InputActionMap gameplayMap;
    private InputAction movement;

    private Rigidbody playerRb;

    [SerializeField]
    Joystick leftStick;

    private float horizontalInput, verticalInput;
    private float speed = 10f;
    private float velocity = 10f;
    private float stopModifier = 0.92f;


    private void Awake()
    {
        gameplayMap = controlAsset.FindActionMap("Gameplay");
        movement = gameplayMap.FindAction("Movement");

        playerRb = gameObject.GetComponent<Rigidbody>();

        movement.performed += context => Move(context);
        movement.canceled += context => Move(context);
    }

  
    private void OnEnable()
    {
        gameplayMap.Enable();
    }

    private void OnDisable()
    {
        gameplayMap.Disable();
    }


    public void Move(InputAction.CallbackContext context)
    {
        //horizontalInput = context.ReadValue<Vector2>().x;
        verticalInput = context.ReadValue<Vector2>().y;
        playerRb.AddForce(Vector3.forward * verticalInput * speed);
        //playerRb.AddForce(Vector3.right * horizontalInput * speed);

        if (playerRb.velocity.magnitude > velocity)
        {
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, velocity);
        }
        if (horizontalInput == 0 && verticalInput == 0)
        {
            playerRb.velocity = playerRb.velocity * stopModifier;

            //full stop
            if (Mathf.Abs(playerRb.velocity.x) < 0.2f && Mathf.Abs(playerRb.velocity.z) < 0.2f)
            {
                playerRb.velocity = playerRb.velocity * 0f;


            }
        }
    }
}
