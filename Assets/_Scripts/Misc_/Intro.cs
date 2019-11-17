using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject introCutscene, introPlane, playerChar, playerCharUI;
    float timer = 24.1f;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            introCutscene.SetActive(false);
            introPlane.SetActive(false);
            playerChar.SetActive(true);
            playerCharUI.SetActive(true);
        }

        if (timer <= -3f) Destroy(this.gameObject);
        print(timer);
    }
}
