using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Weapon
{
   
    //public float angle;
    //protected List<ushort> disperseAngles;
    //protected List<float> disperseLengths;
    //protected int index;

    //protected PlayerInputActions inputActions;
    //protected float inputValue;

    //protected Vector3 DisperseVector(Vector3 originalVector, float angle)
    //{
    //    Vector3 vector = originalVector.normalized; //Original vector must be normalized

    //    //Taking random values from pregenerated lists
    //    ushort angleDis = disperseAngles[index];
    //    float lenghtDis = disperseLengths[index];

    //    index++;
    //    if (index >= disperseAngles.Count) index = 0; //Cycling indexes

    //    angle *= Mathf.Deg2Rad; //Angle from degrees to rads

    //    float ratioMultiplier = Mathf.Tan(angle); //Tangens of angle for ratio between Dispersion Leg and Base Leg       

    //    //Adding UP vector multiplied by ratio and random value and rotated on random angle
    //    vector += Quaternion.AngleAxis(angleDis, originalVector) * (lenghtDis * ratioMultiplier * transform.up);

    //    return vector.normalized;
    //}

    //protected void InitAnglesAndLengthLists()
    //{
    //    disperseAngles = new();
    //    for (int _ = 0; _ < 50; _++)
    //    {
    //        disperseAngles.Add((ushort)Random.Range(1, 357));
    //    }

    //    disperseLengths = new();
    //    for (int _ = 0; _ < 50; _++)
    //    {
    //        disperseLengths.Add(Random.Range(0f, 1f));
    //    }

    //    index = 0;
    //}

}
