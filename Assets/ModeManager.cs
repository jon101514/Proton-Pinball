using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour {

	public static ModeManager instance;

	public Text timerText;
	public LightControl modeLight;
	public LightControl ernieLight;
	public LightControl orbLight;
	public LightControl raidLight;
	public LightControl wizLight;
	public LightControl warioLight;

	private bool timerOn = false;
	private float timer;
	private string currentMode = "";
	private List<string> completedModes;
	private List<string> remainingModes;

	// Number of modes completed
	private int modesCompleted = 0;

	// Individual mode variables
	private int vodHP = 3;
	private int orbLHP = 2;
	private int orbRHP = 2;
	private int ernieShapes = 6;

	private const float VOD_TIME = 60f;
	private const float ERNIE_TIME = 60f;
	private const float ORB_TIME = 60f;
	private const float RAID_TIME = 60f;
	private const float WIZ_TIME = 120f;

	private const int VOD_BONUS = 30000;
	private const int ORB_BONUS = 50000;
	private const int ERNIE_BONUS = 60000;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		completedModes = new List<string>();
		remainingModes = new List<string>();
		remainingModes.Add("wario");
		remainingModes.Add("orb");
		remainingModes.Add("ernie");
		remainingModes.Add("vod");
		remainingModes.Add("raid");
	}

	/** By starting a mode, lets the DMD know, sets the current mode, starts the timer, plays the appropriate music.
	 * "wario" "vod" "ernie" "orb" "raid" "wiz"
	 */
	public void StartMode() {
		Debug.Log("[*]-START MODE-[*]");
		modesCompleted++;
		RampDoor.instance.ModeDoorLock(); // Ensure that the door on the left ramp is locked so that balls always go down the ramp and not down the bucket.
		if (remainingModes.Count == 0) { // All modes completed; wizard mode activates
			WizardMode();
			ResetModeLists();
			return;
		}
		string mode = remainingModes[Random.Range(0, remainingModes.Count)];
		remainingModes.Remove(mode);
		completedModes.Add(mode);
		// No timer for Wario mode.
		if (mode == "wario") {
			MusicPlayer.instance.PlayAudio("Wario");
		} else {
			// if (mode == "vod") { // SAVE THE VOD
			// 	MusicPlayer.instance.PlayAudio("Save The VOD");
			// 	vodHP = 3;
			// 	timer = VOD_TIME;
			// HOTD2 mode used to be "SAVE THE VOD" using Ace of Spades 64.
			if (mode == "vod") { // REPLACE VOD with HOTD2
				MusicPlayer.instance.PlayAudio("HOTD");
				vodHP = 3;
				timer = VOD_TIME;
			} else if (mode == "ernie") { // ERNIE'S MAGIC SHAPES
				MusicPlayer.instance.PlayAudio("Ernie");
				ernieShapes = 6;
				timer = ERNIE_TIME;
			} else if (mode == "orb") { // ORB-3D
				MusicPlayer.instance.PlayAudio("Orb 3D");
				orbLHP = 2;
				orbRHP = 2;
				timer = ORB_TIME;
			} else if (mode == "raid") { // RAID COUNTERMEASURES
				MusicPlayer.instance.PlayAudio("Countermeasures");
				timer = RAID_TIME;
			}
			timerOn = true;
		}
		UIManager.instance.StartMode(mode);
		currentMode = mode;
	}

	public void TargetHit(string targetID, int score) {
		DebugPrinter.instance.SetRecentTarget(targetID); // Let Debug Printer know what's been hit.
		switch (currentMode) { // Depending on the mode...
			case "wario": // Not developed yet.
				EndMode();
				break;
			case "vod": // Hit the cookie 3 times.
				if (targetID == "COOK") {
					VODDamage();
				}
				break;
			case "ernie": // Hit the egg 6 times.
				if (targetID == "RT") {
					ErnieMode();
				}
				break;
			case "orb": // Shoot both ramps 2X each.
				OrbDamage(targetID);
				break;
			case "raid": // All targets are worth double.
				ScoreManager.instance.AddToScore(score);
				break;
			default:
				break;
		}
	}

	public void Multiball() {
		currentMode = "multiball";
	}

	public void EndMultiball() {
		EndMode();
	}

	/* While Ernie mode is in play, hit the egg to build the shape. Egg must be hit 6 times to score the bonus. */
	private void ErnieMode() {
		ernieShapes--;
		if (ernieShapes == 1) {
			UIManager.instance.ModeMessage("ONE MORE HIT");
		}
		if (ernieShapes <= 0) { // Win condition
			ScoreManager.instance.AddToScore(ERNIE_BONUS);
			UIManager.instance.ModeMessage("IT'S A SUPER CROWN!\n60,000 POINTS");
			MusicPlayer.instance.PlayAudio("Ernie Fanfare");
			EndMode(false);
		}
	}

	/* During Orb3D, the left and right ramps must be hit 2X each to score the bonus.*/
	private void OrbDamage(string targetID) {
		if (targetID == "LR" && orbLHP > 0) {
			orbLHP--;
		} else if (targetID == "RR" && orbRHP > 0) {
			orbRHP--;
		}
		if ((targetID == "LR" && orbLHP <= 0) || (targetID == "RR" && orbRHP <= 0)) { // They go for an eye that's already been completed
			UIManager.instance.ModeMessage("HEAD FOR THE\nOTHER RAMP!");
		} else if (targetID == "LR" || targetID == "RR") {
			UIManager.instance.ModeMessage("LEFT EYE: " + orbLHP + " HP\n" + "RIGHT EYE: " + orbRHP + " HP");
		}
		if (orbLHP <= 0 && orbRHP <= 0) { // Win condition
			ScoreManager.instance.AddToScore(ORB_BONUS);
			UIManager.instance.ModeMessage("ORB-3D CLEARED\n50,000 POINTS");
			MusicPlayer.instance.PlayAudio("Orb Fanfare");
			EndMode(false);
		}

	}

	/* When the cookie is hit during Save the VOD/HOTD2, subtract HP and
	if HP is 0 or less, give players a bonus and end the mode.*/
	private void VODDamage() {
		vodHP--;
		if (vodHP == 2) {
			UIManager.instance.ModeMessage("TWO MORE HITS");
		} else if (vodHP == 1) {
			UIManager.instance.ModeMessage("ONE MORE HIT");
		}
		if (vodHP <= 0) { // Win condition
			ScoreManager.instance.AddToScore(VOD_BONUS);
			// Changed the win message to match HOTD2.
			UIManager.instance.ModeMessage("HOTD2 DONE!\n30,000 POINTS");
			MusicPlayer.instance.PlayAudio("HOTD Fanfare");
			EndMode(false);
		}
	}

	/* When Raid mode is over, play the Raid fanfare. */
	private void RaidEnd() {
		timerText.text = "0";
		currentMode = ""; 
		timerOn = false;
		UIManager.instance.ModeMessage("THANKS FOR THE RAID!");
		ScoreManager.instance.AddToScore(0); // Update the scoreboard.
		MusicPlayer.instance.PlayAudio("Raid Fanfare");
	}

	public void WizardMode() {
		MusicPlayer.instance.PlayAudio("Wizard");
		timer = WIZ_TIME;
	}

	/** 
	 * "wario" "vod" "ernie" "orb" "raid" "wiz" or "" for no mode
	 */
	public string GetMode() { return currentMode; }

	public int GetModesCompleted() { return modesCompleted; }
	public void ResetModesCompleted() { modesCompleted = 0; }

	public List<string> GetRemainingModes() { return remainingModes; }
	public List<string> GetCompletedModes() { return completedModes; }

	/** Ends the current mode and sets it back to "".
	 *
	 */
	public void EndMode(bool needToResumeMusic = true) { 
		if (currentMode == "raid" && GameManager.instance.GetGameState() != "endofball") {
			RaidEnd();
			return;
		}
		timerText.text = "0";
		currentMode = ""; 
		timerOn = false;
		ScoreManager.instance.AddToScore(0); // Update the scoreboard.
		if (needToResumeMusic) {
			MusicPlayer.instance.ResumeMusic();
		}
	}

	private void Update() {
		if (timerOn) {
			timer -= Time.deltaTime;
			timerText.text = timer.ToString("#.");
			if (timer <= 0.5f) {
				EndMode();
			}
		}
	}

	private void ResetModeLists() {
		foreach (string pMode in completedModes) {
			remainingModes.Add(pMode);
		}
		completedModes.Clear();
	}

}
