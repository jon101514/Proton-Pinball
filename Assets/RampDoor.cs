using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampDoor : MonoBehaviour {

	public static RampDoor instance;

	public Animator anim;

	private float timer = 0f;

	private const float TIME_LIMIT = 2f;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	private void Update() {
		timer += Time.deltaTime;
	}

	/* When a mode starts, make sure the door's in ramp position to avoid triggering multiball during said mode. */
	public void ModeDoorLock() {
		if (anim.GetBool("isLeft") == true) { 
			Close();
		}
	}

	public void Switch() {
		Debug.Log("Switch");
		if (timer <= TIME_LIMIT) {
			return;
		}
		timer = 0f;
		if (anim.GetBool("isLeft") == true) { // If the ball will go into the bucket
			Close();
		} else { // If the ball will go down the ramp
			if (!ModeActive()) { // If there are no modes currently happening, then we can swing the door the other side.
				Open();
			}
		}
	}

	// The ball goes to the bucket to the right.
	public void Open() {
		anim.SetBool("isLeft", true);
	}

	// The ball goes down the ramp to the left.
	public void Close() {
		anim.SetBool("isLeft", false);
	}

	public bool ModeActive() {
		return ModeManager.instance.GetMode() != "";
	}
}
