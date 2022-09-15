using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : Move
{

    public GameObject player;
    public Vector3 patrolPoint;
    public Vector3 goal;

    NavMeshAgent agent;
    Ray lineOfSight;
    RaycastHit hit;
    public LayerMask playerMask;

    [HideInInspector]
    public Collider playerColl;
    bool playerAlive;
    HealthPlayer playerHealth;

    AudioSource engineAudio;
    public enum AIEnum { Idle, Chase, Attack}
    public AIEnum AIState;

    Transform turret;

    Vector3 relPos;
    Quaternion InnerRotQ;

    [SerializeField ]float maxSpeed;

    private void OnEnable()
    {
        //patrolPoint = transform.position;
        OnPlayerChanged();
        
        Player.playerIsChanged += OnPlayerChanged; //çäåñü ìû ïîäïèñûâàåìñÿ

    }

    private void OnDisable()
    {
        //òóò ìû îòïèñûâàåìñÿ
        Player.playerIsChanged -= OnPlayerChanged;
    }

    void OnPlayerChanged()
    {
        Debug.Log("onpleerchange");
        player = Player.PlayerHull;

        //if (Player.PlayerHull.transform.position != null)
        //{
        //    patrolPoint = Player.PlayerHull.transform.position;
        //}
        if (player != null)
        {
            playerColl = player.GetComponent<Collider>();
            playerHealth = playerColl.GetComponent<HealthPlayer>();
            playerAlive = playerHealth.Alive;
        }

        
    }

    void Start()
    {
        maxSpeed = 5;

        agent = GetComponent<NavMeshAgent>();

        engineAudio = GetComponent<AudioSource>();

        turret = GetComponentInChildren<Turret>().gameObject.transform;

        goal = patrolPoint;
        if (player == null)
        {
            Debug.Log("piska");
        }

        AIState = AIEnum.Idle;
        agent.SetDestination(goal);



    }

    // ( Time.deltaTime * 50 = 1) ×ÒÎÁÛ ÂÛĞÎÂÍßÒÜ ÔÈÊÑÅÄ ÀÏÄÅÉÒ È ÏĞÎÑÒÎ ÀÏÄÅÉÒ
    void FixedUpdate()
    {
        

        switch(AIState)
        {
            case AIEnum.Idle:
                //Debug.Log("idle");
                Idle();
                break;
            case AIEnum.Chase:
                Chase();
                break;
            case AIEnum.Attack:
                Attack();
                break;
        }

        
    }


    void Idle()
    {
        lineOfSight = new Ray(transform.position, player.transform.position - transform.position);
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        Physics.Raycast(lineOfSight, out hit);

        //Debug.Log(hit.distance);        
        //Debug.Log(hit.collider);
        //Debug.Log(playerAlive);



        if (hit.distance < 50 && hit.collider == playerColl && playerAlive)
        {
            Debug.Log("playerdetected");
            agent.SetDestination(player.transform.position);
            agent.speed = maxSpeed;
            engineAudio.pitch = Mathf.Clamp01(agent.speed / 5);
            AIState = AIEnum.Chase;
            
        }
    }

    void Chase()
    {
        lineOfSight = new Ray(transform.position, player.transform.position - transform.position);
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        Physics.Raycast(lineOfSight, out hit);

        relPos = Vector3.ProjectOnPlane((player.transform.position - turret.transform.position), transform.up);
        InnerRotQ = Quaternion.LookRotation(relPos, transform.up);
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, InnerRotQ, 1);

        agent.SetDestination(player.transform.position);

        if (hit.distance > 50 || !playerAlive)
        {
            agent.SetDestination(patrolPoint);
            engineAudio.pitch = Mathf.Clamp01(agent.speed / 5);
            AIState = AIEnum.Idle;
        }
        if (hit.distance < 19 && hit.collider == playerColl)
        {
            agent.speed = maxSpeed/3;
            engineAudio.pitch = Mathf.Clamp01(agent.speed / 5);
            AIState = AIEnum.Attack;
        }
    }

    void Attack()
    {
        lineOfSight = new Ray(transform.position, player.transform.position - transform.position);
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        Physics.Raycast(lineOfSight, out hit);

        relPos = Vector3.ProjectOnPlane((player.transform.position - turret.transform.position), transform.up);
        InnerRotQ = Quaternion.LookRotation(relPos, transform.up);
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, InnerRotQ, 1);

        agent.SetDestination(player.transform.position);
        playerAlive = playerHealth.Alive;

        if ((hit.collider != Player.PlayerHullColl && hit.collider != Player.PlayerTurretColl) || hit.distance > 21)
        {
            agent.speed = maxSpeed;
            engineAudio.pitch = Mathf.Clamp01(agent.speed / 5);
            AIState = AIEnum.Chase;

        }

        if (!playerAlive)
        {
            agent.SetDestination(patrolPoint);
            engineAudio.pitch = Mathf.Clamp01(agent.speed / 5);
            AIState = AIEnum.Idle;
        }

    }


}

