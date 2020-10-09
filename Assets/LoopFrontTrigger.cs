using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopFrontTrigger : MonoBehaviour {

	private float touchTime = 0;

	public float GetTouchTime() { return touchTime; }

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag.Equals("Ball")) {
			touchTime = Time.time;
		}
	}
}
