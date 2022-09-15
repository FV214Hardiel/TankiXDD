using System;
using UnityEngine;

public class HealthPlayer : Health
{

   
    public Collider[] Debris;
    public Material BurntTankText;

    public HealthBarPlayer hb;
    
    
    MonoBehaviour[] scrs;

    Transform sounds;
    

    public static event Action playerIsDead;

    private void OnEnable()
    {
        
        baseHP = GetComponent<EntityHandler>().tankCard.baseHP; // Getting Base Health from tank card
        GetComponent<EntityHandler>().health = this;
       
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

    public override void TakingDMG(float damage, GameObject source)
    {
        takingHitSound.Play();

        HP -= damage;
        
        HP = Mathf.Clamp(HP, 0, maxHP);
        hb.UpdateBar(HP);

        if (HP == 0 && Alive)
        {
            Dying(source);            
        }

    }

    public override void OverDamage(float overdmg, GameObject source)
    {
        Debug.Log("OverdamagePlayer");

        HP -= overdmg;
        
        HP = Mathf.Clamp(HP, 0, maxHP);
        hb.UpdateBar(HP);

        if (HP == 0 && Alive)
        {
            Dying(source);
        }
    }

    public override void Dying(GameObject killer)
    {
        Destroy(Instantiate(ExpPref, transform), 9);

        //trying disable all scripts
        scrs = GetComponentsInChildren<MonoBehaviour>();
        Debug.Log(scrs);
        foreach (MonoBehaviour mono in scrs)
        {
            mono.enabled = false;
            //Destroy(mono);

        }

       

        //debris go boom
        foreach (Collider nearby in Debris)
        {
            nearby.gameObject.SetActive(true);
            nearby.TryGetComponent(out Rigidbody rb);
            rb.AddExplosionForce(55, ExplosionPoint.transform.position, 10);

        }

        //paint it black
        Hull.GetComponent<Renderer>().material = BurntTankText;
        Turret.GetComponent<Renderer>().material = BurntTankText;
        Gun.GetComponent<Renderer>().material = BurntTankText;

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

        Debug.Log("BOOM");
        enabled = false;
    }


}
