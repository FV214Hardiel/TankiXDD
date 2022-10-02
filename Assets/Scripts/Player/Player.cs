using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public static event Action playerIsChanged;    

    public static GameObject PlayerHull;
    public static GameObject PlayerTurret;
    public static GameObject PlayerGun;

    public static Collider PlayerHullColl;
    public static Collider PlayerTurretColl;

    public static EntityHandler PlayerEH;

    public static Camera PlayerCamera;

    public static string PlayerName;
    
    void OnEnable()
    {
        //spawner
        //Debug.Log("enable");
        instance = this;
        PlayerName = GameInfoSaver.instance.enterName;

        GameObject spawnPoint = GameObject.Find("PlayerSpawnLocation");

        AllHullsTurrets.CreatePlayerTank(spawnPoint.transform.position, spawnPoint.transform.rotation, 
            GameInfoSaver.instance.chosenHull, GameInfoSaver.instance.hullTier, GameInfoSaver.instance.chosenTurret, GameInfoSaver.instance.turretTier);


    }

    public void ChangePlayerHull(GameObject hull)
    {
        
        PlayerHull = hull;
        PlayerHullColl = hull.GetComponent<Collider>();

        Cinemachine.CinemachineFreeLook flcam = GameObject.Find("ThirdPersonCamera").GetComponent<Cinemachine.CinemachineFreeLook>();
        flcam.Follow = PlayerHull.transform.Find("cameraFollow");
        flcam.LookAt = PlayerHull.transform.Find("cameraFollow");
        flcam.m_Heading.m_Bias = hull.transform.rotation.eulerAngles.y;
        flcam.m_YAxis.Value = 0.3f;

        playerIsChanged?.Invoke();
        //Debug.Log("PlayerHull changed");

    }

    public void ChangePlayerTurret(GameObject turret)
    {
        PlayerTurret = turret;
        
        PlayerTurretColl = turret.GetComponent<Collider>();
        
        PlayerGun = PlayerTurret.GetComponentInChildren<Gun>().gameObject;
        

        playerIsChanged?.Invoke();
        //Debug.Log("PlayerTurret changed");

    }






}
