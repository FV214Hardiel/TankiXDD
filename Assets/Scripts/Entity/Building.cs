using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectsHandler))]
public class Building : MonoBehaviour, IEntity
{
    public Health HealthScript { get { return health; } set { health = value; } }
    public Shield ShieldScript { get { return shield; } set { shield = value; } }
    public GameObject Gameobject { get { return gameObject; } }
    public bool IsDead {get {return health.Alive; } set { health.Alive = value; } }

    public EffectsHandler EffH { get { return effh; } set { effh = value; } }

    public LayerMask EnemiesMasks { get { return enemiesMask; } set { enemiesMask = value; } }
    public LayerMask FriendlyMasks { get { return friendsMask; } set { friendsMask = value; } }

    Health health;
    Shield shield;

    EffectsHandler effh;

    LayerMask friendsMask;
    LayerMask enemiesMask;

    public string team;

    

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
}
