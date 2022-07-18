/** 
 *	This debug printer prints the status of variables constantly, de-cluttering the console.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import this to display/manipulate text

public class DebugPrinter : MonoBehaviour {

	// SINGLETON
	public static DebugPrinter instance;

	// PUBLIC
	public bool debugOn;
	public GameObject debugPanel;
	public Text debugText;

	// PRIVATE
	private string recentTarget = "";

	// CONSTANT
	private const float REFRESH_TIME = 1/30f;

	/* Set up the Singleton design pattern. */
	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	/* If we want to display the debug panel, then we show it and start the update loop. */
	private void Start() {
		if (debugOn) {
			debugPanel.SetActive(true);
			InvokeRepeating("UpdateText", REFRESH_TIME, REFRESH_TIME);
		} else {
			debugPanel.SetActive(false);
		}
	}

	/* Public function that lets us know the most recent target that's been hit.
	   Called by ModeManager.
	   pRecent - the string ID of the target that's been most recently hit. */
	public void SetRecentTarget(string pRecent) {
		recentTarget = pRecent;
	}

	/* */
	private void UpdateText() {
		string debug = "";
		debug += "--GAME MANAGER--\n";
		debug += "BallsInPlay: " + GameManager.instance.GetBallsInPlay() + "\tGameState: " + GameManager.instance.GetGameState() + "\tTimer: " + GameManager.instance.GetTimer().ToString("#.##") + "\n";
		debug += "Kickback: " + GameManager.instance.GetKickback() + "\tBallsLocked: " + GameManager.instance.GetBallsLocked() + "\tBalls: " + GameManager.instance.GetBalls() + "\n";
		debug += "\n--MODE MANAGER--\n";
		debug += "RemainingModes: {" + PrintList(ModeManager.instance.GetRemainingModes()) + "}\nCompletedModes: {" + PrintList(ModeManager.instance.GetCompletedModes()) + "}\tCurrentMode: " + ModeManager.instance.GetMode() + "\n";
		debug += "\n--SCORE MANAGER--\n";
		debug += "Score: " + ScoreManager.instance.GetScore() + "\tJetHits: " + ScoreManager.instance.GetJetHits();  
		debug += "\n--TARGET HIT--\n";
		debug += recentTarget;
		debugText.text = debug;
	}

	private string PrintList(List<string> ls) {
		string result = "";
		foreach (string s in ls) { result += s + ", "; }
		return result;
	}

}
