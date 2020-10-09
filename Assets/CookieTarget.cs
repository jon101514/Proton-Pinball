using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieTarget : MonoBehaviour {

	public int points = 1000;
	public CookieAnim[] cookies;

	private int hp = 2;

	private void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag.Equals("Ball")) {

			ScoreManager.instance.AddToScore(points);
			hp--;
			if (hp > 1) {
				UIManager.instance.GenericMessage(hp + " COOKIE HITS\nTO START MODE");
			} else if (hp == 1) {
				UIManager.instance.GenericMessage("ONE MORE HIT\nTO START MODE");
			}
			else if (hp == 0) {	
				ModeManager.instance.StartMode();
				for (int i = 0; i < cookies.Length; i++) {
					cookies[i].Open();
				}	
				StartCoroutine(CloseCookie());
			}
		}
	}

	private IEnumerator CloseCookie() {
		yield return new WaitForSecondsRealtime(1.0f);
		for (int i = 0; i < cookies.Length; i++) {
			cookies[i].Close();
		}	
		hp = 2;
	}
}
