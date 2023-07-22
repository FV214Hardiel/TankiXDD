using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmarkerScript : MonoBehaviour
{  

    private void OnEnable()
    {
        Destroy(gameObject, 0.2f);
        
    }

}
