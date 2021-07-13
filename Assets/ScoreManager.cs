﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	// SINGLETON
	public static ScoreManager instance;

	// PRIVATE
	private int score = 0;
	private int jetHits = 0;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	/** Adds a specified amount of points to the player's score. 
	 * int points - the number of points to add to the score.
	 */
	public void AddToScore(int points) {
		score += points;
		UIManager.instance.UpdateScoreUI();
	}

	/** Returns the score.
	 *
	 */
	public int GetScore() { return score; }
	public int GetJetHits() { return jetHits; }

	/** Resets the score back to 0 and lets the UI know as well.
	 *
	 */
	public void ResetScore() { 
		score = 0; 
		UIManager.instance.UpdateScoreUI();
	}

	/** Increase the number of jet hits by 1.
	 *
	 */
	public void JetHit() { jetHits++; }

	/** Counts the end-of-ball bonuses for modes/jets/spins.
	 * Note that this will also reset the amount of modes/jets/spins completed for this ball.
	 */
	public void EndOfBallBonus(int modesComplete) {
		StartCoroutine(CountBonuses(modesComplete));
	}	

	private IEnumerator CountBonuses(int modesComplete) {
		int totalBonus = 0;
		int modeBonus = 0;
		int jetBonus = 0;
		int spinBonus = 0;

		yield return new WaitForSeconds(0.084f);
		modeBonus = 5000 * modesComplete;
		UIManager.instance.GenericMessage("MODES\n5000 x " + modesComplete + " = " + PrintScore(modeBonus), 0.531f);
		yield return new WaitForSeconds(0.531f);
		jetBonus = 250 * jetHits;
		UIManager.instance.GenericMessage("JETS\n250 x " + jetHits + " = " + PrintScore(jetBonus), 0.445f);
		AddToScore(spinBonus);
		yield return new WaitForSeconds(0.445f);
		spinBonus = 100 * FindObjectOfType<SpinningTarget>().GetLoops();
		UIManager.instance.GenericMessage("SPINS\n100 x " + FindObjectOfType<SpinningTarget>().GetLoops() + " = " + PrintScore(spinBonus), 0.531f);
		AddToScore(spinBonus);
		yield return new WaitForSeconds(0.531f);
		UIManager.instance.GenericMessage("TOTAL BONUS:\n", 0.531f);
		yield return new WaitForSeconds(0.531f);
		totalBonus = modeBonus + jetBonus + spinBonus;
		UIManager.instance.GenericMessage("TOTAL BONUS:\n" + PrintScore(totalBonus), 1);
		yield return new WaitForSeconds(1f);
		// Reset the values as counted for the bonuses here.
		FindObjectOfType<SpinningTarget>().ResetLoops();
		ModeManager.instance.ResetModesCompleted();
		jetHits = 0;
		UIManager.instance.UpdateScoreUI();
	}

	private string PrintScore(int score) {
		return string.Format("{0:#,###0}", score);
	}

}
