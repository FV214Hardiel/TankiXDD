using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStormbreaker : PlayerShooting
{
    AudioSource chargingSound;
    AudioSource chargedSound;

    bool isCharging;

    [SerializeField]
    float chargeEnergy;

    public float maxCharge;

    Light chargeLight;
    float testTimer;

    public LayerMask affectedLayers;
    public LayerMask stoppingLayers;


    Vector3 shotVector;

    public GameObject prefabOfShot;
    public ParticleSystem shotEffect;
    void Start()
    {
        source = GetComponentInParent<IEntity>();
        muzzle = transform.Find("muzzle");
        chargeLight = transform.Find("ChargeLight").GetComponent<Light>();

        inputActions = new();
        if (!GameHandler.instance.GameIsPaused) inputActions.PlayerTankControl.Enable();

       
        inputActions.PlayerTankControl.Fire.started += FireButtonStarted;
        inputActions.PlayerTankControl.Fire.canceled += FireButtonCanceled;

        shotSound = GetComponent<AudioSource>();
        chargingSound = transform.Find("ChargingSound").GetComponent<AudioSource>();
        chargedSound = transform.Find("ChargedSound").GetComponent<AudioSource>();

        remainingDelay = 0;

        friendlyMask = source.FriendlyMasks;
        enemyMask = source.EnemiesMasks;

       
        affectedLayers += enemyMask;

        chargeEnergy = 0;

       

    }

    void Update()
    {
        if (GameHandler.instance.GameIsPaused) return; //Checking pause

        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            return;
        }

        if (isCharging)
        {
            testTimer += Time.deltaTime;
            chargeEnergy += Time.deltaTime * 10;
            chargeLight.intensity = chargeEnergy / maxCharge;
            if (chargeEnergy >= maxCharge)
            {
                //print(testTimer);
                
                isCharging = false;
                chargedSound.Play();
                chargeLight.intensity = 3;
            }
        }

    }

    void Shot(Vector3 shotVector)
    {
               
        shotSound.Play();
        shotEffect.Play();

        Ray ray = new(muzzle.position, shotVector);

        RaycastHit[] hit = Physics.RaycastAll(ray, weapRange, affectedLayers);

        List<RaycastHit> newList = new();
        newList.AddRange(hit);

        Vector3 endOfLine = transform.position + transform.forward * weapRange;
        newList.Sort((x, y) => x.distance.CompareTo(y.distance));

        List<IDamagable> hitList = new();

        foreach (RaycastHit item in newList)
        {
            
            if (item.transform.gameObject.layer == stoppingLayers)
            {
                endOfLine = item.point;
                break;
            }

            IDamagable damagable = item.transform.GetComponentInParent<IDamagable>();

            if (damagable != null)
            {
                if (!hitList.Contains(damagable))
                {
                    damagable.DealDamage(damage, source);
                    hitList.Add(damagable);
                }
                

            }
        }

        WeaponTrail.Create(prefabOfShot, muzzle.position, endOfLine);

        remainingDelay = delayBetweenShots;

    }

    private void OnEnable()
    {
        
    }

    private void OnDestroy()
    {
        inputActions.PlayerTankControl.Fire.started -= FireButtonStarted;
        inputActions.PlayerTankControl.Fire.canceled -= FireButtonCanceled;
    }

    void FireButtonStarted(InputAction.CallbackContext context)
    {
        if (remainingDelay > 0) return;

        //print("charging started");
        
        isCharging = true;
        chargingSound.Play();
    }

    void FireButtonCanceled(InputAction.CallbackContext context)
    {
       // print("charging canceled");

        chargingSound.Stop();
        isCharging = false;
        if (chargeEnergy >= maxCharge)
        {
            chargedSound.Stop();
            Shot(muzzle.forward);            
            
        }
        chargeEnergy = 0;
        testTimer = 0;
        chargeLight.intensity = 0;
        //print(testTimer);
    }
}
