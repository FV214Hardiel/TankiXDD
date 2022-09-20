using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretMod", menuName = "Tanks/TurretMod")]
public class TurretMod : Modification
{
    public float damage;

    public float delayBetweenShots;
    public float reloadTime;

    public float attackRange;

    public float turretRotationSpeed;

    public Texture2D lightmap;
    public Texture2D detailsmap;

}

