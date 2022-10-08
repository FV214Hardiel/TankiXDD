using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr5 : MonoBehaviour
{
    public LayerMask testMask;

    void Start()
    {
        print(testMask.value);

        testMask = LayerMask.GetMask("RedTeam", "GreenTeam");

        print(testMask.value);
    }


   
}
