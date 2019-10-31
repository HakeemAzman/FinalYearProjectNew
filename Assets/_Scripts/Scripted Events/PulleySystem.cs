using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleySystem : MonoBehaviour
{
    public Animator GateAnimator;

    private void OnCollisionStay(Collision other)
    {
        print("Contact Count Enter: " + other.contactCount);

        if(other.transform.tag == "Player" || other.transform.tag == "Companion")
        {
            //open gate halfway
            GateAnimator.GetComponent<Animator>().Play("GateHalfwayUp");
        }
        else if (other.transform.tag == "Player" && other.transform.tag == "Companion")
        {
            //open gate fully
            GateAnimator.GetComponent<Animator>().Play("GateFullyOpen");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        print("Contact Count Exit: " + other.contactCount);
        if (other.transform.tag == "Player" || other.transform.tag == "Companion")
        {
            //open gate fully
            GateAnimator.GetComponent<Animator>().Play("GateHalfwayDown");
        }
        else if (other.transform.tag == "Player" && other.transform.tag == "Companion")
        {
            //open gate full
            GateAnimator.GetComponent<Animator>().Play("GateFullyClose");
        }
    }
}
