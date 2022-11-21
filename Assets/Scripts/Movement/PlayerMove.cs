using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : Move
{

    float PlayerRotInput;
    float PlayerMoveInput;
    
    Rigidbody rb;

    AudioSource engineAudio;

    [SerializeField] float Speed;
    [SerializeField] float TurnSpeed;
    public float maxSpeed;
    float EngineSoundInput;

    public WheelCollider[] wheels;

    bool isOnGround;

    PlayerInput playerInput;
    PlayerInputActions playerInputActions;

    private void Start()
    {
        TurnSpeed *= Time.fixedDeltaTime;

        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0.2f;

        engineAudio = GetComponent<AudioSource>();

        playerInputActions = new();
        playerInputActions.PlayerTankControl.Enable();
        playerInputActions.PlayerTankControl.Movement.started += AddTorque;
        playerInputActions.PlayerTankControl.Movement.canceled += ZeroTorque;
        

        StartCoroutine(CustomUpdate(0.2f));

    }
    private void Update()
    {
        //No movement and sound when paused
        if (GameHandler.instance.GameIsPaused)
        {
            engineAudio.pitch = 0;
            return;
        }

        //Getting input values. Smooth for engine audio for increasing sound effect
        PlayerMoveInput = Input.GetAxisRaw("Vertical");        
        EngineSoundInput = Input.GetAxis("Vertical");
        PlayerRotInput = Input.GetAxis("Horizontal");

        //Setting audio frequency
        engineAudio.pitch = 0.2f + Mathf.Clamp01(Mathf.Abs(EngineSoundInput) + Mathf.Abs(PlayerRotInput)) * 0.4f;

        //Flipping tank
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Vector3 rot = transform.eulerAngles;
            rot = new Vector3(rot.x + 180, rot.y + 180, rot.z);
            transform.eulerAngles = rot;
            transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        }

    }

    public IEnumerator CustomUpdate(float timeDelta)
    {
        while (true)
        {
            foreach (WheelCollider item in wheels)
            { 
                if (item.isGrounded)
                {
                    isOnGround = true;
                    break;
                }
                isOnGround = false;
                
            }
            yield return new WaitForSeconds(timeDelta);
        }

    }

    void FixedUpdate()
    {
        if (!isOnGround) return;

        rb.AddForce(PlayerMoveInput * Speed * transform.forward, ForceMode.Acceleration);
                
        transform.Rotate(0f, PlayerRotInput * TurnSpeed, 0f);
    }

    
    public override float GetMaxSpeed()
    {
        return Speed;
    }

    public override void SetMaxSpeed(float multiplier)
    {
        Speed *= multiplier;
    }

    void AddTorque(InputAction.CallbackContext callback)
    {

        //print("AddTorque");
        foreach (WheelCollider item in wheels)
        {
            item.wheelDampingRate = 1;
            item.motorTorque = 0.0001f;
            item.brakeTorque = 0;
        }
    }

    void ZeroTorque(InputAction.CallbackContext callback)
    {
        //print("ZeroTorque");
        foreach (WheelCollider item in wheels)
        {
            item.wheelDampingRate = 6;
            item.motorTorque = 0;
            item.brakeTorque = 40;
        }
    }

    private void OnDisable()
    {
        playerInputActions.PlayerTankControl.Movement.started -= AddTorque;
        playerInputActions.PlayerTankControl.Movement.canceled -= ZeroTorque;
    }

}
