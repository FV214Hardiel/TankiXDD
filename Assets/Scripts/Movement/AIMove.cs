using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(WeaponAI))]

public class AIMove : Move
{
    
    public Vector3 patrolPoint;

    TankEntity eh;

    NavMeshAgent agent;
    RaycastHit hit;

    

    public float areaOfVision;
    public float attackRange;
    
    Transform target;
    Transform newTarget;

    float maxBlindChaseDuration;
    float chaseTimer;       
    
    public enum AIEnum { Patrol, Chase, Attack}
    public AIEnum AIState;

    Transform turret;

    Vector3 relPos;
    Quaternion InnerRotQ;   

    public LayerMask enemyLayers;
    public LayerMask ignoringLayers;

    bool isStunned;

    Transform muzzle;

    WeaponAI weaponScript;




    void Start()
    {
        eh = GetComponent<TankEntity>();
        weaponScript = GetComponent<WeaponAI>();

        ignoringLayers = eh.FriendlyMasks;

        maxBlindChaseDuration = 5;
        maxSpeed = 8;

        Speed = maxSpeed;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;

        engineAudio = GetComponent<AudioSource>();

        turret = GetComponentInChildren<Turret>().gameObject.transform;
        muzzle = turret.Find("muzzle");

        AIState = AIEnum.Patrol;
        agent.SetDestination(patrolPoint);


        StartCoroutine(CustomUpdate(1));

        eh.EntityStunned += OnStun;
        eh.EntityAwaken += OnUnStun;

    }

    private void OnStun()
    {
        
        engineAudio.enabled = false;
        agent.enabled = false;
        isStunned = true;

    }
    private void OnUnStun()
    {
       
        engineAudio.enabled = true;
        agent.enabled = true;
        isStunned = false;


    }
    private void OnDisable()
    {
        StopAllCoroutines();
        

        eh.EntityStunned -= OnStun;
        eh.EntityAwaken -= OnUnStun;
    }


    void Update()
    {
        if (isStunned) return;

        switch (AIState)
        {
            case AIEnum.Patrol:

                Patrol();
                break;
            case AIEnum.Chase:
                Chase();
                
                break;
            case AIEnum.Attack:

                Attack();
                break;
        }
    }

    public IEnumerator CustomUpdate(float timeDelta)
    {
        while (true)
        {
           // print(AIState);
            if (isStunned)
            {
                yield return new WaitForSeconds(timeDelta);
                continue;
            }
               
                chaseTimer -= timeDelta; //decreasing chase timer
            switch (AIState)
            {
                case AIEnum.Patrol:

                    PatrolCustomUpdate();
                    
                    break;


                case AIEnum.Chase:

                    ChaseCustomUpdate();

                    break;


                case AIEnum.Attack:

                    AttackCustomUpdate();

                    break;
            }

            yield return new WaitForSeconds(timeDelta);
        }
    }


    Transform SearchForEnemies()
    {
       // print("Searching");
        //Searching all colliders in range
        Collider[] colliders = Physics.OverlapSphere(transform.position, areaOfVision / 2, enemyLayers);

        Transform newTarget = null;
        float distance = areaOfVision / 2 + 1;

        if (colliders.Length > 0) //if there are targets in an inner circle
        {
            foreach (Collider item in colliders)
            { 
                if (Vector3.Distance(transform.position, item.transform.position) < distance) //If distance with new item is less than earlier
                {                           
                    distance = Vector3.Distance(transform.position, item.transform.position); //New least distance                           
                    newTarget = item.transform; //New target
                    
                }
            }
            
        }
        else
        {
            colliders = Physics.OverlapSphere(transform.position, areaOfVision, enemyLayers);
            foreach (Collider item in colliders)
            {
                if (Physics.Linecast(transform.position, item.transform.position, out RaycastHit hit, ~ignoringLayers)) //Checking direct vision
                {
                    if (hit.transform == item.transform) //If only intersection is with target
                    {
                        if (Vector3.Distance(transform.position, item.transform.position) < distance) //If distance with new item is less than earlier
                        {
                            distance = Vector3.Distance(transform.position, item.transform.position); //New least distance
                           
                            newTarget = item.transform; //New target
                        }

                    }
                }
            }
        }

        if (newTarget != null) newTarget = newTarget.GetComponentInParent<IEntity>().Gameobject.transform;
        return newTarget;
    }

