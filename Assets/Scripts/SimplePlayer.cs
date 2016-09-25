using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SimplePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(Input.GetAxis("RHorizontal")) > 0.3f || Mathf.Abs(Input.GetAxis("RVertical")) > 0.3f)
        {
            transform.position += transform.TransformDirection(new Vector3(Input.GetAxis("RHorizontal"), 0, -Input.GetAxis("RVertical"))) * Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene(1);
        }
	}
}
