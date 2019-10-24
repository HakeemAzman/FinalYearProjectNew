using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float player_Health = 100f;
    public Slider player_HealthSliderUI;

    // Update is called once per frame
    void Update()
    {
        player_HealthSliderUI.value = player_Health;
    }
}
