using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumbersPopup : MonoBehaviour
{
    // Start is called before the first frame update

    //Camera main;

    TextMeshPro textMesh;

    float speed;

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
    void OnEnable()
    {
        speed = 10;
        //main = Camera.main; 
        //textMesh = transform.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Time.deltaTime * Vector3.up * speed;
        speed -= Time.deltaTime * 3;

    }
}
