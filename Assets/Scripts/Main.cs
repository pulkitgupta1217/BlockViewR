using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	public void scan()
    {
        int objCount = 0;
        //hide objects
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            objCount++;
        }
        int y = 0;
        do
        {
            List<Transform> objectsOnLayer = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (Mathf.RoundToInt(transform.GetChild(i).position.y) == y)
                {
                    //same y
                    transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                    objectsOnLayer.Add(transform.GetChild(i));
                    objCount--;
                }
            }
            screenshot(y, objectsOnLayer);
            y++;
        } while (objCount > 0);
    }

    void screenshot(int z, List<Transform> objects)
    {
        Debug.Log("SS");
        //Application.CaptureScreenshot("Level " + z+".png");
    }
}
