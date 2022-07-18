using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBumper : MonoBehaviour {

	public Animator anim;

	/** When touched, expand the inner part of the bumper.
	 *
	 */
	private void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.CompareTag("Ball")) {
			Expand();
		}
	}

	public void Expand() {
		anim.SetTrigger("expand");

	}
}
