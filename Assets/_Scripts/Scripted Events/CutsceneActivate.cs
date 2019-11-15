using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneActivate : MonoBehaviour
{
    public GameObject obs;
    public float cutsceneLengthInSecs;
    public PlayableDirector pd;

    bool cutsceneActivated;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            obs.SetActive(true);
            StartCoroutine(WaitForCutscene());
        }
    }

    IEnumerator WaitForCutscene()
    {
        player.GetComponent<PlayerMovement>().player_SetSpeed = 0;
        yield return new WaitForSeconds(cutsceneLengthInSecs);
        player.GetComponent<PlayerMovement>().player_SetSpeed = 8;
        Destroy(this.gameObject);
    }
}
