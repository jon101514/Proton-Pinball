using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour {

	public static Tilt instance;

	private int tilts = 4;
	private float tiltTimer = 60f;
	private float inputTimer = 1/2f;

	private const float ANIM_TIME = 0.133f;
	private const float TILT_TIME = 20f; // Gives a new tilt every interval this long (in sec)
	private const float INPUT_TIME = 1/2f; // If holding a tilt on joystick, amount of time it takes for the tilt action to repeat.

	private Animator anim;
	private Transform tm;


	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		anim = GetComponent<Animator>();
		tm = GetComponent<Transform>();
	}

	// Update is called once per frame
	private void Update () {
		if ((Input.GetKeyDown(KeyCode.A) || Input.GetAxis("Horizontal") <= -0.5f) && inputTimer <= 0f) {
			Tilter(-1);
		} else if ((Input.GetKeyDown(KeyCode.Semicolon) || Input.GetAxis("Horizontal") >= 0.5f) && inputTimer <= 0f) {
			Tilter(1);
		} else if ((Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.H) || Mathf.Abs(Input.GetAxis("Vertical")) >= 0.5f) && inputTimer <= 0f) {
			Tilter(2);
		}
		// Manage the tilt and input timers.
		tiltTimer -= Time.deltaTime;
		inputTimer -= Time.deltaTime;
		if (tiltTimer <= 0 && tilts < 4) { // Add a new tilt.
			tiltTimer = TILT_TIME;
			tilts++;
		}
	}


	public void ResetTilts() {
		tilts = 4;
	}

	public bool GetInputTimerLessThanZero() {
		return inputTimer <= 0;
	}

	private void Tilter(int dir) {
		if (GameManager.instance.GetGameState() != "game") {
			return;
		} else if (tilts <= 0) {
			UIManager.instance.Tilt();
			Flipper[] flippers = FindObjectsOfType<Flipper>();
			for (int i = 0; i < flippers.Length; i++) {
				flippers[i].SetCanFlip(false);
			}
		} else if (tilts == 1) {
			UIManager.instance.TiltWarning("CAUTION!\nONE TILT LEFT");
		} else if (tilts == 2) {
			UIManager.instance.TiltWarning("CAUTION!\nTWO TILTS LEFT");
		} 
		inputTimer = INPUT_TIME;
		tilts--;
		anim.SetBool("canTilt", true);
		anim.SetInteger("tiltDir", dir);
		CancelInvoke();
		Invoke("AnimReset", ANIM_TIME);
	}

	private void AnimReset() {
		anim.SetBool("canTilt", false);
		anim.SetInteger("tiltDir", 0);
	}
}
