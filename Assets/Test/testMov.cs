using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testMov : MonoBehaviour
{

    float PlayerRotInput;
    float PlayerMoveInput;

    Rigidbody rb;

    

    [SerializeField] float Speed;
    [SerializeField] float TurnSpeed;

    public WheelCollider[] wheels;

    PlayerInput playerInput;
    PlayerInputActions playerInputActions;

    public float brake;

    
    

    private void Start()
    {
        TurnSpeed *= Time.fixedDeltaTime;

        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0.2f;

        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new();
        playerInputActions.PlayerTankControl.Enable();
        playerInputActions.PlayerTankControl.Movement.started += AddTorque;
        playerInputActions.PlayerTankControl.Movement.canceled += ZeroTorque;




    }

    private void OnDisable()
    {
        playerInputActions.PlayerTankControl.Movement.started -= AddTorque;
        playerInputActions.PlayerTankControl.Movement.canceled -= ZeroTorque;

    }
    private void Update()
    {
        

        //Getting input values. Smooth for engine audio for increasing sound effect
        PlayerMoveInput = Input.GetAxisRaw("Vertical");
        
        PlayerRotInput = Input.GetAxis("Horizontal");
        

        //Setting audio frequency


        //Flipping tank
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Vector3 rot = transform.eulerAngles;
            rot = new Vector3(rot.x + 180, rot.y + 180, rot.z);
            transform.eulerAngles = rot;
            transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        }

    }

    void FixedUpdate()
    {
        rb.AddForce(PlayerMoveInput * Speed * transform.forward, ForceMode.Acceleration);
        

        transform.Rotate(0f, PlayerRotInput * TurnSpeed, 0f);
    }

    void AddTorque(InputAction.CallbackContext callback)
    {
        print("Added");

        foreach (WheelCollider item in wheels)
        {
            item.wheelDampingRate = 1;
            item.motorTorque = 0.0001f;
            item.brakeTorque = 0;
        }
    }

    void ZeroTorque(InputAction.CallbackContext callback)
    {
        print("Zeroed");
        foreach (WheelCollider item in wheels)
        {
            item.wheelDampingRate = 5;
            item.motorTorque = 0;
            item.brakeTorque = brake;
        }
    }

    public void Test(InputAction.CallbackContext callback)
    {
        //print(callback.action);
    }


}
