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
    }

    //public static CumulativeDamageNumbers CreateStatic(Transform damagePopupPrefab, Vector3 position, float numbers, Color colour)
    //{
    //    Transform DamageNumbersObject = Instantiate(damagePopupPrefab, position, Camera.main.transform.rotation);

    //    CumulativeDamageNumbers popup = DamageNumbersObject.GetComponent<CumulativeDamageNumbers>();

    //    popup.textMesh = DamageNumbersObject.GetComponent<TextMeshPro>();
    //    popup.value = numbers;


    //    popup.timer = 2;

    //    popup.textMesh.renderer.material.SetColor("_OutlineColor", colour);

    //    return popup;
    //}

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
        //print(numbers);

        value += numbers;
        textMesh.text = Mathf.Floor(value).ToString();
        timer = 2;
    }

    public void SetValue(float numbers)
    {
        //print(numbers);

        value = numbers;
        textMesh.text = Mathf.Floor(value).ToString();
        timer = 2;
    }


}
