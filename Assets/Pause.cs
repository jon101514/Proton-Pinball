using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	public GameObject pauseMenu;

	private bool paused = false;
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.GetGameState() == "game") {
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause")) {
				paused = !paused;
				ResolvePause();
			}	
		}
	}

	private void ResolvePause() {
		MusicPlayer.instance.PauseAndUnpause(paused);
		pauseMenu.SetActive(paused);
		if (paused) {
			SFXManager.instance.PlaySound("pause");
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}
}
