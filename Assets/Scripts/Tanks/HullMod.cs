using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HullMod", menuName = "Tanks/HullMod")]
public class HullMod : Modification
{
    public float baseHP;

    public float hullSpeed;
    public float hullRotation;

    public float baseSP;
    public float shieldRechargeRate;
    public float shieldRechargeDelay;

    public Texture2D lightmap;
    public Texture2D detailsmap;

}
