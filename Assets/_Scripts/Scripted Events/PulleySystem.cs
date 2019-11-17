using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleySystem : MonoBehaviour
{
    public Animator GateAnimator;
    public float counter;


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag =="Companion")
        {
            counter = 1;
        }

        if(other.gameObject.tag == "Player")
        {
            counter = 2;
        }

        if(counter == 1)
        {
            GateAnimator.GetComponent<Animator>().SetBool("isHalfOpen", true);
        }

        if (counter == 2)
        {
            GateAnimator.GetComponent<Animator>().SetBool("isFullOpen", true);
        }
        //print("Contact Count Enter: " + other.contactCount);

        //if(other.gameObject.tag == "Player" || other.gameObject.tag == "Companion")
        //{
        //    //open gate halfway
        //    GateAnimator.GetComponent<Animator>().SetBool("isHalfOpen", true);
        //}
        //if (other.gameObject.tag == "Player" && other.gameObject.tag == "Companion")
        //{
        //    //open gate fully
        //    print("GatefullyOpen");
        //    GateAnimator.GetComponent<Animator>().SetBool("isFullOpen",true);
        //}
    }

    private void OnCollisionExit(Collision other)
    {
        print("Contact Count Exit: " + other.contactCount);
        if (other.transform.tag == "Player" || other.transform.tag == "Companion")
        {
            print("Come back");
            //open gate fully
            GateAnimator.GetComponent<Animator>().SetBool("isHalfOpen",false);
        }
        //else if (other.transform.tag == "Player" && other.transform.tag == "Companion")
        //{
        //    //open gate full
        //    GateAnimator.GetComponent<Animator>().SetBool("isFullOpen", false);
        //}
    }
}
