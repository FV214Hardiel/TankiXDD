using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List", menuName = "Tanks/List")]
public class AllHullsTurrets : ScriptableObject
{
   

    public GameObject[] allHulls;
    public GameObject[] allTurrets;
}
