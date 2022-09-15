using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirebird : PlayerShooting
{
    public float angle;
    float ratioMultiplier;
    Vector3 vector;

    List<float> angles;
    int index;
    public GameObject prefab;

    public float delayBetweenShots;
    float remDelay;

    AudioSource shotSound;

    [SerializeField] float projectileSpeed;
    private void Start()
    {
        source = GetComponentInParent<EntityHandler>().gameObject;
        muzzle = transform.Find("muzzle");
        
        //Debug.Log(damage);

        shotSound = GetComponent<AudioSource>();
        shotSound.pitch = 0;

        angles = new List<float>();
        for (int _ = 0; _ < 20; _++)
        {
            angles.Add(Random.Range(2, 356));
        }
        //transform.position = Random.insideUnitCircle * 2;
        vector = transform.forward * 10;
        index = 0;

        //From given accuracy calculating final tan of vector
        angle = angle * Mathf.Deg2Rad;
        ratioMultiplier = Mathf.Atan(angle);


    }
    

    // Update is called once per frame
    void Update()
    {
        if (remDelay > 0)
        {
            remDelay -= Time.deltaTime;
        }

        if (!GameHandler.GameIsPaused && Input.GetButton("Fire1") && remDelay <= 0)
        {
            shotSound.pitch = 1;
            vector = muzzle.forward + Quaternion.AngleAxis(angles[index], muzzle.forward) * muzzle.up * ratioMultiplier;
            vector = vector.normalized * projectileSpeed;

            index++;
            if (index >= angles.Count) index = 0;
            FirebirdShot.CreateShot(prefab, muzzle.position, vector, source, damage);
            
            remDelay = delayBetweenShots;
            //Debug.Log("Firebird shot: " + remDelay.ToString());


        }

        if (!GameHandler.GameIsPaused && !Input.GetButton("Fire1"))
        {
            shotSound.pitch = 0;
        }
    }
}
