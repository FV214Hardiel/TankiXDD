using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAI : MonoBehaviour
{
    bool isAttack = false;

    float deltaTime = 1;

    TankEntity source;
    [SerializeField]
    LayerMask enemyMask;
    Ray lineOfFire;
    RaycastHit hit;

    float rangeForCheck;
    Weapon weaponScript;
    Transform muzzle;

    IEnumerator corotine;

    void Start()
    {
        source = GetComponent<TankEntity>();
        weaponScript = GetComponentInChildren<Weapon>();
        muzzle = weaponScript.muzzle;
        corotine = CustomUpdate(deltaTime);
        rangeForCheck = weaponScript.weapRange + weaponScript.additionalRange;
        enemyMask = source.EnemiesMasks;
    }

    
    public IEnumerator CustomUpdate(float timeDelta)
    {
        while (true)
        {
            lineOfFire = new Ray(muzzle.position, muzzle.forward);
            if (Physics.Raycast(lineOfFire, rangeForCheck, enemyMask))
            {
                weaponScript.OpenFire();
            }
            else
            {
                weaponScript.CeaseFire();
            }

            yield return new WaitForSeconds(timeDelta);

        }
    }

    public void StartAttack()
    {
        if (isAttack) return;

        isAttack = true;
        StartCoroutine(corotine);
    }

    public void EndAttack()
    {
        if (!isAttack) return;

        isAttack = false;
        StopCoroutine(corotine);
        weaponScript.CeaseFire();
    }

}
