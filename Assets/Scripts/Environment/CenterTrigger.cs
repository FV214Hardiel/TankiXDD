using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTrigger : MonoBehaviour
{
    public IEntity source;

    private void Start()
    {
        source = GetComponentInParent<IEntity>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (source == null) { source = GetComponentInParent<IEntity>(); }
        try
        {
            other.GetComponent<ISpecialZone>().TriggerEntered(source);
        }
        catch 
        {
            print("NO COMPONENT");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (source == null) { source = GetComponentInParent<IEntity>(); }
        try
        {
            other.GetComponent<ISpecialZone>().TriggerExited(source);
        }
        catch
        {
            print("NO COMPONENT");
        }
    }
}
