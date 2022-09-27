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

    Transform sounds;
    

    Transform damagePopupPrefab;

    public event Action EnemyDestroyed;
    void Start()
    {
        GetComponent<EntityHandler>().health = this;

        baseHP = GetComponent<EntityHandler>().hullMod.baseHP; // Getting Base Health from tank card
        maxHP = baseHP;
        HP = maxHP;
        Alive = true;

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
        //moveScript = GetComponent<AIMove>();
        //shotScript = GetComponent<AIShooting>();

        Destroy(transform.Find("mount").gameObject);

        damagePopupPrefab = Resources.Load<Transform>("DamageNumbersPopup");

        ExplosionPoint = transform.Find("ExplosionPoint");

        sounds = transform.Find("Sounds");
        destructionSound = sounds.Find("DestructionSound").GetComponent<AudioSource>();
        takingHitSound = sounds.Find("TakingHitSoundHealth").GetComponent<AudioSource>();


        Debris = transform.Find("debris").gameObject.GetComponentsInChildren<Collider>(true);
    }

    
    public override void TakingDMG(float damage, GameObject source)
    {
        takingHitSound.Play();

        HP -= damage;

        HP = Mathf.Clamp(HP, 0, maxHP);
        enemyHealthBar.value = HP;

        DamageNumbersPopup.Create(damagePopupPrefab, transform.position + Vector3.up * 2, transform.right, damage, Color.red);

        if (HP <= 0 && Alive)
        {
            Dying(source);
        }
    }

    public override void OverDamage(float overdmg, GameObject source)
    {
       
        HP -= overdmg;

        HP = Mathf.Clamp(HP, 0, maxHP);
        enemyHealthBar.value = HP;

        //DamageNumbersPopup.Create(damagePopupPrefab, transform.position + Vector3.up * 2, overdmg, Color.red);

        if (HP <= 0 && Alive)
        {
            Dying(source);
        }
    }

    public override void Dying(GameObject killer)
    {

        GetComponent<EntityHandler>().Die();

        agent.enabled = false;
        
        Destroy(Instantiate(ExpPref, transform), 9);

        scrs = GetComponentsInChildren<MonoBehaviour>();
        //Debug.Log(scrs);
        foreach (MonoBehaviour mono in scrs)
        {
            mono.enabled = false;
            //Destroy(mono);

        }

        

        foreach (Collider nearby in Debris)
        {
            nearby.gameObject.SetActive(true);
            nearby.TryGetComponent(out Rigidbody rb);
            rb.AddExplosionForce(55, ExplosionPoint.transform.position, 10);

        }
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

        destructionSound.Play();

        Alive = false;


        Debug.Log(killer.name + " killed " + gameObject.name);
        EnemyDestroyed?.Invoke();

        enabled = false;
    }

    
}
