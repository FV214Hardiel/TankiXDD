using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankEntity : MonoBehaviour, IDamagable, IEntity, IDestructible
{
    public Health HealthScript { get { return health; } set { health = value; } }
    public Shield ShieldScript { get { return shield; } set { shield = value; } }
    public GameObject Gameobject { get { return gameObject; } }

    public IEntity Entity { get { return this; } }

    public bool IsDead
    {
        get 
        { return isDead; }
        set
        { isDead = value; }
    }
    public bool IsStatusAffectable { get { return effh.isStatusAffectable; } }

    public EffectsHandler EffH { get { return effh; } set { effh = value; } }

    public LayerMask EnemiesMasks { get { return enemiesMask; } set { enemiesMask = value; } }
    public LayerMask FriendlyMasks { get { return friendsMask; } set { friendsMask = value; } }


    public TankHull hullCard;
    public HullMod hullMod;

    public TankTurret turretCard;
    public TurretMod turretMod;

    public string team;

    public bool isSkin;
    public SkinCard skin;

    Color basePlayerColor;
    Color baseEnemyColor;

    public Move moveScript;

    Health health;
    Shield shield;
    public EffectsHandler effh;    

    public ReceivingDamageEffects PropertyReceivingDamageEffects { get { return health.receivingDamageEffects; } set { health.receivingDamageEffects = value; } }

    float empTenacityMax;
    public float empTenacity;

    LayerMask friendsMask;
    LayerMask enemiesMask;

    public List<Component> abilities;

    public float outOfDamage;
    public bool isPlayer { get; set; }

    
    bool isDead;
    bool isStunned;

    AudioSource hitMarker;

    public List<MeshRenderer> meshRenderers;


    public event Action EntityStunned;
    public event Action EntityAwaken;
    public static event Action playerIsDead;

    Transform sounds;

    AudioSource stunSound;
    AudioSource unstunSound;

    void Awake()
    {
        empTenacityMax = 100;
        empTenacity = empTenacityMax;

        basePlayerColor = new Color(0.38f, 0.45f, 0.39f);
        
        baseEnemyColor = new Color(0.82f, 0.38f, 0.31f);              

        //Mesh Renderers
        meshRenderers = new();

        abilities = new();

        sounds = transform.Find("Sounds");
        stunSound = sounds.Find("StunnedSound").GetComponent<AudioSource>();
        unstunSound = sounds.Find("UnstunnedSound").GetComponent<AudioSource>();

        ExpPref = Resources.Load<GameObject>("tankExplosion");


        //ReceivingDamageEffects receivingDamageEffects = new();

    }

    private void Update()
    {
        
        if (empTenacity < empTenacityMax)
        {
            empTenacity += 30*Time.deltaTime;

            if (isStunned && empTenacity > 0)
            {
                UnStun();
                
            }
        }
        

        if (isStunned) return;

        //Out of combat timer
        outOfDamage += Time.deltaTime;

        //Debug input
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            print("test1");
            
            DealDamage(new Damage(307, this));
        }

        //if (Input.GetKeyUp(KeyCode.L))
        //{
        //    print("test2");
        //    effh.AddEffect(new Regeneration(4, 4));
        //}
    }



    public void DealDamage(Damage dmgInstance)
    {
        if (isDead)
        {
            return;
        }
        outOfDamage = 0; //On every attempt of dealing dmg timer resets
        
        Damage newDmgInstance = dmgInstance; //Applying defence buffs and debuffs
        if (health.receivingDamageEffects != null)
        {
            foreach (ReceivingDamageEffects item in health.receivingDamageEffects.GetInvocationList())
            {
                newDmgInstance.damage = item.Invoke(newDmgInstance.damage);
            }
        }


        if (shield.currentSP > 0)
        {
            shield.TakingDMG(newDmgInstance);
        }
        else
        {
            health.TakingDMG(newDmgInstance);
        }
        if (newDmgInstance.source == Player.PlayerEntity) //Playing hit sound only for player
        {
            //hitMarker.Play();
            UIHitmarkerScript.instance.CreateHitmarker();
            LevelStatisticsManager.instance.AddValue("player_damage", newDmgInstance.damage);

        }
    }

    public void DealEMP(Damage dmgInstance)
    {
        if (isDead)
        {
            return;
        }
        outOfDamage = 0; //On every attempt of dealing dmg timer resets

        if (shield.currentSP > 0)
        {
            shield.TakingEMP(dmgInstance);
        }
        else
        {
            LoseEMPTenacity(dmgInstance);
        }
        

        if (dmgInstance.source == Player.PlayerEntity) //Playing hit sound only for player
        {
            hitMarker.Play();
        }
    }

    public void DealAOE(Damage dmgInstance)
    {
        if (isDead)
        {
            return;
        }
        outOfDamage = 0; //On every attempt of dealing dmg timer resets

        Damage newDmgInstance = dmgInstance; //Applying defence buffs and debuffs
        if (health.receivingDamageEffects != null)
        {
            foreach (ReceivingDamageEffects item in health.receivingDamageEffects.GetInvocationList())
            {
                newDmgInstance.damage = item.Invoke(newDmgInstance.damage);
            }
        }


        if (shield.currentSP > 0)
        {
            shield.TakingDMG(newDmgInstance);
        }
        else
        {
            health.TakingDMG(newDmgInstance);
        }
        if (newDmgInstance.source == Player.PlayerEntity) //Playing hit sound only for player
        {
            //hitMarker.Play();
            UIHitmarkerScript.instance.CreateHitmarker();
            LevelStatisticsManager.instance.AddValue("player_damage", newDmgInstance.damage);

        }
    }
    public void LoseEMPTenacity(Damage dmgInstance)
    {
        empTenacity -= dmgInstance.damage;
        if (empTenacity < 0 && !isStunned)
        {
            Stun();
        }

    }

    void Stun()
    {
        isStunned = true;
        stunSound.Play();

        EntityStunned?.Invoke();

        foreach (MeshRenderer item in meshRenderers) //Changing shader
        {
            item.material.SetFloat("_isStunned", 1.0f);
        }
        

    }

    void UnStun()
    {
        EntityAwaken?.Invoke();

        isStunned = false;
        unstunSound.Play();

        foreach (MeshRenderer item in meshRenderers) //Changing shader
        {
            item.material.SetFloat("_isStunned", 0.0f);
        }
    }

    //Setting some specific values for AI Tank
    public void AITankSetup()
    {        
        isPlayer = false;

        meshRenderers.Add(GetComponent<MeshRenderer>());

        SetLayermasks();

        foreach (MeshRenderer item in meshRenderers)
        {
            item.material.SetFloat("_isSkin", 0.0f);
            item.material.SetColor("_TankColor", baseEnemyColor);            
            
            item.gameObject.layer = LayerMask.NameToLayer(team);

        }
           
        effh = gameObject.AddComponent<EffectsHandler>();

        hitMarker = GameObject.Find("HitSFX").GetComponent<AudioSource>();

        //shield.EnableShieldShader();
    }

    //Setting some specific values for Player Tank
    public void PlayerTankSetup(bool isNew)
    {
        try
        {
            Destroy(transform.Find("floatingBars").gameObject);
        }
        catch (Exception e)
        {
            print(e.Message);
        }

        if (isNew)
        {            

            if (GameInfoSaver.instance.chosenSkin != null)
            {
                isSkin = true;
                skin = GameInfoSaver.instance.chosenSkin;
            }

            isPlayer = true;

            effh = gameObject.AddComponent<EffectsHandler>();

            hitMarker = GameObject.Find("HitSFX").GetComponent<AudioSource>();

            Player.PlayerEntity = this;
        }        

        meshRenderers.Add(GetComponent<MeshRenderer>());
                SetLayermasks();

        
        foreach (MeshRenderer item in meshRenderers)
        {
            if (isSkin)
            {
                item.material.SetFloat("_isSkin", 1.0f);
                item.material.SetTexture("_Skin", skin.skinTexture);
            }
            else
            {
                item.material.SetFloat("_isSkin", 0.0f);
                item.material.SetColor("_TankColor", basePlayerColor);
            }

            item.gameObject.layer = LayerMask.NameToLayer(team);            

        }

    }

    //Setting some specific values for decorative tank
    public void DecorativeSetup()
    {
        try
        {
            Destroy(transform.Find("floatingBars").gameObject);
        }
        catch (Exception e) 
        {
            print(e.Message);
        }

        foreach (Transform item in GetComponentsInChildren<Transform>(true))
        {
            item.gameObject.layer = LayerMask.NameToLayer("UI");
        }
        isPlayer = false;
        isDead = true;

        meshRenderers.Add(GetComponent<MeshRenderer>());

        foreach (MeshRenderer item in meshRenderers) //Changing shader
        {
            item.material.SetColor("_TankColor", basePlayerColor);
            if (skin != null)
            {                               
                isSkin = true;
                item.material.SetFloat("_isSkin", 1.0f);
                item.material.SetTexture("_Skin", skin.skinTexture);
            }
            else
            {
                isSkin = false;
                item.material.SetFloat("_isSkin", 0.0f);                
            }
            
        }
        enabled = false;
    }

    void SetLayermasks()
    {
        friendsMask = LayerMask.GetMask(team);

        List<string> oppTeams = new();
        foreach (string item in LevelHandler.instance.teams)
        {
            if (item != team)
            {
                oppTeams.Add(item);
                oppTeams.Add("Shield" + item);
            }
        }
        enemiesMask = LayerMask.GetMask(oppTeams.ToArray());
        //print(enemiesMask.value);
    }



    

    public GameObject ExpPref;
    public event Action TankDestroyed;

    public void Die(IEntity killer)
    {
        isDead = true;
        gameObject.layer = LayerMask.NameToLayer("Dead");

        foreach (MeshRenderer item in meshRenderers) //Changing shader
        {
            item.material.SetFloat("_isDead", 1.0f);
        }

        
        GameObject turret = gameObject.GetComponentInChildren<Turret>().gameObject;
        GameObject gun = gameObject.GetComponentInChildren<Gun>().gameObject;

        Rigidbody hullRB = GetComponent<Rigidbody>();
        Rigidbody turretRB = turret.GetComponent<Rigidbody>();
        Rigidbody gunRB = gun.GetComponent<Rigidbody>();

        Transform ExplosionPoint = transform.Find("ExplosionPoint");
        Collider[] Debris = transform.Find("debris").gameObject.GetComponentsInChildren<Collider>(true); 
        

        Destroy(Instantiate(ExpPref, transform), 9);

        //trying disable all scripts
        MonoBehaviour[] scrs = GetComponentsInChildren<MonoBehaviour>();

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
        hullRB.useGravity = true;
        turretRB.useGravity = true;
        gunRB.useGravity = true;

        gun.GetComponent<Collider>().enabled = true;

        hullRB.isKinematic = false;
        turretRB.isKinematic = false;
        gunRB.isKinematic = false;

        //tank go boom

        hullRB.AddExplosionForce(100, ExplosionPoint.position, 5);
        turretRB.AddExplosionForce(90, ExplosionPoint.position, 9);
        gunRB.AddExplosionForce(30, ExplosionPoint.position, 5);

        Destroy(transform.Find("mount").gameObject);

        sounds.Find("DestructionSound").GetComponent<AudioSource>().Play();

        if (isPlayer)
        {
            playerIsDead?.Invoke();
        }
        else
        {
            GetComponent<NavMeshAgent>().enabled = false; //Disabling AI   
        }
        
        Debug.LogWarning(killer.Gameobject.name + " killed " + gameObject.name);

        TankDestroyed?.Invoke(); //If anyone is interested
        if (killer == Player.PlayerEntity) 
        { GameInfoSaver.instance.CurrencyProp.AddCurrency((ushort)(health.maxHP / 10)); }

        Destroy(gameObject, 8); //Destroying corpse after some time

    }

    
        
}

