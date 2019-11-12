﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttack : MonoBehaviour
{
    public float radius;
    public float kbForce;
    public int damage = 30;
    public CompanionScript cs;
    public GameObject Player;
    //public EnemyHealth eH;

    // Start is called before the first frame update
    void Start()
    {
        // eH = GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>();
        cs.GetComponent<CompanionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            areaofEffect();
        }
    }

    void areaofEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyEnemy in colliders)
        {
            if(nearbyEnemy.tag == "Enemy")
            {
                nearbyEnemy.gameObject.GetComponent<EnemyHealth>().enemy_Health -= damage;
                cs.charges -= 1;
            }
            //Rigidbody rb = nearbyEnemy.GetComponent<Rigidbody>();
            //if (rb != null)
            //{
            //    //rb.AddExplosionForce(kbForce, transform.position, radius);
            //    //eH = GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>();
            //    //GameObject.FindGameObjectsWithTag("Enemy");
            //    //eH.enemy_Health -= 1000;
            //}
        }
    }
}
