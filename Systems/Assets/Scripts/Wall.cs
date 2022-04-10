using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player")
            Debug.Log("Press E to climb");
    }

    void OnTriggerStay(Collider other){
        if(other.tag == "Player"){
            if(Input.GetKeyDown("e") && !other.GetComponent<Movement>().IsClimbing() && other.GetComponent<Movement>().allowClimb)
                other.GetComponent<Movement>().EnableClimbing();
            else if (Input.GetKeyDown("space") && other.GetComponent<Movement>().IsClimbing() && other.GetComponent<Movement>().allowClimb)
                other.GetComponent<Movement>().DisableClimbing();
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Player")
            if(other.GetComponent<Movement>().IsClimbing())
                other.GetComponent<Movement>().DisableClimbing();
    }
}
