using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr2 : MonoBehaviour
{

    [SerializeField] Transform dmgNumbers;
    // Start is called before the first frame update
    void Start()
    {
        dmgNumbers = Resources.Load<Transform>("DamageNumbersPopup");

        Instantiate(dmgNumbers, transform.position, transform.rotation);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
