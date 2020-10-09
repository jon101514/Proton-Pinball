using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerObject : MonoBehaviour {

	public Image fadeout;

	public void GoToScene(string sceneName) {
		StartCoroutine(SceneTransition(sceneName));
	}

	public void StartFadeout(float pFadeTime) {
		StartCoroutine(Fadeout(pFadeTime));
	}

	private IEnumerator Fadeout(float pFadeTime) {
		float fadeTimer = pFadeTime;
		while (fadeTimer >= 0) {
			fadeout.color = Color.Lerp(Color.clear, Color.black, (pFadeTime - fadeTimer) / (pFadeTime));
			fadeTimer -= Time.deltaTime;
			yield return new WaitForEndOfFrame();	
		}
	}

	private IEnumerator SceneTransition(string sceneName) {
		float fadeTimer = 3/4f;
		while (fadeTimer >= 0) {
			fadeout.color = Color.Lerp(Color.clear, Color.black, (3/4f - fadeTimer) / (3/4f));
			fadeTimer -= Time.deltaTime;
			yield return new WaitForEndOfFrame();	
		}
		SceneManager.LoadScene(sceneName);
	}
}
