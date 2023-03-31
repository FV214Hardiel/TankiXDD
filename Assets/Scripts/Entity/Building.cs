using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(EffectsHandler))]
public class Building : MonoBehaviour, IEntity, IDamagable, IDestructible
{
    public Health HealthScript { get { return health; } set { health = value; } }
    public Shield ShieldScript { get { return shield; } set { shield = value; } }
    public GameObject Gameobject { get { return gameObject; } }

    public IEntity Entity { get { return this; } }
    public bool IsDead {get {return !health.Alive; } set { health.Alive = !value; } }
    public bool IsStatusAffectable { get { return effh.isStatusAffectable; } }

   // bool isAffectable = false;

    public EffectsHandler EffH { get { return effh; } set { effh = value; } }

    public LayerMask EnemiesMasks { get { return enemiesMask; } set { enemiesMask = value; } }
    public LayerMask FriendlyMasks { get { return friendsMask; } set { friendsMask = value; } }

    [SerializeField]
    Health health;
    Shield shield;

    EffectsHandler effh;

    LayerMask friendsMask;
    LayerMask enemiesMask;

    public string team;

    public event System.Action EntityStunned;
    public event System.Action EntityAwaken;

    public bool isPlayer { get { return false; }}
    public bool isObjective;
    public static event System.Action objectiveBuildingDestroyed;

    public ReceivingDamageEffects PropertyReceivingDamageEffects { get { return health.receivingDamageEffects; } set { health.receivingDamageEffects = value; } }

    private void Start()
    {
        effh = GetComponent<EffectsHandler>();

        SetLayermasks();
        //gameObject.layer = LayerMask.NameToLayer(team);
        foreach (Collider item in GetComponentsInChildren<Collider>())
        {
            item.gameObject.layer = LayerMask.NameToLayer(team);
        }

        health = GetComponent<Health>();
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

    public void DealDamage(Damage dmgInstance)
    {
        Damage newDmgInstance = dmgInstance; //Applying defence buffs and debuffs
        if (PropertyReceivingDamageEffects != null)
        {
            foreach (ReceivingDamageEffects item in PropertyReceivingDamageEffects.GetInvocationList())
            {
                newDmgInstance.damage = item.Invoke(newDmgInstance.damage);
            }
        }

        health.TakingDMG(newDmgInstance);

        if (dmgInstance.source == Player.PlayerEntity) //Playing hit sound only for player
        {

            UIHitmarkerScript.instance.CreateHitmarker();

        }
    }
    public void DealEMP(Damage dmgInstance)
    {
        //TakingDMG(damage / 10, entity);
    }

    public void DealAOE(Damage dmgInstance)
    {
        //EntityStunned?.Invoke();
        //EntityAwaken?.Invoke();
    }

    GameObject aliveMeshes;
    GameObject deadMeshes;

    Collider[] debris;
    Rigidbody rb;

    public void Die(IEntity killer)
    {
        aliveMeshes = GetComponentInChildren<BuildingOk>().gameObject;
        deadMeshes = GetComponentInChildren<BuildingNotOk>(true).gameObject;

        debris = deadMeshes.GetComponentsInChildren<Collider>(true);

        aliveMeshes.SetActive(false);
        deadMeshes.SetActive(true);

        //Destroy(Instantiate(ExpPref, transform), 5);

        foreach (MonoBehaviour inthere in GetComponentsInChildren<MonoBehaviour>())
        {
            Destroy(inthere);
        }

        foreach (Collider nearby in debris)
        {

            nearby.TryGetComponent(out rb);
            rb.AddExplosionForce(80, transform.position + transform.up, 40);

        }
        health.Alive = false;

        if (isObjective)
        {
            objectiveBuildingDestroyed?.Invoke();
        }
    }
}
