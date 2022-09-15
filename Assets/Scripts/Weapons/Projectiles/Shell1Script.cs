using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell1Script : MonoBehaviour
{
    public Rigidbody ShellB;
    public float expForce;
    public float expRadius;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Gun"))
        {
            Explode();
        }
        
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, expRadius);

        foreach (Collider nearby in colliders)
        {
            if (nearby.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(expForce, transform.position, expRadius);
            }
        }
        Destroy(gameObject);
        Debug.Log("UBIT");
    }
}
