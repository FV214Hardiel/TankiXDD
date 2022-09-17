using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : Move
{

    
    public Vector3 patrolPoint;
    public Vector3 goal;

    NavMeshAgent agent;
    Ray lineOfSight;
    RaycastHit hit;

    public LayerMask enemyLayers;
    public float areaOfVision;

    public float attackRange;
    //public bool isTargetLocked;
    
    Transform target;
    Transform newTarget;

    float maxBlindChaseDuration;
    float chaseTimer;

    

    bool playerAlive;
    

    AudioSource engineAudio;
    public enum AIEnum { Patrol, Chase, Attack}
    public AIEnum AIState;

    Transform turret;

    Vector3 relPos;
    Quaternion InnerRotQ;

    //[SerializeField] float maxSpeed;

   

    

    void Start()
    {
        maxBlindChaseDuration = 2;
        //maxSpeed = 5;

        agent = GetComponent<NavMeshAgent>();

        engineAudio = GetComponent<AudioSource>();

        turret = GetComponentInChildren<Turret>().gameObject.transform;

        
        AIState = AIEnum.Patrol;
        agent.SetDestination(patrolPoint);

        StartCoroutine(CustomUpdate(1));

    }

    
    void Update()
    {
       
        //switch(AIState)
        //{
        //    case AIEnum.Patrol:
                
        //        Patrol();
        //        break;
        //    case AIEnum.Chase:
        //        Chase();
        //        break;
        //    case AIEnum.Attack:
        //        Attack();
        //        break;
        //}        
    }

    public IEnumerator CustomUpdate(float timeDelta)
    {
        while (true)
        {
            Debug.Log(AIState);
            chaseTimer -= timeDelta; //decreasing chase timer
            switch (AIState)
            {
                case AIEnum.Patrol:
                    target = SearchForEnemies();
                    if (target != null) //if an enemy is near and visible then start chasing or attacking
                    {
                        if (Vector3.Distance(transform.position, target.position) < (attackRange - 3)) //if enemy in range of attack then attack
                        {
                            AIState = AIEnum.Attack;
                            //isTargetLocked = true;
                        }
                        else //if not just chase
                        {
                            AIState = AIEnum.Chase;
                            chaseTimer = maxBlindChaseDuration;
                        }
                    }
                    break;


                case AIEnum.Chase:
                    newTarget = SearchForEnemies(); //if a new enemy is found then switch on the new target
                    if (newTarget != null && newTarget != target)
                    {
                        
                        target = newTarget;
                        agent.SetDestination(target.position);
                        break;
                    }

                    Physics.Linecast(transform.position, target.position, out hit);
                    Debug.Log(hit.collider);
                    if (hit.collider.gameObject == target.gameObject) //if no intersection
                    {   
                        if (hit.distance > (areaOfVision+2)) //If enemy ran away
                        {   
                            if (chaseTimer <= 0) //if timer ran out then forget the target
                            {
                                target = null;
                                AIState = AIEnum.Patrol;
                                agent.SetDestination(patrolPoint);
                            }
                            //if timer didnt run out then continue to last targets position
                        }
                        else //if enemy in vision and in range refresh position
                        {
                            chaseTimer = maxBlindChaseDuration;
                            agent.SetDestination(target.position);
                            if (Vector3.Distance(transform.position, target.position) < (attackRange - 3)) //if enemy in range of attack
                            {
                                AIState = AIEnum.Attack;
                                //isTargetLocked = true;
                            }
                        }
                    }
                    else //if there is something between NPC and target
                    {
                        if (Vector3.Distance(transform.position, target.position) < areaOfVision / 2) //if target isnt visible but close enough then continue chase
                        {
                            chaseTimer = maxBlindChaseDuration;
                            agent.SetDestination(target.position);
                        }
                        else
                        {
                            if (chaseTimer <= 0) //if timer ran out then forget the target
                            {
                                target = null;
                                AIState = AIEnum.Patrol;
                                agent.SetDestination(patrolPoint);
                            }
                        }
                        

                    }
                    break;


                case AIEnum.Attack:
                    //if target isnt visible OR out of range then chase
                    Physics.Linecast(transform.position, target.position, out hit);
                    if (hit.collider.gameObject != target.gameObject || Vector3.Distance(transform.position, target.position) < (attackRange + 3))
                    {
                        newTarget = SearchForEnemies(); //if a new enemy is found then switch on the new target
                        if (newTarget != null) 
                        {
                            target = newTarget;
                            agent.SetDestination(target.position);
                            
                        }
                        else 
                        {
                            
                            agent.SetDestination(target.position);
                        }
                        chaseTimer = maxBlindChaseDuration;
                        AIState = AIEnum.Chase;
                    }
                                            
                    break;
            }

            yield return new WaitForSeconds(timeDelta);
        }
    }


    Transform SearchForEnemies()
    {        
        //Searching all colliders in range
        Collider[] colliders = Physics.OverlapSphere(transform.position, areaOfVision, enemyLayers);
        
        //
        Transform newTarget = null;
        float distance = areaOfVision + 10;
        
         foreach (Collider item in colliders)
            {
                if (Physics.Linecast(transform.position, item.transform.position, out RaycastHit hit)) //Checking direct vision
                {
                    if (hit.collider.gameObject == item.gameObject) //If only intersection is with target
                    {
                        //Debug.Log("Vision with " + item.name);

                        if (Vector3.Distance(transform.position, item.transform.position) < distance) //If distance with new item is less than earlier
                        {
                            distance = Vector3.Distance(transform.position, item.transform.position); //New least distance
                            newTarget = item.transform; //New target
                        }

                    }
            }
        }

        //Debug.Log(newTarget);
        return newTarget;
    }

    void Patrol()
    {
        //Debug.Log("Patrol");
    }

    void PatrolCustomUpdate()
    {
        SearchForEnemies();
    }

    void Chase()
    {
       
    }

    void ChaseCustomUpdate()
    {

    }

    void Attack()
    {
        

    }
    void AttackCustomUpdate()
    {


    }


}

