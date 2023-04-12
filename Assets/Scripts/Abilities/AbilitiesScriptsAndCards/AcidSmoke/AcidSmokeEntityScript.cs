using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSmokeEntityScript : MonoBehaviour, ISpecialZone
{

    float duration;
    float remainingDuration;
    float innerCD;
    float remInnerCD;

    float damage;
    float radius;
    IEntity source;

    List<IEntity> hitColls;

    // Start is called before the first frame update
    void Start()
    {
        innerCD = 0.4f;
        remInnerCD = 0;

        transform.localScale = Vector3.one * radius;

        hitColls = new List<IEntity>();
    }

    public void Init(float _damage, float _radius, IEntity _source)
    {
        damage = _damage;
        radius = _radius;
        source = _source;
    }
    void Update()
    {
        if (remInnerCD <= 0)
        {
            
            
            foreach (IEntity item in hitColls)
            {
                if (item !=  source)
                item.DealAOE(new Damage(damage, source));
                
            }


            remInnerCD = innerCD;
        }
        remInnerCD -= Time.deltaTime;
    }

    public void TriggerEntered(IEntity other)
    {
        hitColls.Add(other);
    }

    public void TriggerExited(IEntity other)
    {
        hitColls.Remove(other);
    }
}
