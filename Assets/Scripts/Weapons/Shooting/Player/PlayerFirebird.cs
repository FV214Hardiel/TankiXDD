using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirebird : PlayerShooting
{
    public float angle;    
    Vector3 shotVector;

    Transform muzzle;

    List<ushort> disperseAngles;
    List<float> disperseLengths;
    int index;

    public GameObject prefab;

    public float delayBetweenShots;
    float remainingDelay;

    AudioSource shotSound;

    public float weapRange;
    public float projectileSpeed;
    float timeOfLife;

    PlayerInputActions inputActions;
    float inputValue;

    private void Start()
    {
        source = GetComponentInParent<EntityHandler>().gameObject;
        muzzle = transform.Find("muzzle");

        inputActions = new();
        if (!GameHandler.GameIsPaused) inputActions.PlayerTankControl.Enable();

        timeOfLife = weapRange / projectileSpeed;

        shotSound = GetComponent<AudioSource>();
        shotSound.pitch = 0;

        disperseAngles = new();
        for (int _ = 0; _ < 50; _++)
        {
            disperseAngles.Add((ushort)Random.Range(1, 357));
        }

        disperseLengths = new();
        for (int _ = 0; _ < 50; _++)
        {
            disperseLengths.Add(Random.Range(0f, 1f));
        }

        index = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameHandler.GameIsPaused) //Checking pause
        {
            shotSound.pitch = 0;
            return;
        }

        inputValue = inputActions.PlayerTankControl.Fire.ReadValue<float>();
        
        shotSound.pitch = inputValue;

        if (remainingDelay > 0) //Decreasing delay timer          
        {
            remainingDelay -= Time.deltaTime;
            return; 
        }
                
        //Making a shot
        if (inputValue > 0)
        {
            shotVector = DisperseVector(muzzle.forward, angle) * projectileSpeed;

            FirebirdShot.CreateShot(prefab, muzzle.position, shotVector, source, damage, timeOfLife);

            remainingDelay = delayBetweenShots;
        }
        
    }

    Vector3 DisperseVector(Vector3 originalVector, float angle)
    {

        Vector3 vector = originalVector.normalized; //Original vector must be normalized

        //Taking random values from pregenerated lists
        ushort angleDis = disperseAngles[index];
        float lenghtDis = disperseLengths[index];
        index++;
        if (index >= disperseAngles.Count) index = 0; //Cycling indexes

        angle *= Mathf.Deg2Rad; //Angle from degrees to rads
        float ratioMultiplier = Mathf.Tan(angle); //Tangens of angle for ratio between Dispersion Leg and Base Leg
        
        //Adding UP vector multiplied by ratio and random value and rotated on random angle
        vector += Quaternion.AngleAxis(angleDis, originalVector) * (lenghtDis * ratioMultiplier * muzzle.up); 

        return vector.normalized;
    }
}
