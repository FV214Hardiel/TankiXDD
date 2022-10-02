using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityHandler : MonoBehaviour
{
    public TankHull hullCard;
    public HullMod hullMod;

    public TankTurret turretCard;
    public TurretMod turretMod;

    public bool isSkin;
    public Texture2D skinTexture;

    Color basePlayerColor;
    Color baseEnemyColor;


    public Move moveScript;

    public Health health;
    public Shield shield;
    public EffectsHandler effh;

    //float baseArmor;
    public float armor;

    float empTenaciryMax;
    public float empTenacity;

    public LayerMask friendsMask;
    public LayerMask enemiesMask;

    public List<Component> abilities;

    public float outOfDamage;
    public bool isPlayer;

    public bool isDead;
    AudioSource hitMarker;

    public List<MeshRenderer> meshRenderers;

    void OnEnable()
    {
        empTenaciryMax = 100;
        empTenacity = empTenaciryMax;

        basePlayerColor = new Color(0.38f, 0.45f, 0.39f);
        
        baseEnemyColor = new Color(0.82f, 0.38f, 0.31f);              

        //Mesh Renderers
        meshRenderers = new();        
        meshRenderers.Add(GetComponent<MeshRenderer>());

        


    }

    private void Update()
    {
        
        //Out of combat timer
        outOfDamage += Time.deltaTime;
        if (empTenacity < empTenaciryMax)
        {
            empTenacity += Time.deltaTime;

        }
        

        //Debug input
        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    AddEffect(new Regeneration(10, 10, gameObject));
        //}
    }

    public void DealDamage(float dmg, EntityHandler source)
    {
        if (isDead)
        {
            return;
        }
        outOfDamage = 0; //On every attempt of dealing dmg timer resets
        if (shield.currentSP > 0)
        {
            shield.TakingDMG(dmg, source);
        }
        else
        {
            health.TakingDMG(dmg, source);
        }
        if (source == Player.PlayerEH) //Playing hit sound only for player
        {
            hitMarker.Play();
        }
    }

    public void DealEMP(float dmg, EntityHandler source)
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
        

        if (source == Player.PlayerEH) //Playing hit sound only for player
        {
            hitMarker.Play();
        }
    }

    public void LoseEMPTenacity(float dmg, EntityHandler source)
    {
        empTenacity -= dmg;
        if (empTenacity < 0)
        {
            print("Tank is stunned by " + source.name);
        }

    }

    //Setting some specific values for AI Tank
    public void AITankSetup()
    {        
        isPlayer = false;
        friendsMask = LayerMask.GetMask("EnemyTeamRed");
        enemiesMask = LayerMask.GetMask("PlayerTeamGreen");

        foreach (MeshRenderer item in meshRenderers)
        {
            item.material.SetFloat("_isSkin", 0.0f);
            item.material.SetColor("_TankColor", baseEnemyColor);            
            
            item.gameObject.layer = LayerMask.NameToLayer("EnemyTeamRed");

        }

           
        effh = gameObject.AddComponent<EffectsHandler>();

        hitMarker = GameObject.Find("HitSFX").GetComponent<AudioSource>();
    }

    //Setting some specific values for Player Tank
    public void PlayerTankSetup()
    {
        isPlayer = true;

        friendsMask = LayerMask.GetMask("PlayerTeamGreen");
        enemiesMask = LayerMask.GetMask("EnemyTeamRed");

        if (GameInfoSaver.instance.chosenSkin != null)
        {
            isSkin = true;
            skinTexture = GameInfoSaver.instance.chosenSkin;
        }

        foreach (MeshRenderer item in meshRenderers)
        {
            if (isSkin)
            {
                item.material.SetFloat("_isSkin", 1.0f);
                item.material.SetTexture("_Skin", skinTexture);
            }
            else
            {
                item.material.SetFloat("_isSkin", 0.0f);
                item.material.SetColor("_TankColor", basePlayerColor);
            }

            item.gameObject.layer = LayerMask.NameToLayer("PlayerTeamGreen");

        }
        print(gameObject.layer);

        abilities = new();
        effh = gameObject.AddComponent<EffectsHandler>();

        hitMarker = GameObject.Find("HitSFX").GetComponent<AudioSource>();

        Player.PlayerEH = this;


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
            if (skinTexture != null)
            {                               
                isSkin = true;
                item.material.SetFloat("_isSkin", 1.0f);
                item.material.SetTexture("_Skin", skinTexture);
            }
            else
            {
                isSkin = false;
                item.material.SetFloat("_isSkin", 0.0f);                
            }
            
        }
        this.enabled = false;
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

