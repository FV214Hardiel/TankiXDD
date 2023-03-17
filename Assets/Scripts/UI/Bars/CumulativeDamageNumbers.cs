using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CumulativeDamageNumbers : MonoBehaviour
{
    public TextMeshPro textMesh;

    public float value;

    float timer;   

    private void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        //value = 0f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {            
            SetValue(0);
            
            gameObject.SetActive(false);
        }
    }

    public void AddValue(float numbers)
    {
        value += numbers;
        textMesh.text = Mathf.Floor(value).ToString();
        timer = 2;
    }

    public void SetValue(float numbers)
    {
        value = numbers;
        textMesh.text = Mathf.Floor(value).ToString();
        timer = 2;
    }


}
