using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaStuffDeletePls : MonoBehaviour
{

    public GameObject enemySpawn;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enemySpawn.SetActive(true);
        }
    }
}