    void Patrol()
    {
        //Debug.Log("Patrol");
    }

    void PatrolCustomUpdate()
    {
        agent.SetDestination(patrolPoint);
        target = SearchForEnemies();
        if (target != null) //if an enemy is near and visible then start chasing or attacking
        {
            if (Vector3.Distance(transform.position, target.position) < (attackRange - 3)) //if enemy in range of attack then attack
            {
                //AIState = AIEnum.Attack;
                //weaponScript.StartAttack();

                //agent.speed = maxSpeed / 3;
                //engineAudio.pitch = 0.6f;
                SwitchToAttack();
                //isTargetLocked = true;
            }
            else //if not just chase
            {
                //AIState = AIEnum.Chase;
                //agent.speed = maxSpeed;
                //engineAudio.pitch = 1;
                //chaseTimer = maxBlindChaseDuration;

                SwitchToChase();
            }
        }
    }

    void Chase()
    {
        if (target == null)
        {
            SwitchToPatrol();
            return;
        }
        relPos = Vector3.ProjectOnPlane((target.transform.position - turret.transform.position), transform.up);
        InnerRotQ = Quaternion.LookRotation(relPos, transform.up);
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, InnerRotQ, Time.deltaTime * 50);
    }

    void ChaseCustomUpdate()
    {
        if (target == null)
        {
            SwitchToPatrol();
            return;
        }
        newTarget = SearchForEnemies(); //if a new enemy is found then switch on the new target
        if (newTarget != null && newTarget != target)
        {
            target = newTarget;
            agent.SetDestination(target.position);
            return;
        }

        Physics.Linecast(transform.position, target.position, out hit, ~ignoringLayers);

        if (hit.transform == null) return;

        if (hit.collider.gameObject == target.gameObject) //if no intersection
        {
            if (hit.distance > (areaOfVision + 2)) //If enemy ran away
            {
                if (chaseTimer <= 0) //if timer ran out then forget the target
                {
                    SwitchToPatrol();
                }
                //if timer didnt run out then continue to last targets position
            }
            else //if enemy in vision and in range refresh position
            {
                chaseTimer = maxBlindChaseDuration;
                agent.SetDestination(target.position);
                if (Vector3.Distance(transform.position, target.position) < (attackRange - 3)) //if enemy in range of attack
                {
                    SwitchToAttack();
                    agent.SetDestination(target.position);
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
                    SwitchToPatrol();
                }
            }


        }
    }

    void Attack()
    {
        if (target == null)
        {
            SwitchToPatrol();
            return;
        }

        relPos = Vector3.ProjectOnPlane((target.transform.position - turret.transform.position), transform.up);
        InnerRotQ = Quaternion.LookRotation(relPos, transform.up);
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, InnerRotQ, Time.deltaTime * 50);

    }
    void AttackCustomUpdate()
    {
        if (target == null)
        {
            SwitchToPatrol();
            return;
        }
        //if target isnt visible OR out of range then chase
        Physics.Linecast(transform.position, target.position, out hit, ~ignoringLayers);   
       
        
        if (hit.transform != target || Vector3.Distance(transform.position, target.position) > (attackRange + 3))
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
            SwitchToChase();
        }
    }

    void SwitchToAttack()
    {
        AIState = AIEnum.Attack;
        weaponScript.StartAttack();
        agent.speed = maxSpeed / 3;
        engineAudio.pitch = 0.6f;
    }

    void SwitchToPatrol()
    {
        target = null;
        AIState = AIEnum.Patrol;
        weaponScript.EndAttack();
        agent.speed = maxSpeed / 2;
        engineAudio.pitch = 0.8f;
    }

    void SwitchToChase()
    {
        chaseTimer = maxBlindChaseDuration;
        AIState = AIEnum.Chase;
        weaponScript.EndAttack();
        agent.speed = maxSpeed;
        engineAudio.pitch = 1;
    }

}

