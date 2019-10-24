using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemy_Health;
    public GameObject deathParticle;
    public Compbat combatS;
    public CompanionScript cs;


    public void Start()
    {
        cs = GameObject.FindWithTag("Companion").GetComponent<CompanionScript>();
        combatS = GameObject.FindWithTag("Companion").GetComponent<Compbat>();
    }

    void Update()
    {
        if(enemy_Health <= 0)
        {
            cs.speedFloat = 5;
            Instantiate(deathParticle, transform.position, transform.rotation);
            combatS.isEnemy = false;
            Destroy(gameObject, 0.5F);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ArmAttack")
        {
            enemy_Health -= 25;
        }
    }
}
