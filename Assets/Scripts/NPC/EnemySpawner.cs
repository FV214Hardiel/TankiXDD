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
        if (!CDready) return; 

        dist = Vector3.Distance(transform.position, player.transform.position);
        if ((dist < detectionRange || spawnWithoutPlayerNear) & availableSpawns > 0)
        {           
            enemy = AllHullsTurrets.CreateEnemyTank(transform.position, transform.rotation, Hull[Random.Range(0, Hull.Length)], 0, Turret[Random.Range(0, Turret.Length)], 0);
            
            availableSpawns--;

            StartCoroutine(RespawnerCD());

            //enemy.GetComponent<HealthEnemy>().EnemyDestroyed += EnemyDead;
        }

    }

    private IEnumerator RespawnerCD()
    {
        CDready = false;
        yield return CD;
        CDready = true;
    }

    //void EnemyDead()
    //{
    //    noEnemy = true;
    //}


}
