using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate float ReceivingDamageEffects(float x);

public class Health : MonoBehaviour
{
    IEntity entity;

    public float baseHP;
    public float HP;
    public float maxHP;

    Transform barsTransform;

    IHealthBar hb;

    float takenDamageSum;

    Transform sounds;

    Transform damagePopupPrefab;
    public CumulativeDamageNumbers popup;
    DamageNumbersPopup dmgPops;

    protected AudioSource takingHitSound;

    public bool Alive;

    public ReceivingDamageEffects receivingDamageEffects;

    void OnEnable()
    {
        entity = GetComponent<IEntity>();
        entity.HealthScript = this;//Adding this instance to EH
        
        dmgPops = GetComponentInChildren<DamageNumbersPopup>(true);

        //pops.Init();

        //baseHP = GetComponent<TankEntity>().hullMod.baseHP; // Getting Base Health from tank card
        baseHP = 250;
        maxHP = baseHP;
        HP = maxHP;
        Alive = true;


        if (entity.isPlayer)
        {
            hb = GameObject.Find("HealthBarUI").GetComponent<IHealthBar>();

        }
        else
        {
            //Health bar
            hb = GetComponentInChildren<IHealthBar>(true);
            barsTransform = transform.Find("floatingBars");

            popup = GetComponentInChildren<CumulativeDamageNumbers>(true);

            damagePopupPrefab = Resources.Load<Transform>("DamageNumbersPopup");
        }
        
        hb.StartBar();

        hb.ChangeMaxHP(maxHP);
        hb.UpdateBar(HP);       

        sounds = transform.Find("Sounds");
        takingHitSound = sounds.Find("TakingHitSoundHealth").GetComponent<AudioSource>();
    }

    public virtual void TakingDMG(Damage dmgInstance)
    {
        takingHitSound.Play();

        HP -= dmgInstance.damage;

        HP = Mathf.Clamp(HP, 0, maxHP);
        hb.UpdateBar(HP);

        if (!entity.isPlayer)
        {
            if (takenDamageSum == 0)
            {
                StartCoroutine(AccumulateDMG());

            }
            takenDamageSum += dmgInstance.damage;

            PopupAdd(dmgInstance.damage);
        }

        

        //daed
        if (HP <= 0 && Alive)
        {
            GetComponent<IDestructible>().Die(dmgInstance.source);
            //Dying(dmgInstance.source);
        }
    }

    public virtual float Heal(float amount, IEntity source)
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


        hb.UpdateBar(HP);

        return amount;
    }

    public virtual void OverDamage(Damage dmgInstance)
    {
        HP -= dmgInstance.damage;

        HP = Mathf.Clamp(HP, 0, maxHP);
        hb.UpdateBar(HP);

        if (!entity.isPlayer)
        {
            if (takenDamageSum == 0)
            {
                StartCoroutine(AccumulateDMG());

            }
            takenDamageSum += dmgInstance.damage;

            PopupAdd(dmgInstance.damage);
            
        }

        if (HP <= 0 && Alive)
        {
            GetComponent<IDestructible>().Die(dmgInstance.source);
            //            Dying(dmgInstance.source);
        }
    }

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

        //DamageNumbersPopup.CreateStatic(damagePopupPrefab, barsTransform, takenDamageSum, Color.red);
        print(dmgPops);
        dmgPops?.Init(takenDamageSum, Color.green);
        takenDamageSum = 0; //zeroing damage for next frame

    }

    /*
    public virtual GameObject GetHull()
    {
        return null;
    }
    public virtual void SetHull(GameObject hull)
    {
        Hull = hull;
    }

    public virtual GameObject GetTurret()
    {
        return null;
    }
    public virtual void SetTurret(GameObject turret)
    {
       Turret = turret;
    }

    public virtual GameObject GetGun()
    {
        return null;
    }
    public virtual void SetGun(GameObject gun)
    {
        Gun = gun;
    }

    */

}
