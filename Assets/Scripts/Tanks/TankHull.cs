using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Hull", menuName = "Tanks/Hulls")]
public class TankHull : ScriptableObject
{
    public string hullName;
    
    public GameObject prefabOfHull;

    public float baseHP;
    public float hullSpeed;
    public float hullRotation;

    public float baseSP;
    public float shieldRechargeRate;
    public float shieldRechargeDelay;

    public List<HullMod> modifications;


    //public GameObject CreateEnemyTank(Vector3 spawnPosition, Quaternion spawnRotation)
    //{
    //    GameObject tunk = Instantiate(prefabOfHull); //instantiate корпус

    //    tunk.transform.SetPositionAndRotation(spawnPosition, spawnRotation);

    //    //Adding EH
    //    EntityHandler eh = tunk.AddComponent<EntityHandler>();
    //    eh.hullCard = this;
    //    eh.AITankSetup();


    //    //Enabling Shield and Health
    //    tunk.AddComponent<AIShield>();
    //    tunk.GetComponent<HealthEnemy>().enabled = true;

    //    //Enabling Movement
    //    tunk.GetComponent<AIMove>().enabled = true;
    //    tunk.GetComponent<NavMeshAgent>().enabled = true;

    //    //Destroying Player scripts
    //    Destroy(tunk.GetComponent<PlayerMove>());
    //    Destroy(tunk.GetComponent<HealthPlayer>());
    //    Destroy(tunk.GetComponent<AbilityHandler>());

    //    return tunk;
    //}

    //public GameObject CreatePlayerTank(Vector3 spawnPosition, Quaternion spawnRotation)
    //{
    //    GameObject tunk = Instantiate(prefabOfHull);
    //    tunk.name = "PlayerHull";
    //    tunk.transform.SetPositionAndRotation(spawnPosition, spawnRotation);

    //    //Adding EH
    //    EntityHandler eh = tunk.AddComponent<EntityHandler>();
    //    eh.hullCard = this;
    //    eh.PlayerTankSetup();

    //    //Enabling Shield and Health
    //    //tunk.GetComponent<Shield>().enabled = true;
    //    tunk.AddComponent<PlayerShield>();
    //    tunk.GetComponent<HealthPlayer>().enabled = true;

    //    //Enabling Movement
    //    tunk.GetComponent<PlayerMove>().enabled = true;

    //    //Enabling Abilities        
    //    tunk.GetComponent<AbilityHandler>().enabled = true;


    //    tunk.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

    //    Destroy(tunk.GetComponent<AIMove>());
    //    Destroy(tunk.GetComponent<HealthEnemy>());
    //    Destroy(tunk.GetComponent<NavMeshAgent>());
    //    Destroy(tunk.transform.Find("enemyHealthBar").gameObject);



    //    Player.instance.ChangePlayerHull(tunk);



    //    return tunk;
    //}

}