//public class AIMove : Move
//{

//    public GameObject player;
//    public Vector3 patrolPoint;
//    public Vector3 goal;   

//    NavMeshAgent agent;
//    Ray lineOfSight;
//    RaycastHit hit;
//    public LayerMask playerMask;

//    [HideInInspector]
//    public Collider playerColl;
//    bool playerAlive;
//    HealthPlayer playerHealth;
//    bool inChase;

//    [HideInInspector]
//    public bool inAttack;
//    bool isIdle;

//    Transform turret;

//    Vector3 relPos;
//    Quaternion InnerRotQ;

//    private void OnEnable()
//    {
//        patrolPoint = transform.position;
//        OnPlayerChanged();
//        Player.playerIsChanged += OnPlayerChanged; //çäåñü ìû ïîäïèñûâàåìñÿ

//    }

//    private void OnDisable()
//    {
//        //òóò ìû îòïèñûâàåìñÿ
//        Player.playerIsChanged -= OnPlayerChanged;
//    }

//    void OnPlayerChanged()
//    {
//        player = Player.PlayerHull;
//        patrolPoint = Player.PlayerHull.transform.position;
//        playerColl = player.GetComponent<Collider>();
//        playerHealth = playerColl.GetComponent<HealthPlayer>();
//        playerAlive = playerHealth.Alive;
//    }

//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();

//        turret = GetComponentInChildren<Turret>().gameObject.transform;

//        if (goal == null)
//        {
//            goal = patrolPoint;
//        }

//        /*playerColl = player.GetComponent<Collider>();
//        playerHealth = playerColl.GetComponent<HealthPlayer>();
//        playerAlive = playerHealth.Alive;
//        playerAlive = true;*/

//        inChase = false;
//        inAttack = false;
//        isIdle = true;
//        agent.SetDestination(goal);



//    }

//    // ( Time.deltaTime * 50 = 1) ×ÒÎÁÛ ÂÛĞÎÂÍßÒÜ ÔÈÊÑÅÄ ÀÏÄÅÉÒ È ÏĞÎÑÒÎ ÀÏÄÅÉÒ
//    void Update()
//    {
//        lineOfSight = new Ray(transform.position, player.transform.position - transform.position);
//        Debug.DrawRay(transform.position, player.transform.position - transform.position);
//        Physics.Raycast(lineOfSight, out hit);


//        if (isIdle)
//        {
//            Idle();
//        }

//        if (inChase)
//        {

//            Chase();

//        }

//        if (inAttack)
//        {
//            Attack();
//        }



//    }


//    void Idle()
//    {

//        if (hit.distance < 50 && hit.collider == playerColl && playerAlive)
//        {

//            agent.SetDestination(player.transform.position);
//            agent.speed = 6;
//            inChase = true;

//            isIdle = false;
//        }
//    }

//    void Chase()
//    {
//        relPos = Vector3.ProjectOnPlane((player.transform.position - turret.transform.position), transform.up);
//        InnerRotQ = Quaternion.LookRotation(relPos, transform.up);
//        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, InnerRotQ, Time.deltaTime * 50);

//        agent.SetDestination(player.transform.position);

//        if (hit.distance > 50 || !playerAlive)
//        {

//            agent.SetDestination(patrolPoint);
//            isIdle = true;

//            inChase = false;
//        }
//        if (hit.distance < 19 && hit.collider == playerColl)
//        {

//            agent.speed = 2;
//            inAttack = true;

//            inChase = false;
//        }
//    }

//    void Attack()
//    {
//        relPos = Vector3.ProjectOnPlane((player.transform.position - turret.transform.position), transform.up);
//        InnerRotQ = Quaternion.LookRotation(relPos, transform.up);
//        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, InnerRotQ, Time.deltaTime * 50);

//        agent.SetDestination(player.transform.position);
//        playerAlive = playerHealth.Alive;

//        if ((hit.collider != Player.PlayerHullColl && hit.collider != Player.PlayerTurretColl) || hit.distance > 21)
//        {
//            agent.speed = 6;
//            inChase = true;

//            inAttack = false;

//        }

//        if (!playerAlive)
//        {
//            agent.SetDestination(patrolPoint);
//            isIdle = true;

//            inAttack = false;
//        }

//    }


//}

/*
public Transform goal;
Transform parent;
NavMeshAgent agent;
void Start()
{

    agent = GetComponent<NavMeshAgent>();
    Debug.Log(agent.steeringTarget);
    agent.SetDestination(transform.position);


}


void Update()
{
    agent.SetDestination(goal.position);

}*/