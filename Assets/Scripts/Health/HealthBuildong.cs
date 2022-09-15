using System;
using UnityEngine;

public class HealthBuildong : Health
{

    

    public GameObject BuildingOk;
    public GameObject BuildingNeOk;
       
    public Collider[] Debris;

    public bool isObjective;
    public static event Action objectiveBuildingDestroyed;    
    void Start()
    {
        maxHP = baseHP;
        HP = maxHP;
        Alive = true;

        BuildingOk = GetComponentInChildren<BuildingOk>().gameObject;
        BuildingNeOk = GetComponentInChildren<BuildingNotOk>(true).gameObject;
        Debris = BuildingNeOk.GetComponentsInChildren<Collider>(true);

    }

    public override void TakingDMG(float damage, GameObject source)
    {
        HP -= damage;
        HP = Mathf.Clamp(HP, 0, maxHP);
        if (HP <= 0 && Alive)
        {
            BuildingOk.SetActive(false);
            BuildingNeOk.SetActive(true);
            Destroy(Instantiate(ExpPref, transform), 5);
            foreach (MonoBehaviour inthere in GetComponentsInChildren<MonoBehaviour>())
            {
                Destroy(inthere);
            }
            foreach (Collider nearby in Debris)
            {

                nearby.TryGetComponent(out Rigidbody rb);
                rb.AddExplosionForce(80, transform.position + transform.up, 40);

            }
            Alive = false;

            if (isObjective)
            {
                objectiveBuildingDestroyed?.Invoke();
            }    
            
        }
    }

    

    
    
}
