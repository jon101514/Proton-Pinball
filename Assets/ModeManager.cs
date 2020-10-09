using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour {

	public static ModeManager instance;


	public Text timerText;

	private bool timerOn = false;
	private float timer;
	private string currentMode;
	private List<string> completedModes;
	private List<string> remainingModes;

	private const float VOD_TIME = 60f;
	private const float ERNIE_TIME = 60f;
	private const float ORB_TIME = 60f;
	private const float RAID_TIME = 60f;
	private const float WIZ_TIME = 60f;

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
			if (mode == "vod") { // SAVE THE VOD
				MusicPlayer.instance.PlayAudio("Save The VOD");
				timer = VOD_TIME;
			} else if (mode == "ernie") { // ERNIE'S MAGIC SHAPES
				MusicPlayer.instance.PlayAudio("Ernie");
				timer = ERNIE_TIME;
			} else if (mode == "orb") { // ORB-3D
				MusicPlayer.instance.PlayAudio("Orb 3D");
				timer = ORB_TIME;
			} else if (mode == "raid") { // RAID COUNTERMEASURES
				MusicPlayer.instance.PlayAudio("Countermeasures");
				timer = RAID_TIME;
			}
			UIManager.instance.StartMode(mode);
			currentMode = mode;
			timerOn = true;
		}
	}

	public void WizardMode() {
		MusicPlayer.instance.PlayAudio("Wizard");
		timer = WIZ_TIME;
	}

	/** 
	 * "wario" "vod" "ernie" "orb" "raid" "wiz" or "" for no mode
	 */
	public string GetMode() { return currentMode; }

	/** Ends the current mode and sets it back to "".
	 *
	 */
	public void EndMode() { currentMode = ""; }

	private void Update() {
		if (timerOn) {
			timer -= Time.deltaTime;
			timerText.text = timer.ToString("#.");
		}
	}

	private void ResetModeLists() {
		foreach (string pMode in completedModes) {
			remainingModes.Add(pMode);
		}
		completedModes.Clear();
	}

}
