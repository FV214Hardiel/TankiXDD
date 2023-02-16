using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectsHandler))]
public class Building : MonoBehaviour, IEntity, IDamagable
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

    }
}
