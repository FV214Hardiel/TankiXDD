using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanTrail : MonoBehaviour
{
    public float timeOfLife;
    LineRenderer lr;
    Color bColor;
    Color eColor;
    Color c;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        //bColor = lr.startColor;
        eColor = lr.endColor;
        

        Destroy(gameObject, timeOfLife);

    }

    // Update is called once per frame
    void Update()
    {
        //bColor.a -= Time.deltaTime / timeOfLife;
        eColor.a -= Time.deltaTime / timeOfLife;

        //lr.startColor = bColor;
        lr.endColor = eColor;
    }

    public static void Create(GameObject prefab, Vector3 origin, Vector3 end)
    {
        LineRenderer lr = Instantiate(prefab).GetComponent<LineRenderer>();
        lr.SetPosition(0, origin);
        lr.SetPosition(1, end);
    }
}
