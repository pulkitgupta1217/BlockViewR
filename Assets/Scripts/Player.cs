using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Transform handTransform;
    Transform planeTransform;

    public GameObject lego11, lego12, lego13, lego14, lego22, lego23, lego24, lego13s, lego14s, lego23s, lego24s;

    float theta = 0;
    float radius = 9.93f;

    // Use this for initialization
    void Start () {
        handTransform = GameObject.Find("Hand").transform;
        planeTransform = GameObject.Find("Model").transform;
    }
    bool rhorizontalDown = false;
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        theta += 45 * horizontal * Time.deltaTime;
        //Debug.Log(new Vector3(radius * Mathf.Sin(Mathf.Deg2Rad * theta), 0, -radius * Mathf.Cos(Mathf.Deg2Rad * theta)));
        transform.position = new Vector3(radius*Mathf.Sin(Mathf.Deg2Rad*theta),3.34f, -radius*Mathf.Cos(Mathf.Deg2Rad*theta));
        if (horizontal != 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -horizontal*45 * Time.deltaTime, 0));
        }

        //move position of hand by increments of 1
        if (Input.GetAxisRaw("RHorizontal") != 0 && !rhorizontalDown)
        {
            int round = 0;
            if (Input.GetAxisRaw("RHorizontal") > 0)
            {
                round = 1;
            } else if (Input.GetAxisRaw("RHorizontal") < 0)
            {
                round = -1;
            }
            handTransform.position += Vector3.right * round;
            rhorizontalDown = true;
        }
        if (Input.GetAxisRaw("RHorizontal") == 0)
        {
            rhorizontalDown = false;
        }
        //create object at position of hand
        if (Input.GetButton("Fire1"))
        {
            placeObject(lego11);
        }
	}

    void placeObject(GameObject lego)
    {
        Transform obj = (Instantiate(lego, planeTransform) as GameObject).transform;
        obj.position = handTransform.position;
    }
}
