using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttack : MonoBehaviour
{
    public float radius;
    public float kbForce;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            print("Hi");
            areaofEffect();
        }
    }

    void areaofEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyEnemy in colliders)
        {
            Rigidbody rb = nearbyEnemy.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(kbForce, transform.position, radius);

            }
        }
    }
}
