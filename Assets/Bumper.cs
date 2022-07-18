using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

	[Range(0, 4)]
	public float xpForce = 2f;
	[Range(0, 8f)]
	public float xpRad = 4f;
	public Vector3 forceDir;

	/** Apply forces to the ball when touched.
	 *
	 */
	private void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.CompareTag("Ball")) {
			// Debug.Log("BALL BUMPER");
			if (forceDir == Vector3.zero) {
				coll.gameObject.GetComponent<Rigidbody>().AddExplosionForce(xpForce, transform.position, xpRad, 0.0f, ForceMode.Impulse);
			} else {
				coll.gameObject.GetComponent<Rigidbody>().AddForce(forceDir.normalized * xpForce, ForceMode.Impulse);
			}
		}
	}
}
