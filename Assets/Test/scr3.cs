using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable IDE1006 // Naming Styles
public class scr3 : MonoBehaviour
#pragma warning restore IDE1006 // Naming Styles
{
    
    void Start()
    {

       Material mat = GetComponent<Renderer>().sharedMaterials[1];
        //print(mat);

        //Invoke("TurnOff", 1);

        //Invoke("TurnOn", 6);
    }

    private void Update()
    {
        //print("test");
    }

    void TurnOff()
    {
        this.enabled = false;
    }

    void TurnOn()
    {
        this.enabled = true;
    }







}
