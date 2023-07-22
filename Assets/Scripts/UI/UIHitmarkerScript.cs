using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHitmarkerScript : MonoBehaviour
{
    public static UIHitmarkerScript instance;
    public GameObject hitmarkerPrefab;
    public AudioSource hitmarkerSound;


    private void OnEnable()
    {
        instance = this;
        hitmarkerSound = GetComponent<AudioSource>();
        
    }

    public GameObject CreateHitmarker()
    {
        hitmarkerSound.Play();

        return Instantiate(hitmarkerPrefab, gameObject.transform);
    }


}
