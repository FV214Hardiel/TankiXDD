using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHitmarkerScript : MonoBehaviour
{
    public static UIHitmarkerScript instance;
    public GameObject hitmarkerPrefab;


    private void OnEnable()
    {
        instance = this;
    }

    public GameObject CreateHitmarker()
    {       



        return Instantiate(hitmarkerPrefab, gameObject.transform);
    }


}
