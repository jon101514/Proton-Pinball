using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public static MenuManager instance;

	public GameObject menuCanvas;
	public GameObject buttons;
	public GameObject optionsMenu;
	public Canvas gameCanvas;


	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	private void Start() {
		EndGame();
	}

	public void StartGame() {
		menuCanvas.SetActive(false);
		optionsMenu.SetActive(false);
		gameCanvas.enabled = true;
		GameManager.instance.StartGame();
	}

	public void Options() {
		buttons.SetActive(false);
		optionsMenu.SetActive(true);
	}

	public void EndGame() {
		menuCanvas.SetActive(true);
		buttons.SetActive(true);
		optionsMenu.SetActive(false);
		gameCanvas.enabled = false;
		GameManager.instance.SetGameState("menu");
	}

	public void QuitGame() {
		StartCoroutine(FadeToEnd());
	}

	private IEnumerator FadeToEnd() {
		FindObjectOfType<SceneManagerObject>().StartFadeout(3/2f);
		yield return new WaitForSeconds(3/2f);
		Application.Quit();
	}

}
