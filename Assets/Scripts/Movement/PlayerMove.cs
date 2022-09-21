using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private void Start()
    {
        TurnSpeed *= Time.fixedDeltaTime;

        rb = GetComponent<Rigidbody>();

        engineAudio = GetComponent<AudioSource>();

    }
    private void Update()
    {
        //No movement and sound when paused
        if (GameHandler.GameIsPaused)
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

    void FixedUpdate()
    {
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

}
