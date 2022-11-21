using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumbersPopup : MonoBehaviour
{
    public TextMeshPro textMesh;

    public float value;

    float timer;

    Transform mainCamera;
    public static void Create(Transform damagePopupPrefab, Vector3 position, Vector3 right, float numbers, Color colour)
    {
        int disperse = Random.Range(0, 21);
        disperse -= 10;
        Transform DamageNumbersObject = Instantiate(damagePopupPrefab, position + right * disperse/5, Camera.main.transform.rotation);
        TextMeshPro textMesh = DamageNumbersObject.GetComponent<TextMeshPro>();
        textMesh.text = Mathf.Floor(numbers).ToString();
        textMesh.renderer.material.SetColor("_OutlineColor", colour);
        Destroy(DamageNumbersObject.gameObject, 2);
    }

    public static DamageNumbersPopup CreateStatic(Transform damagePopupPrefab, Vector3 position, float numbers, Color colour)
    {
        Transform DamageNumbersObject = Instantiate(damagePopupPrefab, position, Camera.main.transform.rotation);

        DamageNumbersPopup popup = DamageNumbersObject.GetComponent<DamageNumbersPopup>();

        popup.textMesh = DamageNumbersObject.GetComponent<TextMeshPro>();
        popup.value = numbers;
        

        popup.timer = 2;
        
        popup.textMesh.renderer.material.SetColor("_OutlineColor", colour);        

        return popup;
    }
    void Start()
    {        
        mainCamera = Camera.main.transform; 
        
        textMesh.text = Mathf.Floor(value).ToString();
    }

    
    void Update()
    {
        transform.rotation = mainCamera.rotation;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }        

    }

    public void ChangeText(float numbers)
    {
        value += numbers;
        textMesh.text = Mathf.Floor(value).ToString();
        timer = 2;
    }
}
