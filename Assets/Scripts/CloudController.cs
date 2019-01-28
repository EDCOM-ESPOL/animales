using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

    public float cloudSpeed = 0.5f;

	// Use this for initialization
	void Start () {
        //Debug.Log(transform.position.x);
        //Debug.Log(transform.localPosition.x);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(transform.position.x);

        transform.localPosition = new Vector3 (transform.localPosition.x + cloudSpeed, transform.localPosition.y, 0f);

        if (transform.localPosition.x >= 1200f)
        {
            transform.localPosition = new Vector3(-1200f, transform.localPosition.y, 0f);
        }
	}
}
