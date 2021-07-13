using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinball : MonoBehaviour {

	private float force = 1/4f;
	private float kickback = 4f;
	private float forceball = 1.5f;
	private bool locked = false;

    private const float MAG_FORCE = 3f;

	private Rigidbody rb;
	private Transform tm;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		tm = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown(KeyCode.A) || Input.GetAxis("Horizontal") <= -0.5f) && GameManager.instance.GetGameState() == "game" && Tilt.instance.GetInputTimerLessThanZero()) {
			Debug.Log("HORI");
			rb.AddForce(Vector3.left * force, ForceMode.Impulse);
		} else if ((Input.GetKeyDown(KeyCode.Semicolon) || Input.GetAxis("Horizontal") >= 0.5f) && GameManager.instance.GetGameState() == "game" && Tilt.instance.GetInputTimerLessThanZero()) {
			Debug.Log("HORI");
			rb.AddForce(Vector3.right * force, ForceMode.Impulse);
		} else if ((Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.H) || Mathf.Abs(Input.GetAxis("Vertical")) >= 0.5f) && GameManager.instance.GetGameState() == "game" && Tilt.instance.GetInputTimerLessThanZero()) {
			Debug.Log("HORI");
			rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
		}

		if (Input.GetKeyDown(KeyCode.Q)) {
			rb.AddForce(Vector3.left * force, ForceMode.Impulse);
		} else if (Input.GetKeyDown(KeyCode.E)) {
			rb.AddForce(Vector3.right * force, ForceMode.Impulse);
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			rb.AddForce(Vector3.back * force, ForceMode.Impulse);
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
		}
	}

	public void Unlock() {
		locked = false;
	}

	private void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Bucket" && !locked) { // We're in the bucket.
			locked = true;
			GameManager.instance.LockBall();
		}
	}

	private void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.CompareTag("Drain")) {
			GameManager.instance.DestroyBall(this.gameObject);;
		} else if (coll.gameObject.CompareTag("Door Trigger")) {
			Door.instance.Close();
		} else if (coll.gameObject.CompareTag("Ramp Door Trigger")) {
			RampDoor.instance.Switch();
		} else if (coll.gameObject.CompareTag("Kickback")) {
			if (coll.GetComponent<Kickback>().GetCanKick()) {
				rb.AddForce(Vector3.forward * kickback, ForceMode.Impulse);
			}
		} else if (coll.gameObject.CompareTag("Force Ball")) {
			rb.AddForce(Vector3.forward * forceball, ForceMode.Impulse);
		} else if (coll.gameObject.CompareTag("Slow Ball")) {
			rb.velocity *= 0.875f;
		}
	}

    /* Pull towards the center of the magnet. */
    private void OnTriggerStay(Collider coll) {
        if (coll.gameObject.CompareTag("Ball Magnet")) {
            BallMagnet mag = coll.GetComponent<BallMagnet>();
            if (mag.isOn) {
                rb.AddForce((mag.transform.position - tm.position) * MAG_FORCE, ForceMode.Force);
            }
        }
    }

	private void OnTriggerExit(Collider coll) {
		
	}
}
