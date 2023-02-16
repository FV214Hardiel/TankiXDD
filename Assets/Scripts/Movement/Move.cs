using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public List<float> msMultipliers;

    protected Rigidbody rb;

    protected AudioSource engineAudio;

    public float Speed;
    public float TurnSpeed;
    protected float maxSpeed;


    public virtual float GetMaxSpeed()
    {
        return 0;
    }

    public virtual void SetMaxSpeed(float multiplier)
    {
       
    }

    public virtual void RecalculateSpeed()
    {
        print("virtual rec");
    }

    public virtual IEnumerator Stun()
    {
       
        yield return null;
    }
}
