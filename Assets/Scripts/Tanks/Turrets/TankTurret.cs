using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hull", menuName = "Tanks/Turrets")]
public class TankTurret : ScriptableObject
{
    public string gunName;
    //public float dmg;

    public GameObject prefabOfTurret;

    //public float turretRotationSpeed;

    public List<TurretMod> modifications;
    
    //Creating AI turret
    //public void CreateEnemyGun(GameObject hull)
    //{
    //    //Creating two GOs from prefabs
    //    GameObject turret = Instantiate(prefabOfTurret); 
    //    GameObject gun = turret.GetComponentInChildren<Gun>().gameObject; 
        
    //    turret.transform.SetParent(hull.transform); //Making created hull parent to turret       
    //    turret.transform.position = hull.transform.Find("mount").position; //Mounting turret to hull
    //    turret.transform.rotation = hull.transform.rotation;


    //    //Handling the Handler, putting turret meshes to mesh list 
    //    EntityHandler eh = hull.GetComponent<EntityHandler>();
    //    eh.meshRenderers.Add(turret.GetComponent<MeshRenderer>());
    //    eh.meshRenderers.Add(gun.GetComponent<MeshRenderer>());


    //    //Enabling AI scripts and deleting Player scripts
    //    Destroy(turret.GetComponentInChildren<Rotation>());
    //    Destroy(gun.GetComponentInChildren<GunTarget>());
    //    Destroy(gun.GetComponentInChildren<PlayerShooting>());
    //    gun.GetComponentInChildren<AIShooting>().enabled = true;


    //    //Putting stats card in EH
    //    hull.GetComponent<EntityHandler>().turretCard = this;



    //}

    ////Creating player turret
    //public void CreatePlayerGun(GameObject hull, string playername)
    //{
    //    //Creating two GOs from prefabs
    //    GameObject turret = Instantiate(prefabOfTurret);
    //    GameObject gun = turret.GetComponentInChildren<Gun>().gameObject;

    //    turret.transform.SetParent(hull.transform); //Making created hull parent to turret             
    //    turret.transform.position = hull.transform.Find("mount").position; //Mounting turret to hull
    //    turret.transform.rotation = hull.transform.rotation;

    //    //Handling the Handler, putting turret meshes to mesh list 
    //    EntityHandler eh = hull.GetComponent<EntityHandler>();
    //    eh.meshRenderers.Add(turret.GetComponent<MeshRenderer>());
    //    eh.meshRenderers.Add(gun.GetComponent<MeshRenderer>());


    //    //Enabling Player scripts and deleting AI scripts
    //    turret.GetComponentInChildren<Rotation>().enabled = true;        

    //    gun.GetComponentInChildren<GunTarget>().enabled = true;
    //    gun.GetComponentInChildren<PlayerShooting>().enabled = true;
    //    Destroy(gun.GetComponentInChildren<AIShooting>());     
        

    //    //Putting stats card in EH
    //    hull.GetComponent<EntityHandler>().turretCard = this;


    //    //Making record to Player class about changed turret
    //    Player.instance.ChangePlayerTurret(turret);

    //}

}
