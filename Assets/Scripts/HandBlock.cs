using UnityEngine;
using System.Collections;

public class HandBlock : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other)
    {
        if (other.tag=="Hand")
        {
            return;
        }
        Debug.Log(gameObject.name);
        Debug.Log("Stay: "+other.name);
        Player.colliding = true;
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag=="Hand")
        {
            return;
        }
        Debug.Log("Exit");
        Player.colliding = false;
    }
}
