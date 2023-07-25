using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTarget : MonoBehaviour
{
    Camera playerCamera;

    RaycastHit hit;

    Transform groundTarget;
    UnityEngine.UI.RawImage targetDetector;
    Color baseColor;

    Color orange;

    float range;

    int enemyLayer;
    List<int> enemyLayers;

    LayerMask collidingLayersUI;


    private void Start()
    {        
        playerCamera = Camera.main;

        groundTarget = GameObject.Find("GroundTargetUI").transform; //Just UI element
        targetDetector = GameObject.Find("TargetDetectorUI").GetComponent<UnityEngine.UI.RawImage>(); //Circle that shows if enemy is targeted

        range = GetComponent<Weapon>().weapRange + 3; //Range for RED dot (slightly more than actual)

        baseColor = targetDetector.color; 
        orange = new(1f, 0.56f, 0f); //Color for enemy out of range

        //enemyLayer = LayerMask.NameToLayer("RedTeam"); //Hardcoded enemy layer

        collidingLayersUI = GetComponentInParent<TankEntity>().EnemiesMasks + LevelHandler.instance.groundlayers;

        enemyLayers = new();
        for (int i = 0; i < LevelHandler.instance.teams.Count; i++)
        {
            if (GetComponentInParent<TankEntity>().team != LevelHandler.instance.teams[i])
            {
                enemyLayers.Add(LayerMask.NameToLayer(LevelHandler.instance.teams[i]));
            }
        }


    }
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.transform.forward, out hit, 300f, collidingLayersUI)) 
        {
            groundTarget.position = playerCamera.WorldToScreenPoint(hit.point); //If hit something then targeter is put there

            if (enemyLayers.Contains(hit.collider.gameObject.layer)) //If enemy hit
            {
                if (hit.distance < range) //If enemy in range
                {
                    targetDetector.color = Color.red;
                }
                else //If enemy out of range
                {
                    targetDetector.color = orange;
                }
                
            }
            else 
            {
                targetDetector.color = baseColor; //If no enemy hit
            }
            
        }
        else //If hit nothing then targeter is put at distance
        {
            groundTarget.position = playerCamera.WorldToScreenPoint(transform.position + transform.forward * 100f);
        }
        
    }
    
}
