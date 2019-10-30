﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerCurrentHealth = 100f;
    public float player_Health = 100f;
    public Image playerHealthImage;

    // Update is called once per frame
    void Update()
    {
        playerHealthImage.fillAmount = (float)playerCurrentHealth / (float)player_Health;
    }
}
