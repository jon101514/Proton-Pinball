using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget : MonoBehaviour {


	// PRIVATE
	private DropTargetGroup group; // The DropTargetGroup this belongs to.
	private bool isUp = true;

	// COMPONENT
	private Animator anim;

	private void Awake() {
		anim = GetComponent<Animator>();
	}

	public void Popup() {
		isUp = true;
		anim.SetBool("isUp", true);
		Debug.Log("Popup");
	}

	public void SetGroup(DropTargetGroup pGroup) {
		group = pGroup;
	}

	public bool GetIsUp() {
		return isUp;
	}

	private void Dropdown() {
		isUp = false;
		anim.SetBool("isUp", false);
		group.CheckForAllDown();
	}

	private void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag.Equals("Ball") && isUp) {
			Dropdown();
		}
	}
}
