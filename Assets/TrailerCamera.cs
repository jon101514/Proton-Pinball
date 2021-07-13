using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerCamera : MonoBehaviour {

    float speed = 4f;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed);
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed);
    }
}
