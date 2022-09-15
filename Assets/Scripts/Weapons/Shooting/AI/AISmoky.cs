using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISmoky : AIShooting
{
    
    //public Light shotLight;
    
    public float weapRange;
    public float Damage;

    public float reloadTime;
    float nextShot;

    Transform Muzzle;
    AIMove hull;

    Ray shotLine;
    Ray lineOfFire;
    RaycastHit hit;

    float wait;
    public GameObject expPref;
    public GameObject[] ShotEff;
   
    void Start()
    {
        //team = "Red enemy";

        nextShot = Time.time;
        hull = gameObject.GetComponentInParent<AIMove>();
        Muzzle = transform.Find("muzzle");
        wait = 0.1f;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (hull.AIState == AIMove.AIEnum.Attack)
        {
            //Debug.Log("ataka");
            lineOfFire = new Ray(Muzzle.position, Muzzle.forward);

            Physics.Raycast(lineOfFire, out hit);
            if ((hit.collider == Player.PlayerHullColl || hit.collider == Player.PlayerTurretColl) && Time.time > nextShot)
            {
                //Debug.Log("nachinaem vistrel");
                nextShot = Time.time + reloadTime;
                StartCoroutine(ShotEffect());
            }
        }
    }

    IEnumerator ShotEffect()
    {
        RaycastHit hit;
        foreach (GameObject sht in ShotEff)
        {
            sht.SetActive(true);
        }
        if (Physics.Raycast(Muzzle.position, Muzzle.forward, out hit, weapRange))
        {
            // Debug.Log(hit.collider);            

            //Health health = hit.collider.GetComponentInParent<Health>();
            //if (health != null)
            //{
            //    health.TakingDMG(Damage, source);
            //}
            EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>();
            if (eh != null)
            {
                eh.DealDamage(Damage, source);
            }

            Destroy(Instantiate(expPref, hit.point, Camera.main.transform.rotation), 1);
            
        }
        yield return new WaitForSeconds(wait);
        foreach (GameObject sht in ShotEff)
        {
            sht.SetActive(false);
        }
    }
}
