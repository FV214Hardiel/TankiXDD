using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class scr5 : MonoBehaviour
{
    

    void OnEnable()
    {
        
    }

    private void Update()
    {
       
    }

    public void Action1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("shot");

            RaycastHit[] hit = Physics.RaycastAll(transform.position, transform.forward, 40f);
            
            foreach (RaycastHit item in hit)
            {
                print(item.transform.name);
            }
            
        }

        
        

    }
    


   
}
