﻿using System.Collections;
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
			timerText.enabled = false;
			smallText.enabled = false;
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
			smallText.text = "SHOOT EGG TO LOWER VOLUME";
		} else if (mode == "raid") {
			StartCoroutine(Message("RAID COUNTERMEASURES", 3/2f));
			smallText.text = "ALL SHOTS 2X";
		} else if (mode == "ernie") {
			StartCoroutine(Message("ERNIE'S MAGIC SHAPES", 3/2f));
			smallText.text = "SHOOT COOKIE TO BUILD SHAPES";
		} else if (mode == "orb") {
			StartCoroutine(Message("ORB 3D", 3/2f));
			smallText.text = "SHOOT RAMPS TO ATTACK";
		}
		timerText.enabled = true;
		smallText.enabled = true;
		Debug.Log("DISPLAY MESSAGE");
		Debug.Log("DISPLAY ANIMATION");
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

	public void GenericMessage(string msg) {
		StopAllCoroutines();
		StartCoroutine(Message(msg, 3/2f));
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
		scoreText.enabled = true;
		ballText.enabled = true; 
		largeText.enabled = false;
		if (ModeManager.instance.GetMode() != "") {
			timerText.enabled = true;
			smallText.enabled = true;	
		}
	}
}
