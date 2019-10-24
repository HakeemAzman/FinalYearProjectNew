using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStart : MonoBehaviour
{
    public GameObject projectileSystem;

    void Start()
    {
        projectileSystem.SetActive(true);
    }
}
