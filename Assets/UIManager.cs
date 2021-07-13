using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// SINGLETON
	public static UIManager instance;

	// PUBLIC
	public Text scoreText;
	public Text ballText;
	public Text largeText;
	public Text timerText;
	public Text smallText;

	// PRIVATE
	private bool messaging = false;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}
		UpdateScoreUI();
	}
	
	public void UpdateScoreUI() {
		// scoreText.text = score.ToString("N", nfi);
		if (!messaging) {
			if (!IsModeActive()) { // If a mode is not currently active, display the score.
				timerText.enabled = false;
				smallText.enabled = false;
				scoreText.enabled = true;
			} else { // Otherwise, display the timer and small text not the score.
				timerText.enabled = true;
				smallText.enabled = true;
				scoreText.enabled = false;
			}
			scoreText.text = string.Format("{0:#,###0}", ScoreManager.instance.GetScore()) + "\n";
			ballText.text = " BALLS: " + GameManager.instance.GetBalls().ToString();
			largeText.enabled = false;
		}
	}

	public void StartMode(string mode) {
		// TODO
		Debug.Log("MODE STARTED");
		if (mode == "wario") {
			StartCoroutine(Message("WARIO VIDEO MODE", 3/2f));
			smallText.text = "USE FLIPPERS TO CHOOSE BUCKET";
		} else if (mode == "vod") {
			StartCoroutine(Message("SAVE THE VOD", 3/2f));
			smallText.text = "SHOOT COOKIE TO LOWER VOLUME";
		} else if (mode == "raid") {
			StartCoroutine(Message("RAID COUNTERMEASURES", 3/2f));
			smallText.text = "ALL SHOTS\nSCORE 2X";
		} else if (mode == "ernie") {
			StartCoroutine(Message("ERNIE'S MAGIC SHAPES", 3/2f));
			smallText.text = "SHOOT EGG TO BUILD SHAPES";
		} else if (mode == "orb") {
			StartCoroutine(Message("ORB 3D", 3/2f));
			smallText.text = "SHOOT RAMPS TO ATTACK";
		}
		timerText.enabled = true;
		smallText.enabled = true;
	}

	public void Tilt() {
		StopAllCoroutines();
		StartCoroutine(Message("TILT", 99f));
	}

	public void TiltWarning(string warning) {
		StopAllCoroutines();
		StartCoroutine(Message(warning, 3/2f));
	}

	public void Kickback() {
		StopAllCoroutines();
		StartCoroutine(Message("BALL SAVED\nDON'T MOVE", 3/2f));
	}

	public void ModeMessage(string msg, float len = 3/2f) {
		StopAllCoroutines();
		StartCoroutine(Message(msg, len));
	}

	public void GenericMessage(string msg, float len = 3/2f) {
		if (messaging) { return; }
		StopAllCoroutines();
		StartCoroutine(Message(msg, len));
	}

	private IEnumerator Message(string msg, float len) {
		messaging = true;
		scoreText.enabled = false;
		ballText.enabled = false; 
		timerText.enabled = false;
		smallText.enabled = false;
		largeText.enabled = true;
		largeText.text = msg;
		yield return new WaitForSeconds(len);
		messaging = false;
		ballText.enabled = true; 
		largeText.enabled = false;
		if (IsModeActive()) { // If mode is currently active, show the timer/smalltext
			timerText.enabled = true;
			smallText.enabled = true;
			scoreText.enabled = false;	
		} else {  // Otherwise, hide them.
			timerText.enabled = false;
			smallText.enabled = false;	
			scoreText.enabled = true;
		}
	}

	/** Whether or not a mode is currently in play.
	* */
	private bool IsModeActive() {
		if (ModeManager.instance.GetMode().Trim() == "") { // No mode, false
			return false; 
		} 
		return true;
	}
}
