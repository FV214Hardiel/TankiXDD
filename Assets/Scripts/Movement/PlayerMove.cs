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

    public LayerMask Tank;
    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        engineAudio = GetComponent<AudioSource>();

        //GetComponent<EntityHandler>().moveScript = this;       


    }
    private void Update()
    {
        if (GameHandler.GameIsPaused)
        {
            engineAudio.pitch = 0;
            return;
        }


        PlayerMoveInput = Input.GetAxisRaw("Vertical");        
        EngineSoundInput = Input.GetAxis("Vertical");
        PlayerRotInput = Input.GetAxis("Horizontal");

        engineAudio.pitch = 0.2f + Mathf.Clamp01(Mathf.Abs(EngineSoundInput) + Mathf.Abs(PlayerRotInput)) * 0.4f;


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
        //engineAudio.pitch = 0.25f + 0.75f * (rb.velocity.magnitude / maxSpeed);
       
        
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




    //float PlayerRotInput;
    //float PlayerMoveInput;
    ////private float yRot;
    //private Rigidbody rb;
    //AudioSource engineAudio;
    //[SerializeField] private float Speed;    
    //[SerializeField] private float TurnSpeed;
    //public LayerMask Tank;

    ////bool isGrounded;
    ////Vector3 leftTrack;
    ////Vector3 rightTrack;



    //private void Start()
    //{
    //    GetComponent<EntityHandler>().moveScript = this;
    //    rb = GetComponent<Rigidbody>();
    //    engineAudio = GetComponent<AudioSource>();


    //}
    //private void Update()
    //{


    //    if (!GameHandler.GameIsPaused)
    //    {
    //        PlayerMoveInput = Input.GetAxisRaw("Vertical");
    //        PlayerRotInput = Input.GetAxis("Horizontal");
    //        engineAudio.pitch = 0.25f + PlayerMoveInput * 0.5f;
    //    }
    //    else
    //    {
    //        PlayerMoveInput = 0;
    //    }

    //    if (Input.GetKeyDown(KeyCode.Delete))
    //    {
    //        Vector3 rot = Player.PlayerHull.transform.eulerAngles;
    //        rot = new Vector3(rot.x + 180, rot.y + 180, rot.z);
    //        Player.PlayerHull.transform.eulerAngles = rot;
    //        Player.PlayerHull.transform.position =
    //            new Vector3(Player.PlayerHull.transform.position.x, Player.PlayerHull.transform.position.y + 2, Player.PlayerHull.transform.position.z);



    //    }

    //}


    //void FixedUpdate()
    //{
    //    rb.AddForce(PlayerMoveInput * Speed * transform.forward, ForceMode.Acceleration);
    //    //rb.AddTorque(transform.up * PlayerRotInput * TurnSpeed);
    //    //yRot = (PlayerRotInput) * TurnSpeed;
    //    transform.Rotate(0f, PlayerRotInput * TurnSpeed, 0f);

    //}

    ////private void OnCollisionStay(Collision collision)
    ////{
    ////    Debug.Log(collision.gameObject);
    ////    if (collision != null)
    ////    {
    ////        isGrounded = true;
    ////    }
    ////    else
    ////    {
    ////        isGrounded = false;
    ////    }
    ////}
    //public override float GetMaxSpeed()
    //{
    //    return Speed;
    //}

    //public override void SetMaxSpeed(float multiplier)
    //{
    //    Speed = multiplier;
    //}


}
