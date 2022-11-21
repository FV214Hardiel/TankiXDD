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
    public LayerMask enemyLayers;

    //Ray lineOfSight;

    //public LayerMask playerMask;
    float dist;

    Collider[] colliders;


    void Start()
    {
        //availableSpawns = 2;
        CDready = true;
        noEnemy = true;
        CD = new WaitForSeconds(SpawnCD);

        player = Player.PlayerHull;

        colliders = new Collider[1];

        StartCoroutine(CustomUpdate(1));
    }

    public IEnumerator CustomUpdate(float timeDelta)
    {
        while (true)
        {
            if (!CDready)
            {
                yield return new WaitForSeconds(timeDelta);
                //print("not ready");
                continue;
            }
            if (Physics.OverlapSphereNonAlloc(transform.position, detectionRange, colliders, enemyLayers) > 0 || spawnWithoutPlayerNear)
            {
                //print("enemy detected");
                if (availableSpawns > 0)
                {
                   // print("spawning");
                    enemy = AllHullsTurrets.CreateEnemyTank(transform.position, transform.rotation, Hull[Random.Range(0, Hull.Length)], 0, Turret[Random.Range(0, Turret.Length)], 0);

                    availableSpawns--;

                    StartCoroutine(RespawnerCD());
                }
            }

            yield return new WaitForSeconds(timeDelta);
        }
    }

    void Update()
    {
        
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
