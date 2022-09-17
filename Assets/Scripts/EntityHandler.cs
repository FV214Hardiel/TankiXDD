using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityHandler : MonoBehaviour
{
    public TankHull tankCard;
    public TankTurret turretCard;
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
        
        hitMarker = GameObject.Find("HitSFX").GetComponent<AudioSource>();        

        //Mesh Renderers
        meshRenderers = new();        
        meshRenderers.Add(GetComponent<MeshRenderer>());

        abilities = new();        

        effh = gameObject.AddComponent<EffectsHandler>();       


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

    }

    //Setting some specific values for Player Tank
    public void PlayerTankSetup()
    {
        
        isPlayer = true;
        gameObject.layer = LayerMask.NameToLayer("PlayerTeamGreen");
        
        
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

