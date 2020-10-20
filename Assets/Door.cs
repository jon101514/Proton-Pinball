using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public static Door instance;

	private bool main2 = false;

	private Animator anim;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		anim = GetComponent<Animator>();
	}

	public void Open() {
		anim.SetBool("isOpen", true);
	}

	public void Close() {
		if (anim.GetBool("isOpen") == true && !GameManager.instance.GetKickback()) {
			GameManager.instance.ResetTimer();
			if (main2) {
				MusicPlayer.instance.PlayAudio("Main 2");
			} else {
				MusicPlayer.instance.PlayAudio("Main");
			}
			main2 = !main2;
		}
		GameManager.instance.SetGameState("game");
		anim.SetBool("isOpen", false);
	}
}
