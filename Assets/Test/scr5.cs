using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class scr5 : MonoBehaviour
{
    Ray ray;

    //RaycastHit hit;
    public LayerMask layers;

    void OnEnable()
    {
        ray = new(transform.position, transform.forward);

        
        layers |= (1 << LayerMask.NameToLayer("RedTeam"));
        
        
        
    }

    private void Update()
    {
       
    }

    public void Action1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            print("shot");

            RaycastHit[] hit = Physics.RaycastAll(ray, 40f, layers);
            
            List<RaycastHit> newList = new();
            newList.AddRange(hit);

            //foreach (RaycastHit item in hit)
            //{
            //    print(item.transform.name);
            //}

            Vector3 endOfLine = transform.position + transform.forward * 40f; 
            newList.Sort((x, y) => x.distance.CompareTo(y.distance));
            foreach (RaycastHit item in newList)
            {
                

                print(item.transform.name);
                if (item.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    endOfLine = item.point;
                    break;
                }
            }

            Debug.DrawLine(transform.position, endOfLine, Color.blue, 5f);

        }



        //if (context.performed)
        //{
        //    print("shot with single rays");
        //    float range = 40f;

        //    Physics.Raycast(ray, out hit, range);
            
        //}


        
    }
    


   
}
