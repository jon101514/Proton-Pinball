using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {

	private Coroutine coroutine;

	private SpriteRenderer sr;

	private void Awake() {
		sr = GetComponent<SpriteRenderer>();
	}

	private void Start() {
		SetOnOff(true);
	}

	public void SetOnOff(bool pOn) {
		sr.enabled = pOn;
	}

	public void StartBlinking(float speed = 1/5f) {
		coroutine = StartCoroutine(Blinking(speed));
	}

	public void StopBlinking() {
		StopCoroutine(coroutine);
		sr.enabled = false;
	}

	private IEnumerator Blinking(float speed) {
		while (true) {
			sr.enabled = !sr.enabled;
			yield return new WaitForSeconds(speed);
		}
	}
}
