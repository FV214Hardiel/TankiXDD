using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrail : MonoBehaviour
{
    public float timeOfLife;
    LineRenderer lr;
    
    Color endColor;
    Color startColor;
    Color c;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        startColor = lr.startColor;
        endColor = lr.endColor;
        

        Destroy(gameObject, timeOfLife);

    }

    // Update is called once per frame
    void Update()
    {        
        startColor.a -= Time.deltaTime / timeOfLife;
        endColor.a -= Time.deltaTime / timeOfLife;

        
        lr.endColor = endColor;
        lr.startColor = startColor;
    }

    public static void Create(GameObject prefab, Vector3 origin, Vector3 end)
    {
        LineRenderer lr = Instantiate(prefab, origin, Quaternion.identity).GetComponent<LineRenderer>();
        lr.SetPosition(0, origin);
        lr.SetPosition(1, end);
    }
}
