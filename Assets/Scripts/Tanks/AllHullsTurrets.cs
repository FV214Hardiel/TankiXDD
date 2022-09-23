using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "List", menuName = "Tanks/List")]
public class AllHullsTurrets : ScriptableObject
{
   
    public TankHull[] allHulls;
    public TankTurret[] allTurrets;

    public TankHull[] unlockedHulls;
    public TankTurret[] unlockedTurrets;

    public static GameObject CreatePlayerTank(Vector3 spawnPosition, Quaternion spawnRotation, TankHull chosenHull, byte hullTier, 
        TankTurret chosenTurret, byte turretTier)
    {
        GameObject tunk = Instantiate(chosenHull.prefabOfHull);
        tunk.name = "PlayerHull";
        tunk.transform.SetPositionAndRotation(spawnPosition, spawnRotation);

        //Adding EH
        EntityHandler eh = tunk.AddComponent<EntityHandler>();
        eh.hullCard = chosenHull;
        eh.hullMod = chosenHull.modifications[hullTier];
        

        //Enabling Shield and Health
        //tunk.GetComponent<Shield>().enabled = true;
        tunk.AddComponent<PlayerShield>();
        tunk.GetComponent<HealthPlayer>().enabled = true;

        //Enabling Movement
        tunk.GetComponent<PlayerMove>().enabled = true;

        //Enabling Abilities        
        tunk.AddComponent<AbilityHandler>();

        tunk.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

        Destroy(tunk.GetComponent<AIMove>());
        Destroy(tunk.GetComponent<HealthEnemy>());
        Destroy(tunk.GetComponent<NavMeshAgent>());
        Destroy(tunk.transform.Find("enemyHealthBar").gameObject);

        Player.instance.ChangePlayerHull(tunk);

        //Creating two GOs from prefabs
        GameObject turret = Instantiate(chosenTurret.prefabOfTurret);
        GameObject gun = turret.GetComponentInChildren<Gun>().gameObject;

        turret.transform.SetParent(tunk.transform); //Making created hull parent to turret             
        turret.transform.position = tunk.transform.Find("mount").position; //Mounting turret to hull
        turret.transform.rotation = tunk.transform.rotation;

        //Handling the Handler, putting turret meshes to mesh list 
        eh.meshRenderers.Add(turret.GetComponent<MeshRenderer>());
        eh.meshRenderers.Add(gun.GetComponent<MeshRenderer>());


        //Enabling Player scripts and deleting AI scripts
        turret.GetComponentInChildren<Rotation>().enabled = true;

        gun.GetComponentInChildren<GunTarget>().enabled = true;
        gun.GetComponentInChildren<PlayerShooting>().enabled = true;
        Destroy(gun.GetComponentInChildren<AIShooting>());


        //Putting stats card in EH
        eh.turretCard = chosenTurret;
        eh.turretMod = chosenTurret.modifications[turretTier];

        eh.PlayerTankSetup();

        //Making record to Player class about changed turret
        Player.instance.ChangePlayerTurret(turret);

        return tunk;
    }

    public static GameObject CreateEnemyTank(Vector3 spawnPosition, Quaternion spawnRotation, TankHull chosenHull, byte hullTier, 
        TankTurret chosenTurret, byte turretTier)
    {
        GameObject tunk = Instantiate(chosenHull.prefabOfHull); //instantiate ������

        tunk.transform.SetPositionAndRotation(spawnPosition, spawnRotation);

        //Adding EH
        EntityHandler eh = tunk.AddComponent<EntityHandler>();
        eh.hullCard = chosenHull;
        eh.hullMod = chosenHull.modifications[hullTier];
        eh.AITankSetup();


        //Enabling Shield and Health
        tunk.AddComponent<AIShield>();
        tunk.GetComponent<HealthEnemy>().enabled = true;

        //Enabling Movement
        tunk.GetComponent<AIMove>().enabled = true;
        tunk.GetComponent<NavMeshAgent>().enabled = true;

        //Destroying Player scripts
        Destroy(tunk.GetComponent<PlayerMove>());
        Destroy(tunk.GetComponent<HealthPlayer>());
        Destroy(tunk.GetComponent<AbilityHandler>());

        //Creating two GOs from prefabs
        GameObject turret = Instantiate(chosenTurret.prefabOfTurret);
        GameObject gun = turret.GetComponentInChildren<Gun>().gameObject;

        turret.transform.SetParent(tunk.transform); //Making created hull parent to turret       
        turret.transform.position = tunk.transform.Find("mount").position; //Mounting turret to hull
        turret.transform.rotation = tunk.transform.rotation;


        //Handling the Handler, putting turret meshes to mesh list        
        eh.meshRenderers.Add(turret.GetComponent<MeshRenderer>());
        eh.meshRenderers.Add(gun.GetComponent<MeshRenderer>());


        //Enabling AI scripts and deleting Player scripts
        Destroy(turret.GetComponentInChildren<Rotation>());
        Destroy(gun.GetComponentInChildren<GunTarget>());
        Destroy(gun.GetComponentInChildren<PlayerShooting>());
        gun.GetComponentInChildren<AIShooting>().enabled = true;


        //Putting stats card in EH
        eh.turretCard = chosenTurret;
        eh.turretMod = chosenTurret.modifications[turretTier];

        return tunk;
    }

    public static GameObject CreateDecorative(Transform parent, TankHull chosenHull, byte hullTier, TankTurret chosenTurret, byte turretTier, Texture2D skin)
    {
        GameObject tunk = Instantiate(chosenHull.prefabOfHull);
        tunk.transform.SetParent(parent);
        //tunk.transform.SetPositionAndRotation(parent.position, Quaternion.Euler(parent.rotation.eulerAngles.x, parent.rotation.eulerAngles.y, parent.rotation.eulerAngles.z));
        tunk.transform.SetPositionAndRotation(parent.position, parent.rotation);
        tunk.transform.localEulerAngles = new Vector3(0, 90, 0);       
        tunk.GetComponent<Rigidbody>().useGravity = false;

        GameObject turret = Instantiate(chosenTurret.prefabOfTurret);
        GameObject gun = turret.GetComponentInChildren<Gun>().gameObject;

        turret.transform.SetParent(tunk.transform); //Making created hull parent to turret       
        turret.transform.position = tunk.transform.Find("mount").position; //Mounting turret to hull
        turret.transform.rotation = tunk.transform.rotation;

        tunk.transform.localScale = 60 * Vector3.one;
        tunk.AddComponent<DecorativeRotation>().rotationSpeed = 50;

        EntityHandler eh = tunk.AddComponent<EntityHandler>();
        eh.meshRenderers.Add(turret.GetComponent<MeshRenderer>());
        eh.meshRenderers.Add(gun.GetComponent<MeshRenderer>());
        eh.hullCard = chosenHull;
        eh.hullMod = chosenHull.modifications[hullTier];
        eh.turretCard = chosenTurret;
        eh.turretMod = chosenTurret.modifications[turretTier];
        eh.skinTexture = skin;
        
        eh.DecorativeSetup();


        return tunk;

    }

}
