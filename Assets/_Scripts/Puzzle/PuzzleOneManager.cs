using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleOneManager : MonoBehaviour
{
    [SerializeField] GameObject cubeA, cubeB, gate, robotCage;

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
    }
}
