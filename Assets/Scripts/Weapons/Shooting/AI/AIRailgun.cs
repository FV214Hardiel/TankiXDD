using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIRailgun : AIShooting
{

    LineRenderer laserLine;
    public GameObject shotLight;
    //public Light shotLight;
    Color c;
    public Color cm;

    public float shotDurF;
    WaitForSeconds shotDuration;

    public float chargDurF;
    WaitForSeconds chargeDuration;

    public float weapRange;
    public float Damage;

    public float reloadTime;
    float nextShot;

    Transform Muzzle;
    AIMove hull;

    Ray lineOfFire;
    RaycastHit hit;

    
    void Start()
    {
        nextShot = Time.time;        
        hull = gameObject.GetComponentInParent<AIMove>();
        Muzzle = transform.Find("muzzle");
        shotDuration = new WaitForSeconds(shotDurF / 10f);
        chargeDuration = new WaitForSeconds(chargDurF / 10f);

        
        //Muzzle.gameObject.AddComponent<LineRenderer>();
        laserLine = Muzzle.gameObject.GetComponent<LineRenderer>();
        laserLine.startWidth = 0.1f;


        c = laserLine.material.color;        
        laserLine.material.SetColor("_EmissionColor", cm);
        c = cm;
        shotLight.GetComponent<Light>().color = cm;
    }

    
    void Update()
    {
        if (hull.AIState == AIMove.AIEnum.Attack)
        {
            lineOfFire = new Ray(Muzzle.position, Muzzle.forward);
            //Debug.DrawRay(Muzzle.position, Muzzle.forward);

            Physics.Raycast(lineOfFire, out hit);
            if ((hit.collider == Player.PlayerHullColl || hit.collider == Player.PlayerTurretColl) && Time.time > nextShot)
            {

                nextShot = Time.time + reloadTime + chargDurF;
                StartCoroutine(ShotEffect());
            }
        }
    }

    private IEnumerator ShotEffect()
    {
       

        GameObject shl = Instantiate(shotLight, Muzzle);
        Light intens = shl.GetComponent<Light>();
        intens.intensity = 0f;
        for (float lI = 0; lI < 9; lI += 1)
        {
            intens.intensity += 0.3f;
            yield return chargeDuration;
        }

        laserLine.SetPosition(0, shl.transform.position);        

        if (Physics.Raycast(Muzzle.position, Muzzle.forward, out RaycastHit hit, weapRange))
        {
            //Debug.Log(hit.collider);
            laserLine.SetPosition(1, hit.point);

            EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>();
            if (eh != null)
            {
                eh.DealDamage(Damage, source);
            }

            //Health health = hit.collider.GetComponentInParent<Health>();
            //if (health != null)
            //{
            //    health.TakingDMG(Damage, source);
            //}

        }
        else
        {
            Debug.Log("ne popal");
            laserLine.SetPosition(1, Muzzle.transform.position + weapRange * Muzzle.forward);

        }
        laserLine.enabled = true;
        c.a = 1;
        laserLine.material.color = c;        
        
        for (float lI = 0; lI < 9; lI += 1)
        {

            intens.intensity -= 0.3f;
            c.a -= 0.1f;
            laserLine.material.color = c;
            yield return shotDuration;

        }
       
        laserLine.enabled = false;
        Destroy(shl);


    }
}
