using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IDamagable
{
    GameObject Gameobject { get; }
    bool IsDead { get; set; }
    void DealDamage(float damage, IEntity entity);
    void DealEMP(float damage, IEntity entity);
    EffectsHandler EffH { get;}

    

}


public interface IEntity
{
    GameObject Gameobject { get; }

    Health HealthScript { get; set; }
    Shield ShieldScript { get; set; }

    bool IsDead { get; set; }

    EffectsHandler EffH { get; set; }

    LayerMask EnemiesMasks { get; set; }
    LayerMask FriendlyMasks { get; set; }


    ReceivingDamageEffects PropertyReceivingDamageEffects { get; set; }






}

public class EntityHandler : MonoBehaviour, IDamagable, IEntity
{
    public Health HealthScript { get { return health; } set { health = value; } }
    public Shield ShieldScript { get { return shield; } set { shield = value; } }
    public GameObject Gameobject { get { return gameObject; } }

    public bool IsDead
    {
        get { return isDead; }
        set
        { isDead = value; }
    }

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
    public bool isPlayer;

    
    bool isDead;
    bool isStunned;

    AudioSource hitMarker;

    public List<MeshRenderer> meshRenderers;

    public event System.Action TankStunned;
    public event System.Action TankAwaken;

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
        meshRenderers.Add(GetComponent<MeshRenderer>());

        Transform sounds = transform.Find("Sounds");
        stunSound = sounds.Find("StunnedSound").GetComponent<AudioSource>();
        unstunSound = sounds.Find("UnstunnedSound").GetComponent<AudioSource>();


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
            
            DealDamage(307, this);
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            print("test2");
            effh.AddEffect(new Regeneration(4, 4));
        }
    }



    public void DealDamage(float dmg, IEntity source)
    {
        if (isDead)
        {
            return;
        }
        outOfDamage = 0; //On every attempt of dealing dmg timer resets
        
        float newDmg = dmg; //Applying defence buffs and debuffs
        if (health.receivingDamageEffects != null)
        {
            foreach (ReceivingDamageEffects item in health.receivingDamageEffects.GetInvocationList())
            {
                newDmg = item.Invoke(newDmg);
            }
        }


        if (shield.currentSP > 0)
        {
            shield.TakingDMG(newDmg, source);
        }
        else
        {
            health.TakingDMG(newDmg, source);
        }
        if (source == Player.PlayerEntity) //Playing hit sound only for player
        {
            //hitMarker.Play();
            UIHitmarkerScript.instance.CreateHitmarker();
            LevelStatisticsManager.instance.AddValue("player_damage", newDmg);

        }
    }

    public void DealEMP(float dmg, IEntity source)
    {
        if (isDead)
        {
            return;
        }
        outOfDamage = 0; //On every attempt of dealing dmg timer resets

        if (shield.currentSP > 0)
        {
            shield.TakingEMP(dmg, source);
        }
        else
        {
            LoseEMPTenacity(dmg, source);
        }
        

        if (source == Player.PlayerEntity) //Playing hit sound only for player
        {
            hitMarker.Play();
        }
    }

    public void LoseEMPTenacity(float dmg, IEntity source)
    {
        empTenacity -= dmg;
        if (empTenacity < 0 && !isStunned)
        {
            Stun();
        }

    }

    void Stun()
    {
        isStunned = true;
        stunSound.Play();

        TankStunned?.Invoke();

        foreach (MeshRenderer item in meshRenderers) //Changing shader
        {
            item.material.SetFloat("_isStunned", 1.0f);
        }
        

    }

    void UnStun()
    {
        TankAwaken?.Invoke();

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

        SetLayermasks();

        foreach (MeshRenderer item in meshRenderers)
        {
            item.material.SetFloat("_isSkin", 0.0f);
            item.material.SetColor("_TankColor", baseEnemyColor);            
            
            item.gameObject.layer = LayerMask.NameToLayer(team);

        }

           
        effh = gameObject.AddComponent<EffectsHandler>();

        hitMarker = GameObject.Find("HitSFX").GetComponent<AudioSource>();
    }

    //Setting some specific values for Player Tank
    public void PlayerTankSetup()
    {
        isPlayer = true;

        SetLayermasks();

        if (GameInfoSaver.instance.chosenSkin != null)
        {
            isSkin = true;
            skin = GameInfoSaver.instance.chosenSkin;
        }

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
        

        abilities = new();
        effh = gameObject.AddComponent<EffectsHandler>();

        hitMarker = GameObject.Find("HitSFX").GetComponent<AudioSource>();

        Player.PlayerEntity = this;


    }

    //Setting some specific values for decorative tank
    public void DecorativeSetup()
    {
        foreach (Transform item in GetComponentsInChildren<Transform>(true))
        {
            item.gameObject.layer = LayerMask.NameToLayer("UI");
        }
        isPlayer = false;
        isDead = true;

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
            }
        }
        enemiesMask = LayerMask.GetMask(oppTeams.ToArray());
    }
    public void Die()
    {
        isDead = true;
        gameObject.layer = LayerMask.NameToLayer("Dead");

        foreach (MeshRenderer item in meshRenderers) //Changing shader
        {
            item.material.SetFloat("_isDead", 1.0f);
        }
    }

    
        
}

