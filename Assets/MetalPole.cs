using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPole : MonoBehaviour {

	private Animator anim;

	private void Awake() {
		anim = GetComponent<Animator>();
	}

	public void Wobble() {
		int rand = Random.Range(1, 4);
		anim.SetInteger("animation", rand);
		anim.SetTrigger("play");
	}
}
