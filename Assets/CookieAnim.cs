using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieAnim : MonoBehaviour {

	private Animator anim;

	private void Awake() {
		anim = GetComponent<Animator>();
	}

	public void Open() {
		anim.SetBool("isOpen", true);
		anim.SetTrigger("animate");
	}

	public void Close() {
		anim.SetBool("isOpen", false);
		anim.SetTrigger("animate");
	}
}
