using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public TankHull[] Hull;
    public TankTurret[] Turret;

    public float SpawnCD;
    public float detectionRange;
    public bool CDready;

    public bool noEnemy;
    public bool spawnWithoutPlayerNear;
    public int availableSpawns;

    GameObject enemy;
    
    WaitForSeconds CD;    

    public GameObject player;

    //Ray lineOfSight;

    //public LayerMask playerMask;
    float dist;


    void Start()
    {
        //availableSpawns = 2;
        CDready = true;
        noEnemy = true;
        CD = new WaitForSeconds(SpawnCD);

        player = Player.PlayerHull;
        
    }


    void Update()
    {
        
        dist = Vector3.Distance(transform.position, player.transform.position);
        if (CDready & noEnemy & (dist < detectionRange || spawnWithoutPlayerNear) & availableSpawns > 0)
        {
            
            enemy = Hull[Random.Range(0, Hull.Length)].CreateEnemyTank(transform.position + Vector3.up, transform.rotation);
            Turret[Random.Range(0, Turret.Length)].CreateEnemyGun(enemy);

            //noEnemy = false;
            availableSpawns--;

            StartCoroutine(RespawnerCD());

            enemy.GetComponent<HealthEnemy>().EnemyDestroyed += EnemyDead;

        }


    }

    private IEnumerator RespawnerCD()
    {
        CDready = false;
        yield return CD;
        CDready = true;

    }

    void EnemyDead()
    {
        noEnemy = true;

    }



}
//public class SpawnScr : MonoBehaviour
//{
//    public GameObject Hull;
//    public GameObject Turret;
//    public float SpawnCD;
//    public float detectionRange;
//    public bool CDready;
//    public bool noEnemy;
//    GameObject enemy;


//    bool enemyAlive;
//    WaitForSeconds CD;

//    public GameObject player;

//    Ray lineOfSight;

//    public LayerMask playerMask;


//    void Start()
//    {

//        CDready = true;
//        noEnemy = true;
//        CD = new WaitForSeconds(SpawnCD);

//        player = Player.PlayerHull;
//        playerMask = LayerMask.GetMask("PlayerTank");
//    }


//    void Update()
//    {
//        lineOfSight = new Ray(transform.position + transform.up, player.transform.position - transform.position);
//        Debug.DrawRay(transform.position + transform.up, player.transform.position - transform.position, Color.green);

//        //Debug.Log(hit.collider);

//        Physics.Raycast(lineOfSight, out RaycastHit hit0, detectionRange, playerMask);
//        Debug.Log(hit0.collider);
//        if (CDready & noEnemy & Physics.Raycast(lineOfSight, out RaycastHit hit, detectionRange, playerMask))
//        {
//            enemy = SpawnEnemy(Hull, Turret);
//            enemy.GetComponent<HealthEnemy>().EnemyDestroyed += EnemyDead;

//        }


//    }

//    private IEnumerator RespawnerCD()
//    {

//        yield return CD;
//        CDready = true;       


//    }
//    void EnemyDead()
//    {
//        noEnemy = true;

//    }

//    GameObject SpawnEnemy(GameObject hull, GameObject turret)
//    {

//        GameObject spwndHull = Instantiate(hull, transform.position + Vector3.up, transform.rotation);
//        GameObject spwndTurr = Instantiate(turret, spwndHull.transform);

//        spwndTurr.GetComponent<HingeJoint>().connectedBody = spwndHull.transform.Find("mount").GetComponent<Rigidbody>();

//        spwndHull.GetComponent<AIMove>().player = Player.PlayerHull;
//        spwndHull.GetComponent<AIMove>().patrolPoint = transform.position + transform.forward * 15;
//        //spwndHull.GetComponent<BotMove>().goal = transform.position + transform.forward * 5;

//       
//        noEnemy = false;

//        StartCoroutine(RespawnerCD());
//        return spwndHull;


//    }
//}
