using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour {

	[Tooltip("Button for this flipper.")]
	public KeyCode button;
	public string controllerButton;
	public int dir; // dir -1 left, dir +1 right

	public float restPos = 0f;
	public float pressedPos = 45f;
	public float hitStrength = 4096f;
	public float flipperDamper = 128f;

	private const float DEAD_ZONE = 0.75f;

	HingeJoint hinge;

	private bool canFlip = true;

	private void Awake() {
		hinge = GetComponent<HingeJoint>();
		hinge.useSpring = true;	
	}

	void Update() {
		JointSpring spring = new JointSpring();
		spring.spring = hitStrength;
		spring.damper = flipperDamper;
		if ((Input.GetKey(button) || Input.GetButton(controllerButton) || FlipperAxis() ) && canFlip) {
			spring.targetPosition = pressedPos;
		} else {
			spring.targetPosition = restPos;
		}
		hinge.spring = spring;
		hinge.useLimits = true;
	}

	public void SetCanFlip(bool pCanFlip) {
		canFlip = pCanFlip;
	}

	private bool FlipperAxis() {

		if (dir == -1) { // Left
			if (Input.GetAxis("Left Flipper") > DEAD_ZONE) {
				return true;
			}
			return false;
		} else { // Right 
			if (Input.GetAxis("Right Flipper") > DEAD_ZONE) {
				return true;
			}
			return false;
		}
	}

	/*private Animator anim;

	private void Awake() {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(button)) {
			Debug.Log("Button Hit");
			anim.SetBool("flipUp", true);
			anim.SetTrigger("flip");
		}
		if (Input.GetKeyUp(button)) {
			Debug.Log("Button Hit");
			anim.SetBool("flipUp", false);
			anim.SetTrigger("flip");
		}
	}*/
}
