using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    Transform handTransform;
    Transform planeTransform;

    public GameObject lego11, lego12, lego13, lego14, lego22, lego23, lego24, lego13s, lego14s, lego23s, lego24s;
    GameObject[] legos;
    List<GameObject> lastObjs;
    int legoIndex = 0;
    Quaternion legoRotation = Quaternion.identity;
    Dictionary<string, Color32> colors;
    int colorIndex = 0;

    float theta = 0;
    float radius = 9.93f;

    // Use this for initialization
    void Start () {
        handTransform = GameObject.Find("Hand").transform;
        planeTransform = GameObject.Find("Model").transform;
        legos = new GameObject[11] { lego11, lego12, lego13, lego14, lego22, lego23, lego24, lego13s, lego14s, lego23s, lego24s};
        colors = new Dictionary<string, Color32>(){
            { "White", new Color32(244, 244, 244, 255) },
            { "Yellow" , new Color32(254, 196, 0, 255) },
            { "Orange" , new Color32(231, 99, 24, 255) },
            { "Red" , new Color32(222, 0, 13, 255) },
            { "Purple" , new Color32(222, 55, 139, 255) },
            { "Blue" , new Color32(0, 87, 168, 255) },
            { "Dark Green" , new Color32(0, 123, 40, 255) },
            { "Light Green" , new Color32(199, 185, 11, 255) },
            { "Dark Brown" , new Color32(91, 28, 12, 255) },
            { "Light Brown" , new Color32(214, 114, 64, 255) },
            { "Black" , new Color32(1, 1, 1, 255) }
        };
        lastObjs = new List<GameObject>();
        changeHand();
    }
    bool horizVertDown = false, rightTriggerDown = false, horizDPadDown = false, vertDPad = false;
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("RHorizontal");
        if (Mathf.Abs(horizontal) < .3f)
        {
            horizontal = 0;
        }
        theta += 45 * horizontal * Time.deltaTime;
        //Debug.Log(new Vector3(radius * Mathf.Sin(Mathf.Deg2Rad * theta), 0, -radius * Mathf.Cos(Mathf.Deg2Rad * theta)));
        transform.position = new Vector3(radius*Mathf.Sin(Mathf.Deg2Rad*theta),3.34f, -radius*Mathf.Cos(Mathf.Deg2Rad*theta));
        if (horizontal != 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -horizontal*45 * Time.deltaTime, 0));
        }
        //--------------------------------------------
        //move position of hand by increments of 1
        //x-axis
        if (!horizVertDown && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.3f)
        {
            int round = 0;
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                round = 1;
            } else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                round = -1;
            }
            handTransform.position += new Vector3(Mathf.Round(Mathf.Cos(Mathf.Deg2Rad*theta)), 0, (Mathf.Abs(Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * theta)))==1) ? 0 : Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * theta))) * round;
            horizVertDown = true;
        } else if (!horizVertDown && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.3f)
        {
            //y-axis
            int round = 0;
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                round = 1;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                round = -1;
            }
            handTransform.position += new Vector3(Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * theta)), 0, (Mathf.Abs(Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * theta))) == 1) ? 0 : -Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * theta))) * round;
            horizVertDown = true;
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.3f && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.3f)
        {
            horizVertDown = false;
        }

        //z-axis
        if (Input.GetButtonDown("RightBumper"))
        {
            handTransform.position += 1.2f * Vector3.up;
        } else if (handTransform.position.y > 0 && Input.GetButtonDown("LeftBumper"))
        {
            handTransform.position += 1.2f*Vector3.down;
        }
        //--------------------------------------------
        //change the lego in hand
        if (Input.GetAxisRaw("HorizontalDPad") > .5f && !horizDPadDown)
        {
            //right dpad
            legoIndex++;
            legoIndex %= 11;
            legoRotation = Quaternion.identity;
            changeHand();
            horizDPadDown = true;
        } else if (Input.GetAxisRaw("HorizontalDPad") < -.5f && !horizDPadDown)
        {
            //left dpad
            legoIndex--;
            if (legoIndex == -1)
            {
                legoIndex = 10;
            }
            legoRotation = Quaternion.identity;
            changeHand();
            horizDPadDown = true;
        }

        if (Input.GetAxisRaw("HorizontalDPad") == 0)
        {
            horizDPadDown = false;
        }

        //rotate lego
        if(Input.GetButtonDown("XButton"))
        {
            legoRotation = Quaternion.Euler(legoRotation.eulerAngles + new Vector3(0, -90, 0));
            changeHand();
        } else if (Input.GetButtonDown("YButton"))
        {
            legoRotation = Quaternion.Euler(legoRotation.eulerAngles + new Vector3(0, 90, 0));
            changeHand();
        }

        //change color of lego
        if (Input.GetAxisRaw("VerticalDPad") > .5f && !vertDPad)
        {
            //right dpad
            colorIndex++;
            colorIndex %= 11;
            changeHand();
            vertDPad = true;
        }
        else if (Input.GetAxisRaw("VerticalDPad") < -.5f && !vertDPad)
        {
            //left dpad
            colorIndex--;
            if (colorIndex == -1)
            {
                colorIndex = 10;
            }
            changeHand();
            vertDPad = true;
        }
        if (Input.GetAxisRaw("VerticalDPad") == 0)
        {
            vertDPad = false;
        }

        //create object at position of hand
        if (Input.GetButtonDown("Fire1"))
        {
            placeObject(legos[legoIndex]);
            legoRotation = Quaternion.identity;
        }

        //zoom
        if (Input.GetAxisRaw("RightTrigger") > 0.3f)
        {
            transform.Translate(-Vector3.forward * 3f * Time.deltaTime);
        } else if (Input.GetAxisRaw("RightTrigger") < -0.3f)
        {
            transform.Translate(Vector3.forward * 3f * Time.deltaTime);
        }

        // scan
        if (Input.GetButtonDown("Menu") )
        {
            GameObject.Find("Model").GetComponent<Main>().scan();
        }

        //delete object
        if (Input.GetButtonDown("BButton") && lastObjs.Count > 0)
        {
            Destroy(lastObjs[lastObjs.Count - 1]);
            lastObjs.RemoveAt(lastObjs.Count - 1);
        }
    }

    void changeHand()
    {
        Destroy(handTransform.GetChild(0).gameObject);
        Transform obj = (Instantiate(legos[legoIndex], handTransform) as GameObject).transform;
        obj.rotation = legoRotation;
        obj.localPosition = Vector3.zero;
        int i = 0;
        foreach (KeyValuePair<string, Color32> entry in colors)
        {
            if (i == colorIndex)
            {
                Color32 color = entry.Value;
                color.a = 155;
                obj.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = color;
            }
            i++;
        }
    }

    void placeObject(GameObject lego)
    {
        Transform obj = (Instantiate(lego, planeTransform) as GameObject).transform;
        obj.position = handTransform.position;
        obj.rotation = legoRotation;
        int i = 0; 
        foreach(KeyValuePair<string, Color32> entry in colors)
        {
            if (i == colorIndex)
            {
                obj.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = entry.Value;
            }
            i++;
        }
        lastObjs.Add(obj.gameObject);
        changeHand();
    }
}
