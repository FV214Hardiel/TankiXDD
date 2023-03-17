using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirebird : PlayerShooting
{
   
    Vector3 shotVector;

    public GameObject prefab;

    public float projectileSpeed;
    float timeOfLife;

    private void Start()
    {
        source = GetComponentInParent<TankEntity>();
        muzzle = transform.Find("muzzle");

        inputActions = new();
        //if (!GameHandler.instance.GameIsPaused) 
            inputActions.PlayerTankControl.Enable();

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
        if (GameHandler.instance.GameIsPaused) //Checking pause
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
            Shot();
        }
        
    }

    protected override void Shot()
    {
        shotVector = DisperseVector(muzzle.forward, angle) * projectileSpeed;

        FirebirdShot.CreateShot(prefab, muzzle.position, shotVector, source, damage, timeOfLife);

        remainingDelay = delayBetweenShots;
    }
    


}
