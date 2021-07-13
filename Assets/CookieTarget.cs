using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieTarget : MonoBehaviour {

	public int points = 1000;
	public CookieAnim[] cookies;
    public BallMagnet mag;

	private int hp = 2;

	private void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag.Equals("Ball")) {
			ModeManager.instance.TargetHit("COOK", points);
			ScoreManager.instance.AddToScore(points);
			// If there's currently no mode in play, then take damage.
			if (ModeManager.instance.GetMode() == "") {
				hp--;
				if (hp > 1) {
					UIManager.instance.GenericMessage(hp + " COOKIE HITS\nTO START MODE");
				} else if (hp == 1) {
					UIManager.instance.GenericMessage("ONE MORE HIT\nTO START MODE");
				}
				else if (hp == 0) {	
					ModeManager.instance.StartMode();
                    mag.isOn = true;
                    for (int i = 0; i < cookies.Length; i++) {
						cookies[i].Open();
					}	
					StartCoroutine(CloseCookie());
				}
			}
		}
	}

	private IEnumerator CloseCookie() {
		yield return new WaitForSecondsRealtime(3/2f);
        mag.isOn = false;
        for (int i = 0; i < cookies.Length; i++) {
			cookies[i].Close();
		}	
		hp = 2;
	}
}
