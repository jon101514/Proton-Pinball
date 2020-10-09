using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackTrigger : MonoBehaviour {

	public LoopFrontTrigger front;

	private float touchTime;

	private const float TRIGGER_THRESHOLD = 1/4f;

	void OnTriggerEnter(Collider coll) {
		
		if (coll.gameObject.tag.Equals("Ball")) {
			Debug.Log("TOUCHED A PINBALL");
			touchTime = Time.time;
			Debug.Log(touchTime + "\t" + front.GetTouchTime());
			if (touchTime - front.GetTouchTime() <= TRIGGER_THRESHOLD) {
				SFXManager.instance.PlaySoundGroup("Superman");
			}
		}
	}
}
