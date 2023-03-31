using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumbersPopup : MonoBehaviour
{
    public TextMeshPro textMesh;

    public float value;

    float timer;
    float speed;

    Transform mainCamera;

    //public static void Create(Transform damagePopupPrefab, Vector3 position, Vector3 right, float numbers, Color colour)
    //{
        
    //    Transform DamageNumbersObject = Instantiate(damagePopupPrefab, position + right * disperse/5, Camera.main.transform.rotation);
    //    TextMeshPro textMesh = DamageNumbersObject.GetComponent<TextMeshPro>();
    //    textMesh.text = Mathf.Floor(numbers).ToString();
    //    textMesh.renderer.material.SetColor("_OutlineColor", colour);
    //    print("test");
    //    Destroy(DamageNumbersObject.gameObject, 2);
    //}

    public static DamageNumbersPopup CreateStatic(Transform damagePopupPrefab, Transform hpBar, float numbers, Color colour)
    {
        int disperse = Random.Range(0, 21);
        disperse -= 10;

        Transform DamageNumbersObject = Instantiate(damagePopupPrefab, hpBar.position + hpBar.right * disperse / 5, Camera.main.transform.rotation);

        DamageNumbersPopup popup = DamageNumbersObject.GetComponent<DamageNumbersPopup>();

        popup.textMesh = DamageNumbersObject.GetComponent<TextMeshPro>();

        popup.textMesh.text = Mathf.Floor(numbers).ToString();

        popup.timer = 1.5f;
        
        popup.textMesh.renderer.material.SetColor("_OutlineColor", colour);        

        return popup;
    }

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }
    void Start()
    {
        mainCamera = Camera.main.transform;

        speed = 11;
    }

    
    void Update()
    {
        transform.rotation = mainCamera.rotation;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }

        transform.position += Time.deltaTime * Vector3.up * speed;
        speed -= Time.deltaTime * 7;

    }

    public void Init(float numbers, Color colour)
    {

        int disperse = Random.Range(0, 21);
        disperse -= 10;
        
        GameObject DamageNumbersObject = Instantiate(gameObject, transform.position + transform.right * disperse / 5, Camera.main.transform.rotation);
        DamageNumbersPopup popup = DamageNumbersObject.GetComponent<DamageNumbersPopup>();

        popup.textMesh = DamageNumbersObject.GetComponent<TextMeshPro>();

        popup.textMesh.text = Mathf.Floor(numbers).ToString();

        popup.timer = 1.5f;

        popup.textMesh.renderer.material.SetColor("_OutlineColor", colour);

        DamageNumbersObject.SetActive(true);
    }

   
}
