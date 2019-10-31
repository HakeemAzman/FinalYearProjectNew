using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleySystem : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if(other.contactCount == 1)
        {
            //open gate halfway
        }
        else if(other.contactCount > 1)
        {
            //open gate fully
        }
    }
}
