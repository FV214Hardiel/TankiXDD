using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual float GetMaxSpeed()
    {
        return 0;
    }

    public virtual void SetMaxSpeed(float multiplier)
    {
       
    }

    public virtual IEnumerator Stun()
    {
        print("virtual move");
        yield return null;
    }
}
