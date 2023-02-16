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
    Transform healthBarTransform;

    float takenDamageSum;

    Transform sounds;    

    Transform damagePopupPrefab;
    public CumulativeDamageNumbers popup;

    Transform mainCamera;

    public event Action EnemyDestroyed;
    void Start()
    {
        GetComponent<IEntity>().HealthScript = this; //Adding this instance to EH

        baseHP = GetComponent<EntityHandler>().hullMod.baseHP; // Getting Base Health from tank card
        maxHP = baseHP;
        HP = maxHP;
        Alive = true;


        //Health bar
        enemyHealthBar = GetComponentInChildren<Slider>(true); 
        enemyHealthBar.transform.parent.gameObject.SetActive(true);        
        enemyHealthBar.maxValue = maxHP;
        enemyHealthBar.value = HP;

        healthBarTransform = enemyHealthBar.transform;

        Hull = gameObject;
        Turret = GetComponentInChildren<Turret>().gameObject;
        Gun = GetComponentInChildren<Gun>().gameObject;

        HullRB = Hull.GetComponent<Rigidbody>();
        TurretRB = Turret.GetComponent<Rigidbody>();
        GunRB = Gun.GetComponent<Rigidbody>();

        agent = GetComponent<NavMeshAgent>();

        popup = GetComponentInChildren<CumulativeDamageNumbers>(true);

        damagePopupPrefab = Resources.Load<Transform>("DamageNumbersPopup");
        mainCamera = Camera.main.transform;
        

        ExplosionPoint = transform.Find("ExplosionPoint");

        sounds = transform.Find("Sounds");
        destructionSound = sounds.Find("DestructionSound").GetComponent<AudioSource>();
        takingHitSound = sounds.Find("TakingHitSoundHealth").GetComponent<AudioSource>();


        Debris = transform.Find("debris").gameObject.GetComponentsInChildren<Collider>(true);
    }

   

    public override void TakingDMG(Damage dmgInstance)
    {
        takingHitSound.Play();

        HP -= dmgInstance.damage;

        HP = Mathf.Clamp(HP, 0, maxHP);
        enemyHealthBar.value = HP;

        if (takenDamageSum == 0)
        {
            StartCoroutine(AccumulateDMG());

        }
        takenDamageSum += dmgInstance.damage;

        PopupAdd(dmgInstance.damage);

        //daed
        if (HP <= 0 && Alive)
        {
            Dying(dmgInstance.source);
        }
    }

    //void PopupCreate(float damage)
    //{
        
    //    if (popup == null)
    //    {
    //        popup = DamageNumbersPopup.CreateStatic(damagePopupPrefab, healthBarTransform.position + transform.up + mainCamera.right * -1, damage, Color.red);
    //        popup.transform.SetParent(gameObject.transform);
    //    }
    //    else
    //    {
    //        popup.ChangeText(damage);
    //    }
    //}

    void PopupAdd(float damage)
    {
        //print("HP cum added " + damage);

        if (popup.gameObject.activeSelf == false)
        {
            popup.gameObject.SetActive(true);
        }
        popup.AddValue(damage);
        
    }

    //Coroutine for displaying damage
    //Needed for multiple instances of damage in one frame (shotguean for example)
    System.Collections.IEnumerator AccumulateDMG()
    {
        yield return new WaitForEndOfFrame(); //Waiting for end of frame

        DamageNumbersPopup.CreateStatic(damagePopupPrefab, healthBarTransform, takenDamageSum, Color.red);
        takenDamageSum = 0; //zeroing damage for next frame

    }

    public override float Heal(float amount, IEntity source)
    {
        if (HP >= maxHP) //No heal above full HP
        {
            return 0;
        }

        HP += amount;

        //DamageNumbersPopup.Create(damagePopupPrefab, transform.position + Vector3.up * 2, mainCamera.right, amount, Color.green);

        if (HP > maxHP)
        {

            amount -= (maxHP - HP);
            HP = maxHP;
        }        

        
        enemyHealthBar.value = HP;
        
        return amount;

    }

    //Method for excessive shield damage
    public override void OverDamage(Damage dmgInstance)
    {
       // print("overdmg");
       
        HP -= dmgInstance.damage;

        HP = Mathf.Clamp(HP, 0, maxHP);
        enemyHealthBar.value = HP;

        PopupAdd(dmgInstance.damage);

        if (HP <= 0 && Alive)
        {
            Dying(dmgInstance.source);
        }
    }

    public override void Dying(IEntity killer)
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
        if (killer == Player.PlayerEntity)
        {
            //print("player killed an enemy");
        }
        EnemyDestroyed?.Invoke(); //If anyone is interested

        Destroy(gameObject, 8); //Destroying corpse after some time
        enabled = false;
    }

    
}
