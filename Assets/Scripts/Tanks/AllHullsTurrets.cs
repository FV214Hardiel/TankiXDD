using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "List", menuName = "Tanks/List")]
public class AllHullsTurrets : ScriptableObject
{
   
    public List<TankHull> allHulls;
    public List<TankTurret> allTurrets;

    public List<TankHull> unlockedHulls;
    public List<TankTurret> unlockedTurrets;


    //private void OnEnable()
    //{
    //    PlayerPrefs.DeleteKey("uHulls");
    //    PlayerPrefs.DeleteKey("uTurrets");
    //}
    public void LoadHulls()
    {
        //Load hulls

        string[] uHulls; //empty string array

        if (PlayerPrefs.HasKey("uHulls") && PlayerPrefsX.GetStringArray("uHulls").Length != 0) //checking Key for existing and non-emptiness
        {
            uHulls = PlayerPrefsX.GetStringArray("uHulls"); //getting array from PlayerPrefs
            unlockedHulls = new();
            foreach (string item in uHulls) 
            {
                unlockedHulls.Add(allHulls.Find(x => x.Name == item)); //for each existing string finding and adding hull in 'unlocked' list
            }
        }
        else //if no save file then create and save
        {
            unlockedHulls = new()
            {
                allHulls[0]
            };
            SaveHulls();
        }
        
    }

    public void LoadTurrets()
    {        
        string[] uTurrets; //empty string array

        if (PlayerPrefs.HasKey("uTurrets") && PlayerPrefsX.GetStringArray("uTurrets").Length != 0)
        {
            uTurrets = PlayerPrefsX.GetStringArray("uTurrets");
            unlockedTurrets = new();
            foreach (string item in uTurrets)
            {
                unlockedTurrets.Add(allTurrets.Find(x => x.Name == item));
            }
        }
        else //if no save file then create and save
        {
            unlockedTurrets = new()
            { 
                allTurrets[0]
            };
            
            SaveTurrets();
        }
    }

    public void SaveHulls()
    {
        //Save hulls

        List<string> textNames = new(); //buffer list for storing temp values

        foreach (TankHull item in unlockedHulls)  //getting items from existing 'unlocked' list
        {
            if (item != null)
            {
                textNames.Add(item.Name); //adding items to buffer
            }

        }

        PlayerPrefsX.SetStringArray("uHulls", textNames.ToArray()); //saving buffer in PlayerPrefs
        
    }

    public void SaveTurrets()
    {
        //Save turrets

        List<string> textNames = new(); //buffer list for storing temp values

        foreach (TankTurret item in unlockedTurrets) //getting items from existing 'unlocked' list
        {
            if (item != null)
            {
                textNames.Add(item.Name); //adding items to buffer
            }

        }

        PlayerPrefsX.SetStringArray("uTurrets", textNames.ToArray()); //saving buffer in PlayerPrefs
    }


    public static GameObject CreatePlayerTank(Vector3 spawnPosition, Quaternion spawnRotation, TankHull chosenHull, byte hullTier, 
        TankTurret chosenTurret, byte turretTier, int team = 0)
    {
        GameObject tunk = Instantiate(chosenHull.prefabOfHull);
        tunk.name = "PlayerHull";
        tunk.transform.SetPositionAndRotation(spawnPosition, spawnRotation);

        DestroyImmediate(tunk.GetComponent<AIMove>());
        DestroyImmediate(tunk.GetComponent<HealthEnemy>());
        DestroyImmediate(tunk.GetComponent<NavMeshAgent>());
       // DestroyImmediate(tunk.transform.Find("enemyHealthBar").gameObject);
        

        //Adding EH
        TankEntity eh = tunk.AddComponent<TankEntity>();
        eh.hullCard = chosenHull;
        eh.hullMod = chosenHull.modifications[hullTier];
        eh.isPlayer = true;
        

        //Enabling Shield and Health
        //tunk.AddComponent<PlayerShield>();
        tunk.AddComponent<Shield>();
        tunk.AddComponent<Health>();
        //tunk.GetComponent<HealthPlayer>().enabled = true;

        //Enabling Movement
        tunk.GetComponent<PlayerMove>().enabled = true;

        //Enabling Abilities        
        tunk.AddComponent<AbilityHandler>();
        

        tunk.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;       

        Player.instance.ChangePlayerHull(tunk);

        //Creating two GOs from prefabs
        GameObject turret = Instantiate(chosenTurret.prefabOfTurret);
        GameObject gun = turret.GetComponentInChildren<Gun>().gameObject;

        Destroy(gun.GetComponentInChildren<AIShooting>());

        turret.transform.SetParent(tunk.transform); //Making created hull parent to turret             
        turret.transform.SetPositionAndRotation(tunk.transform.Find("mount").position, tunk.transform.rotation); //Mounting turret to hull

        //Handling the Handler, putting turret meshes to mesh list 
        eh.meshRenderers.Clear();
        eh.meshRenderers.Add(turret.GetComponent<MeshRenderer>());
        eh.meshRenderers.Add(gun.GetComponent<MeshRenderer>());


        //Enabling Player scripts and deleting AI scripts
        turret.GetComponentInChildren<Rotation>().enabled = true;

        gun.AddComponent<GunTarget>();
        gun.GetComponentInChildren<PlayerShooting>().enabled = true;
        


        //Putting stats card in EH
        eh.turretCard = chosenTurret;
        eh.turretMod = chosenTurret.modifications[turretTier];

        eh.team = LevelHandler.instance.teams[team];

        eh.PlayerTankSetup(true);

        //Making record to Player class about changed turret
        Player.instance.ChangePlayerTurret(turret);

        return tunk;
    }

