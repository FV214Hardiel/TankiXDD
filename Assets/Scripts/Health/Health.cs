using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate float ReceivingDamageEffects(float x);

public class Health : MonoBehaviour
{
    public float baseHP;
    public float HP;
    public float maxHP;

    
    protected GameObject Hull;
    protected GameObject Turret;
    protected GameObject Gun;

    protected Rigidbody HullRB;
    protected Rigidbody TurretRB;
    protected Rigidbody GunRB;

    public GameObject ExpPref;
    public Transform ExplosionPoint;

    protected AudioSource destructionSound;
    protected AudioSource takingHitSound;

    public bool Alive;

    public ReceivingDamageEffects receivingDamageEffects;


    public virtual void TakingDMG(float damage, IEntity source)
    {

    }

    public virtual float Heal(float amount, IEntity source)
    {
        return 0;
    }



    public virtual void Dying(IEntity killer)
    {

    }

    public virtual void OverDamage(float overdmg, IEntity source)
    {

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
