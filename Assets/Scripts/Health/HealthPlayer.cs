using System;
using UnityEngine;

public class HealthPlayer : Health
{

   
    public Collider[] Debris;
    
    public HealthBarPlayer hb;    
    
    MonoBehaviour[] scrs;

    Transform sounds;

    Transform damagePopupPrefab;
    Camera mainCamera;

    DamageNumbersPopup popup;


    public static event Action playerIsDead;

    private void OnEnable()
    {
        damagePopupPrefab = Resources.Load<Transform>("DamageNumbersPopup");
        mainCamera = Camera.main;

        baseHP = GetComponent<EntityHandler>().hullMod.baseHP; // Getting Base Health from tank card
        GetComponent<IEntity>().HealthScript = this;
       
        maxHP = baseHP;
        HP = maxHP;
        if (HP > 0)
        {
            Alive = true;
        }
        
    }
    void Start()
    {
        
        

        hb = GameObject.Find("HealthBarUI").GetComponent<HealthBarPlayer>();

        Hull = gameObject;
        Turret = gameObject.GetComponentInChildren<Turret>().gameObject;
        Gun = gameObject.GetComponentInChildren<Gun>().gameObject;

        HullRB = Hull.GetComponent<Rigidbody>();
        TurretRB = Turret.GetComponent<Rigidbody>();
        GunRB = Gun.GetComponent<Rigidbody>();

        ExplosionPoint = transform.Find("ExplosionPoint");
        Debris = transform.Find("debris").gameObject.GetComponentsInChildren<Collider>(true);

        sounds = transform.Find("Sounds");
        destructionSound = sounds.Find("DestructionSound").GetComponent<AudioSource>();
        takingHitSound = sounds.Find("TakingHitSoundHealth").GetComponent<AudioSource>();
        //hb.UpdateBar(HP);


    }

    public override void TakingDMG(Damage dmgInstance)
    {
        takingHitSound.Play();

        HP -= dmgInstance.damage;
        
        HP = Mathf.Clamp(HP, 0, maxHP);
        hb.UpdateBar(HP);

        if (HP == 0 && Alive)
        {
            Dying(dmgInstance.source);            
        }

    }

    public override float Heal(float amount, IEntity source)
    {
       
        if (HP >= maxHP) 
        {
            return 0;
        }

        HP += amount;

        //DamageNumbersPopup.Create(damagePopupPrefab, transform.position + Vector3.up * 2, mainCamera.transform.right, amount, Color.green);

        if (HP > maxHP)
        {

            amount -= (HP - maxHP);
            print(amount);
            HP = maxHP;
        }
        
        hb.UpdateBar(HP);

        return amount;

    }

    public override void OverDamage(Damage dmgInstance)
    {
        Debug.Log("OverdamagePlayer");

        HP -= dmgInstance.damage;
        
        HP = Mathf.Clamp(HP, 0, maxHP);
        hb.UpdateBar(HP);

        if (HP == 0 && Alive)
        {
            Dying(dmgInstance.source);
        }
    }

    public override void Dying(IEntity killer)
    {
        GetComponent<EntityHandler>().Die();

        Destroy(Instantiate(ExpPref, transform), 9);

        //trying disable all scripts
        scrs = GetComponentsInChildren<MonoBehaviour>();
        
        foreach (MonoBehaviour mono in scrs)
        {
            mono.enabled = false;
            
        }
       

        //debris go boom
        foreach (Collider nearby in Debris)
        {
            nearby.gameObject.SetActive(true);
            nearby.TryGetComponent(out Rigidbody rb);
            rb.AddExplosionForce(55, ExplosionPoint.transform.position, 10);

        }

       
        //grabity and non kinematic for all rb
        HullRB.useGravity = true;
        TurretRB.useGravity = true;
        GunRB.useGravity = true;

        Gun.GetComponent<Collider>().enabled = true;

        HullRB.isKinematic = false;
        TurretRB.isKinematic = false;
        GunRB.isKinematic = false;

        //tank go boom

        HullRB.AddExplosionForce(100, ExplosionPoint.position, 5);
        TurretRB.AddExplosionForce(90, ExplosionPoint.position, 9);
        GunRB.AddExplosionForce(30, ExplosionPoint.position, 5);

        destructionSound.Play();

        Alive = false;
        playerIsDead?.Invoke();

        
        enabled = false;
    }


}
