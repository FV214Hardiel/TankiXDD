using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthEnemy : Health
{
   
    
    public Collider[] Debris;   

    //AIMove moveScript;
    //AIShooting shotScript;
    MonoBehaviour[] scrs;
    NavMeshAgent agent;
    Slider enemyHealthBar;

    float takenDamageSum;

    Transform sounds;    

    Transform damagePopupPrefab;

    Camera mainCamera;

    public event Action EnemyDestroyed;
    void Start()
    {
        GetComponent<EntityHandler>().health = this; //Adding this instance to EH

        baseHP = GetComponent<EntityHandler>().hullMod.baseHP; // Getting Base Health from tank card
        maxHP = baseHP;
        HP = maxHP;
        Alive = true;


        //Health bar
        enemyHealthBar = GetComponentInChildren<Slider>(true); 
        enemyHealthBar.transform.parent.gameObject.SetActive(true);        
        enemyHealthBar.maxValue = maxHP;
        enemyHealthBar.value = HP;

        Hull = gameObject;
        Turret = GetComponentInChildren<Turret>().gameObject;
        Gun = GetComponentInChildren<Gun>().gameObject;

        HullRB = Hull.GetComponent<Rigidbody>();
        TurretRB = Turret.GetComponent<Rigidbody>();
        GunRB = Gun.GetComponent<Rigidbody>();

        agent = GetComponent<NavMeshAgent>();

        

        damagePopupPrefab = Resources.Load<Transform>("DamageNumbersPopup");
        mainCamera = Camera.main;

        ExplosionPoint = transform.Find("ExplosionPoint");

        sounds = transform.Find("Sounds");
        destructionSound = sounds.Find("DestructionSound").GetComponent<AudioSource>();
        takingHitSound = sounds.Find("TakingHitSoundHealth").GetComponent<AudioSource>();


        Debris = transform.Find("debris").gameObject.GetComponentsInChildren<Collider>(true);
    }

   

    public override void TakingDMG(float damage, EntityHandler source)
    {
        takingHitSound.Play();

        HP -= damage;

        HP = Mathf.Clamp(HP, 0, maxHP);
        enemyHealthBar.value = HP;

        if (takenDamageSum == 0)
        {
            StartCoroutine(AccumulateDMG());

        }
        takenDamageSum += damage;

        //daed
        if (HP <= 0 && Alive)
        {
            Dying(source);
        }
    }

    //Coroutine for displaying damage
    //Needed for multiple instances of damage in one frame (shotgun for example)
    System.Collections.IEnumerator AccumulateDMG()
    {
        yield return new WaitForEndOfFrame(); //Waiting for end of frame

        DamageNumbersPopup.Create(damagePopupPrefab, transform.position + Vector3.up * 2, mainCamera.transform.right, takenDamageSum, Color.red); 
        takenDamageSum = 0; //zeroing damage for next frame

    }

    public override void Heal(float amount, EntityHandler source)
    {
        if (HP >= maxHP) //No heal above full HP
        {
            return;
        }

        HP += amount;

        DamageNumbersPopup.Create(damagePopupPrefab, transform.position + Vector3.up * 2, mainCamera.transform.right, amount, Color.green);

        HP = Mathf.Clamp(HP, 0, maxHP);
        enemyHealthBar.value = HP;

        //daed
        if (HP == 0 && Alive)
        {
            Dying(source);
        }

    }

    //Method for excessive shield damage
    public override void OverDamage(float overdmg, EntityHandler source)
    {
       
        HP -= overdmg;

        HP = Mathf.Clamp(HP, 0, maxHP);
        enemyHealthBar.value = HP;

        DamageNumbersPopup.Create(damagePopupPrefab, transform.position + Vector3.up * 2, mainCamera.transform.right, overdmg, Color.red);

        if (HP <= 0 && Alive)
        {
            Dying(source);
        }
    }

    public override void Dying(EntityHandler killer)
    {

        GetComponent<EntityHandler>().Die(); //Some Die method from EH

        destructionSound.Play(); //VFX SFX
        Destroy(Instantiate(ExpPref, transform), 9);

        Alive = false;

        agent.enabled = false; //Disabling AI       

        //Disabling all scripts

        scrs = GetComponentsInChildren<MonoBehaviour>();       
        foreach (MonoBehaviour mono in scrs)
        {
            mono.enabled = false;
            
        }
        
        //Scattering the debris
        foreach (Collider nearby in Debris)
        {
            nearby.gameObject.SetActive(true);
            nearby.TryGetComponent(out Rigidbody rb);
            rb.AddExplosionForce(55, ExplosionPoint.transform.position, 10);

        }

        //Scattering meshes
        HullRB.useGravity = true;
        HullRB.AddExplosionForce(100, ExplosionPoint.position, 5);

        TurretRB.isKinematic = false;
        TurretRB.useGravity = true;
        TurretRB.AddExplosionForce(90, ExplosionPoint.position, 15);
        Turret.GetComponentInChildren<MonoBehaviour>().enabled = false;

        GunRB.isKinematic = false;
        GunRB.useGravity = true;
        Gun.GetComponent<Collider>().enabled = true;
        GunRB.AddExplosionForce(10, ExplosionPoint.position, 9);

        Destroy(transform.Find("mount").gameObject);


        //Debug.Log(killer.name + " killed " + gameObject.name);
        EnemyDestroyed?.Invoke(); //If anyone is interested

        Destroy(gameObject, 8); //Destroying corpse after some time
        enabled = false;
    }

    
}
