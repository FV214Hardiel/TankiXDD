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
    Color baseColor;

    public Move moveScript;

    public Health health;
    public Shield shield;
    public EffectsHandler effh;

    float baseArmor;
    public float armor;

    

    public List<Component> abilities;

    public float outOfDamage;
    public bool isPlayer;

    public bool isDead;
    AudioSource hitMarker;

    public List<MeshRenderer> meshRenderers;

    void OnEnable()
    {
        baseColor = new Color(0.63f, 0.63f, 0.63f);              

        //Mesh Renderers
        meshRenderers = new();        
        meshRenderers.Add(GetComponent<MeshRenderer>());

    }

    private void Update()
    {
        
        //Out of combat timer
        outOfDamage += Time.deltaTime;

        //Debug input
        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    AddEffect(new Regeneration(10, 10, gameObject));
        //}
    }

    public void DealDamage(float dmg, GameObject source)
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
        if (source == Player.PlayerHull) //Playing hit sound only for player
        {
            hitMarker.Play();
        }
    }

    //Setting some specific values for AI Tank
    public void AITankSetup()
    {
        
        isPlayer = false;
        gameObject.layer = LayerMask.NameToLayer("EnemyTeamRed");
        effh = gameObject.AddComponent<EffectsHandler>();
        hitMarker = GameObject.Find("HitSFX").GetComponent<AudioSource>();

    }

    //Setting some specific values for Player Tank
    public void PlayerTankSetup()
    {        
        if (GameInfoSaver.instance.chosenSkin != null)
        {
            isSkin = true;
            skinTexture = GameInfoSaver.instance.chosenSkin;
            foreach (MeshRenderer item in meshRenderers)
            {
                isSkin = true;
                item.material.SetFloat("_isSkin", 1.0f);
                item.material.SetTexture("_Skin", skinTexture);
            }

        }
        else
        {
            foreach (MeshRenderer item in meshRenderers)
            {
                isSkin = false;
                item.material.SetFloat("_isSkin", 0.0f);
                item.material.SetColor("_TankColor", baseColor);

            }
            
        }
                
        isPlayer = true;
        gameObject.layer = LayerMask.NameToLayer("PlayerTeamGreen");
        abilities = new();
        effh = gameObject.AddComponent<EffectsHandler>();


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
            item.material.SetColor("_TankColor", baseColor);
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

