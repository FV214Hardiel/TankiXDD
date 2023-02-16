using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CumulativeDamageNumbers : MonoBehaviour
{
    public TextMeshPro textMesh;

    public float value;

    float timer;

    Transform mainCamera;

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
            //Destroy(gameObject);
            SetValue(0);
            
            gameObject.SetActive(false);
        }
    }

    public void AddValue(float numbers)
    {
       // print(numbers);

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
