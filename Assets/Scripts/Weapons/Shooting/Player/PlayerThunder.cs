using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThunder : PlayerShooting
{
    public GameObject shellPref;
    

    public float projectileSpeed;
    float timeOfLife;
    // Start is called before the first frame update
    void Start()
    {
        //Base setup for shooting
        source = GetComponentInParent<EntityHandler>();
        muzzle = transform.Find("muzzle");

        timeOfLife = weapRange / projectileSpeed;

        shotSound = GetComponent<AudioSource>();

        remainingDelay = 0;

        inputActions = new();
        if (!GameHandler.GameIsPaused) inputActions.PlayerTankControl.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.GameIsPaused) return; //Checking pause

        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            return;
        }

        inputValue = inputActions.PlayerTankControl.Fire.ReadValue<float>();

        //Making a shot
        if (inputValue > 0)
        {
            Shot();
        }

    }

    void Shot()
    {
        shotSound.Play();
        ThunderShell.CreateShot(shellPref, muzzle.position, muzzle.forward * 45, source, damage, timeOfLife);
        remainingDelay = delayBetweenShots;
    }
}
