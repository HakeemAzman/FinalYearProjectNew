using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneActivate : MonoBehaviour
{
    public GameObject obs;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            obs.SetActive(true);
        }
    }
}
