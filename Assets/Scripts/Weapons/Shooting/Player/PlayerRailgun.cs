using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRailgun : PlayerShooting
{
    LineRenderer laserLine;
    
    Light shotLight;
    public Light hitLight;
    Color c;

    public float shotDurF;
    WaitForSeconds shotDuration;

    public float chargDurF;
    WaitForSeconds chargeDuration;

    public float weapRange;
    

    public float reloadTime;
    float nextShot;

    //WaitForSeconds reloadTime;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        //laserLight = GetComponentInChildren<Light>();
        shotLight = muzzle.GetComponent<Light>();
        nextShot = Time.time;
        shotDuration = new WaitForSeconds((shotDurF-0.04f)/10f);
        chargeDuration = new WaitForSeconds(chargDurF / 10f);
        c = laserLine.material.color;
    }

    
    void Update()
    {
        if (!GameHandler.GameIsPaused && Input.GetButtonDown("Fire1") && Time.time > nextShot)
        {
            nextShot = Time.time + reloadTime + chargDurF;
            StartCoroutine(ShotEffect());

        }
    }
    
    
    private IEnumerator ShotEffect()
    {
        shotLight.enabled = true;
        shotLight.intensity = 0f;
        for (float lI = 0; lI < 9; lI += 1)
        {
            shotLight.intensity += 0.3f;
            yield return chargeDuration;
        }

        laserLine.SetPosition(0, muzzle.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, weapRange))
        {
            //Debug.Log(hit.point);
            hitLight.transform.position = hit.point;
            laserLine.SetPosition(1, hit.point);

            //Health health = hit.collider.GetComponentInParent<Health>();            
            //if (health != null)
            //{
            //    health.TakingDMG(damage, source);
            //}

            EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>();
            if (eh != null)
            {
                eh.DealDamage(damage, source);
            }
        }
        else
        {
            //Debug.Log(hit.point);
            laserLine.SetPosition(1, muzzle.position + weapRange * muzzle.forward);
            
        }
        laserLine.enabled = true;
        c.a = 1;
        laserLine.material.color = c;
        hitLight.enabled = true;
        yield return new WaitForSeconds(0.04f);
        hitLight.enabled = false;
        for (float lI = 0; lI < 9; lI += 1)
        {
            //hitLight.transform.position = hit.point;
            shotLight.intensity -= 2f;
            c.a -= 0.1f;
            laserLine.material.color = c;
            yield return shotDuration;
            
        }
        hitLight.enabled = false;
        laserLine.enabled = false;
       
        
        

    }
}

   