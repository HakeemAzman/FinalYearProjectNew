using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Animator planeAnim;

    public void StartGame()
    {
        StartCoroutine(StartGameAnims());
    }

    IEnumerator StartGameAnims()
    {
        planeAnim.GetComponent<Animator>().Play("FlyFront");

        yield return new WaitForSeconds(1.3f);

        SceneManager.LoadScene("Main Level");
    }
}
