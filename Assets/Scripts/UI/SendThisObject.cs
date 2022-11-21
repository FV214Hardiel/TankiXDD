using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendThisObject : MonoBehaviour
{
    public ShopScript shopScript;

    public void SendObject()
    {
        shopScript.ButtonClicked(gameObject);
    }
}