//{

//    public GameObject player;
//public Vector3 patrolPoint;
//public Vector3 goal;

//NavMeshAgent agent;
//Ray lineOfSight;
//RaycastHit hit;
//public LayerMask playerMask;


//public Collider playerColl;
//bool playerAlive;
//HealthPlayer playerHealth;

//AudioSource engineAudio;
//public enum AIEnum { Patrol, Chase, Attack }
//public AIEnum AIState;

//Transform turret;

//Vector3 relPos;
//Quaternion InnerRotQ;

//[SerializeField] float maxSpeed;

//private void OnEnable()
//{
//    //patrolPoint = transform.position;
//    OnPlayerChanged();

//    Player.playerIsChanged += OnPlayerChanged;
//}

//private void OnDisable()
//{
//    Player.playerIsChanged -= OnPlayerChanged;
//}

//void OnPlayerChanged()
//{
//    Debug.Log("onpleerchange");
//    player = Player.PlayerHull;

//    //if (Player.PlayerHull.transform.position != null)
//    //{
//    //    patrolPoint = Player.PlayerHull.transform.position;
//    //}
//    if (player != null)
//    {
//        playerColl = player.GetComponent<Collider>();
//        playerHealth = playerColl.GetComponent<HealthPlayer>();
//        playerAlive = playerHealth.Alive;
//    }


//}

//void Start()
//{
//    maxSpeed = 5;

//    agent = GetComponent<NavMeshAgent>();

//    engineAudio = GetComponent<AudioSource>();

//    turret = GetComponentInChildren<Turret>().gameObject.transform;

//    goal = patrolPoint;
//    if (player == null)
//    {
//        Debug.Log("piska");
//    }

//    AIState = AIEnum.Patrol;
//    agent.SetDestination(goal);



//}


//void Update()
//{
//    switch (AIState)
//    {
//        case AIEnum.Patrol:
//            //Debug.Log("Patrol");
//            Patrol();
//            break;
//        case AIEnum.Chase:
//            Chase();
//            break;
//        case AIEnum.Attack:
//            Attack();
//            break;
//    }
//}

//public IEnumerator CustomUpdate(float timeDelta)
//{
//    while (true)
//    {
//        yield return new WaitForSeconds(timeDelta);
//    }
//}


