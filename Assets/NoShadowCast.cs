using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class NoShadowCast : MonoBehaviour
{
    
    void OnEnable()
    {
        transform.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

   
}
