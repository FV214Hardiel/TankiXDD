using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmoky : PlayerShooting
{ //public Light shotLight;

    public float weapRange;
    

    public float reloadTime;
    float remCD;

    
    

    float wait;
    public GameObject expPref;
    public GameObject[] ShotEff;
    AudioSource shotAudio;

    AudioSource hitAudio;

    void Start()
    {
        hitAudio = GameObject.Find("HitSFX").GetComponent<AudioSource>();
        shotAudio = GetComponent<AudioSource>();

        remCD = 0;
        
        muzzle = transform.Find("muzzle");
        wait = 0.1f;


    }

    // Update is called once per frame
    void Update()
    {   if (remCD > 0)
        {
            remCD -= Time.deltaTime;
        }

        if (!GameHandler.GameIsPaused && Input.GetButtonDown("Fire1") && remCD <= 0)
        {
            remCD = reloadTime;
            StartCoroutine(ShotEffect());
        }
    }

    IEnumerator ShotEffect()
    {
        RaycastHit hit;
        foreach (GameObject sht in ShotEff)
        {
            sht.SetActive(true);
        }
        shotAudio.Play();
        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, weapRange))
        {
            // Debug.Log(hit.collider);            

            Health health = hit.collider.GetComponentInParent<Health>();
            if (health != null)
            {
                health.TakingDMG(damage, source);
                hitAudio.Play();

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
