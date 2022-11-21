using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public delegate void ShotDelegate();
    public ShotDelegate shotDelegate;



    protected LayerMask enemyMask;
    protected LayerMask friendlyMask;

    protected virtual void Shot()
    {

    }

    protected virtual void OverloadShot()
    {

    }

    public virtual void EnableOverload()
    {
        shotDelegate = OverloadShot;
    }

    public virtual void DisableOverload()
    {
        shotDelegate = Shot;
    }
}
