using System;
using UnityEngine;

public class HealthBuildong : Health, IDamagable
{
    public GameObject Gameobject { get { return gameObject; } }
    public bool IsDead { get { return !Alive; } set { Alive = !value; } }

    public EffectsHandler EffH { get { return gameObject.GetComponent<EffectsHandler>(); }}


    public GameObject BuildingOk;
    public GameObject BuildingNeOk;
       
    public Collider[] Debris;

    public bool isObjective;
    public static event Action objectiveBuildingDestroyed;

    AudioSource hitSound;

    Rigidbody rb;
    void Start()
    {
        maxHP = baseHP;
        HP = maxHP;
        Alive = true;

        BuildingOk = GetComponentInChildren<BuildingOk>().gameObject;
        BuildingNeOk = GetComponentInChildren<BuildingNotOk>(true).gameObject;

        hitSound = GetComponent<AudioSource>();

        Debris = BuildingNeOk.GetComponentsInChildren<Collider>(true);

    }
    public void DealDamage(float damage, IEntity source)
    {
        float newDmg = damage; //Applying defence buffs and debuffs
        if (receivingDamageEffects != null)
        {
            foreach (ReceivingDamageEffects item in receivingDamageEffects.GetInvocationList())
            {
                newDmg = item.Invoke(newDmg);
            }
        }

        TakingDMG(newDmg, source);
        
        if (source == Player.PlayerEntity) //Playing hit sound only for player
        {
            
            UIHitmarkerScript.instance.CreateHitmarker();

        }
    }
    public void DealEMP(float damage, IEntity entity)
    {
        TakingDMG(damage/10, entity);
    }
    public override void TakingDMG(float damage, IEntity source)
    {
        hitSound.Play();

        HP -= damage;
        HP = Mathf.Clamp(HP, 0, maxHP);
        if (HP <= 0 && Alive)
        {
            Dying();
            
        }
    }

    void Dying()
    {
        BuildingOk.SetActive(false);
        BuildingNeOk.SetActive(true);

        Destroy(Instantiate(ExpPref, transform), 5);

        foreach (MonoBehaviour inthere in GetComponentsInChildren<MonoBehaviour>())
        {
            Destroy(inthere);
        }

        foreach (Collider nearby in Debris)
        {

            nearby.TryGetComponent(out rb);
            rb.AddExplosionForce(80, transform.position + transform.up, 40);

        }
        Alive = false;

        if (isObjective)
        {
            objectiveBuildingDestroyed?.Invoke();
        }
    }

    

    
    
}
