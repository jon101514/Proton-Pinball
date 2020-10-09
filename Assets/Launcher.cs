using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

	public Transform impact;

	private const float MIN_LAUNCH_FORCE = 165.5f;
	private const float MAX_LAUNCH_FORCE = 166f;

	private Rigidbody rb;
	private GameObject ball;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	public void Launch() {
		float force = Random.Range(MIN_LAUNCH_FORCE, MAX_LAUNCH_FORCE);
		Debug.Log("FORCE: " + force);
		ball.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 4f);	
	}

	private void Update() {
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit")) && ball != null && GameManager.instance.GetGameState() == "launch") {
			Launch();
		}
	}

	private void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Ball") {
			ball = coll.gameObject;
		}
	}

	private void OnCollisionExit(Collision coll) {
		if (coll.gameObject.tag == "Ball") {
			ball = null;
		}
	}
}