    public static GameObject CreateEnemyTank(Vector3 spawnPosition, Quaternion spawnRotation, TankHull chosenHull, byte hullTier, 
        TankTurret chosenTurret, byte turretTier, int team = 1)
    {
        GameObject tunk = Instantiate(chosenHull.prefabOfHull); //instantiate корпус

        tunk.transform.SetPositionAndRotation(spawnPosition, spawnRotation);

        //Destroying Player scripts
        DestroyImmediate(tunk.GetComponent<PlayerMove>());
        DestroyImmediate(tunk.GetComponent<HealthPlayer>());
        DestroyImmediate(tunk.GetComponent<AbilityHandler>());

        //Adding EH
        TankEntity eh = tunk.AddComponent<TankEntity>();
        eh.hullCard = chosenHull;
        eh.hullMod = chosenHull.modifications[hullTier];
        eh.isPlayer = false;



        //Enabling Shield and Health        
        tunk.AddComponent<Shield>();
        tunk.AddComponent<Health>();
        //tunk.GetComponent<HealthEnemy>().enabled = true;

        //Enabling Movement
        tunk.GetComponent<AIMove>().enabled = true;
        tunk.GetComponent<NavMeshAgent>().enabled = true;

      

        //Creating two GOs from prefabs
        GameObject turret = Instantiate(chosenTurret.prefabOfTurret);
        GameObject gun = turret.GetComponentInChildren<Gun>().gameObject;

        //Enabling AI scripts and deleting Player scripts
        DestroyImmediate(turret.GetComponentInChildren<Rotation>());
        DestroyImmediate(gun.GetComponentInChildren<GunTarget>());
        DestroyImmediate(gun.GetComponentInChildren<PlayerShooting>());
        gun.GetComponentInChildren<AIShooting>().enabled = true;

        turret.transform.SetParent(tunk.transform); //Making created hull parent to turret       
        turret.transform.SetPositionAndRotation(tunk.transform.Find("mount").position, tunk.transform.rotation); //Mounting turret to hull


        //Handling the Handler, putting turret meshes to mesh list        
        eh.meshRenderers.Add(turret.GetComponent<MeshRenderer>());
        eh.meshRenderers.Add(gun.GetComponent<MeshRenderer>());

        //Putting stats card in EH
        eh.turretCard = chosenTurret;
        eh.turretMod = chosenTurret.modifications[turretTier];

        eh.team = LevelHandler.instance.teams[team];

        eh.AITankSetup();

        return tunk;
    }

    public static GameObject CreateDecorative(Transform parent, TankHull chosenHull, byte hullTier, TankTurret chosenTurret, byte turretTier, SkinCard skin)
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
        turret.transform.SetPositionAndRotation(tunk.transform.Find("mount").position, tunk.transform.rotation); //Mounting turret to hull

        tunk.transform.localScale = 60 * Vector3.one;
        tunk.AddComponent<DecorativeRotation>().rotationSpeed = 30;

        TankEntity eh = tunk.AddComponent<TankEntity>();
        eh.skin = skin;
        eh.meshRenderers.Add(turret.GetComponent<MeshRenderer>());
        eh.meshRenderers.Add(gun.GetComponent<MeshRenderer>());
        eh.hullCard = chosenHull;
        eh.hullMod = chosenHull.modifications[hullTier];
        eh.turretCard = chosenTurret;
        eh.turretMod = chosenTurret.modifications[turretTier];
        
        
        eh.DecorativeSetup();


        return tunk;

    }

    public static GameObject ChangePlayerTurret(Transform parentHull, TankTurret chosenTurret, byte turretTier)
    {
        Quaternion oldRot = Player.PlayerTurret.transform.rotation;
        Destroy(Player.PlayerTurret);

        GameObject turret = Instantiate(chosenTurret.prefabOfTurret);
        GameObject gun = turret.GetComponentInChildren<Gun>().gameObject;

        turret.transform.SetParent(parentHull); //Making created hull parent to turret             
        turret.transform.SetPositionAndRotation(parentHull.Find("mount").position, oldRot); //Mounting turret to hull

        TankEntity eh = parentHull.gameObject.GetComponent<TankEntity>();

        //Handling the Handler, putting turret meshes to mesh list 
        eh.meshRenderers.Clear();

        eh.meshRenderers.Add(turret.GetComponent<MeshRenderer>());
        eh.meshRenderers.Add(gun.GetComponent<MeshRenderer>());


        //Enabling Player scripts and deleting AI scripts
        turret.GetComponentInChildren<Rotation>().enabled = true;

        gun.AddComponent<GunTarget>();
        gun.GetComponentInChildren<PlayerShooting>().enabled = true;
        DestroyImmediate(gun.GetComponentInChildren<AIShooting>());


        //Putting stats card in EH
        eh.turretCard = chosenTurret;
        eh.turretMod = chosenTurret.modifications[turretTier];

        eh.PlayerTankSetup(false);

        Player.instance.ChangePlayerTurret(turret);

        return turret;

    }
}
