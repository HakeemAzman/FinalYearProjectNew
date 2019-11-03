using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleOneManager : MonoBehaviour
{
    [SerializeField] GameObject cubeA, cubeB, gate, robotCage, cageObstacle,GateAnimator;

    [SerializeField] AudioClip gateOpeningSound, heavyGateOpeningSound;
    AudioSource audioS;
    
    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleportA") && Input.GetButtonDown("Stay"))
        {
            this.transform.position = cubeB.transform.position;
        }

        if (other.CompareTag("TeleportB") && Input.GetButtonDown("Stay"))
        {
            this.transform.position = cubeA.transform.position;
        }

        if((other.gameObject.name == "ChainRobot") && Input.GetButtonDown("Stay"))
        {
            robotCage.GetComponent<Animator>().Play("RobotCageOpen");
        }

        if (other.CompareTag("PressurePlate"))
        {
            audioS.PlayOneShot(heavyGateOpeningSound, 0.3f);
        }

        if ((other.gameObject.name == "ObstacleChain") && Input.GetButtonDown("Stay"))
        {
            StartCoroutine("LiftDown");
        }

        if(other.gameObject.name == "PressurePlatePuzzle2")
        {
          GateAnimator.gameObject.GetComponent<Animator>().Play("GateOpenPuzzle2"); 
        }
    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TeleportA") && Input.GetButtonDown("Stay"))
        {
            this.transform.position = cubeB.transform.position;
        }

        if (other.CompareTag("TeleportB") && Input.GetButtonDown("Stay"))
        {
            this.transform.position = cubeA.transform.position;
        }

        if(other.CompareTag("PressurePlate"))
        {
            gate.GetComponent<Animator>().Play("GateOpen");
        }

        if ((other.gameObject.name == "ChainRobot") && Input.GetButtonDown("Stay"))
        {
            robotCage.GetComponent<Animator>().Play("RobotCageOpen");
        }

        if ((other.gameObject.name == "ObstacleChain") && Input.GetButtonDown("Stay"))
        {
            StartCoroutine("LiftDown");
        }
    }

    IEnumerator LiftDown()
    {
        cageObstacle.GetComponent<Animator>().Play("CageObstacleLift");
        yield return new WaitForSeconds(20f);
        cageObstacle.GetComponent<Animator>().Play("CageObstacleLiftDown");
    }
}
