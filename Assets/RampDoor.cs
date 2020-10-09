using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampDoor : MonoBehaviour {

	public static RampDoor instance;

	public Animator anim;

	private float timer = 0f;

	private const float TIME_LIMIT = 1f;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	private void Update() {
		timer += Time.deltaTime;
	}

	public void Switch() {
		Debug.Log("Switch");
		if (timer <= TIME_LIMIT) {
			return;
		}
		timer = 0f;
		if (anim.GetBool("isLeft") == true) {
			Close();
		} else {
			Open();
		}
	}

	public void Open() {
		anim.SetBool("isLeft", true);
	}

	public void Close() {
		anim.SetBool("isLeft", false);
	}
}