//void Patrol()
//{
//    lineOfSight = new Ray(transform.position, player.transform.position - transform.position);
//    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
//    Physics.Raycast(lineOfSight, out hit);

//    //Debug.Log(hit.distance);        
//    //Debug.Log(hit.collider);
//    //Debug.Log(playerAlive);



//    if (hit.distance < 50 && hit.collider == playerColl && playerAlive)
//    {
//        Debug.Log("playerdetected");
//        agent.SetDestination(player.transform.position);
//        agent.speed = maxSpeed;
//        engineAudio.pitch = Mathf.Clamp01(agent.speed / 5);
//        AIState = AIEnum.Chase;

//    }
//}

//void Chase()
//{
//    lineOfSight = new Ray(transform.position, player.transform.position - transform.position);
//    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
//    Physics.Raycast(lineOfSight, out hit);

//    relPos = Vector3.ProjectOnPlane((player.transform.position - turret.transform.position), transform.up);
//    InnerRotQ = Quaternion.LookRotation(relPos, transform.up);
//    turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, InnerRotQ, Time.deltaTime * 50);

//    agent.SetDestination(player.transform.position);

//    if (hit.distance > 50 || !playerAlive)
//    {
//        agent.SetDestination(patrolPoint);
//        engineAudio.pitch = Mathf.Clamp01(agent.speed / 5);
//        AIState = AIEnum.Patrol;
//    }
//    if (hit.distance < 19 && hit.collider == playerColl)
//    {
//        agent.speed = maxSpeed / 3;
//        engineAudio.pitch = Mathf.Clamp01(agent.speed / 5);
//        AIState = AIEnum.Attack;
//    }
//}

//void Attack()
//{
//    lineOfSight = new Ray(transform.position, player.transform.position - transform.position);
//    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
//    Physics.Raycast(lineOfSight, out hit);

//    relPos = Vector3.ProjectOnPlane((player.transform.position - turret.transform.position), transform.up);
//    InnerRotQ = Quaternion.LookRotation(relPos, transform.up);
//    turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, InnerRotQ, Time.deltaTime * 50);

//    agent.SetDestination(player.transform.position);
//    playerAlive = playerHealth.Alive;

//    if ((hit.collider != Player.PlayerHullColl && hit.collider != Player.PlayerTurretColl) || hit.distance > 21)
//    {
//        agent.speed = maxSpeed;
//        engineAudio.pitch = Mathf.Clamp01(agent.speed / 5);
//        AIState = AIEnum.Chase;

//    }

//    if (!playerAlive)
//    {
//        agent.SetDestination(patrolPoint);
//        engineAudio.pitch = Mathf.Clamp01(agent.speed / 5);
//        AIState = AIEnum.Patrol;
//    }

//}

    
//}


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
//    bool isPatrol;

//    Transform turret;

//    Vector3 relPos;
//    Quaternion InnerRotQ;

//    private void OnEnable()
//    {
//        patrolPoint = transform.position;
//        OnPlayerChanged();
//        Player.playerIsChanged += OnPlayerChanged; //Á‰ÂÒ¸ Ï˚ ÔÓ‰ÔËÒ˚‚‡ÂÏÒˇ

//    }

//    private void OnDisable()
//    {
//        //ÚÛÚ Ï˚ ÓÚÔËÒ˚‚‡ÂÏÒˇ
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
//        isPatrol = true;
//        agent.SetDestination(goal);



//    }

//    // ( Time.deltaTime * 50 = 1) ◊“Œ¡€ ¬€–Œ¬Õﬂ“‹ ‘» —≈ƒ ¿œƒ≈…“ » œ–Œ—“Œ ¿œƒ≈…“
//    void Update()
//    {
//        lineOfSight = new Ray(transform.position, player.transform.position - transform.position);
//        Debug.DrawRay(transform.position, player.transform.position - transform.position);
//        Physics.Raycast(lineOfSight, out hit);


//        if (isPatrol)
//        {
//            Patrol();
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


//    void Patrol()
//    {

//        if (hit.distance < 50 && hit.collider == playerColl && playerAlive)
//        {

//            agent.SetDestination(player.transform.position);
//            agent.speed = 6;
//            inChase = true;

//            isPatrol = false;
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
//            isPatrol = true;

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
//            isPatrol = true;

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