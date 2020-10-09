using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// SINGLETON
	public static GameManager instance;

	// PUBLIC
	public GameObject ballPrefab;
	public Vector3 instaFlipPos;
	public Animator cameraAnim; // For swapping between the attract mode camera and play.

	// PRIVATE
	private int balls = 3;
	private string gameState = "menu"; // "over", "game", "launch", "endofball", "menu"
	[SerializeField]
	private int ballsInPlay = 1;
	[SerializeField]
	private float timer;
	private bool kickback = false;
	private int ballsLocked = 0;

	// CONSTANT
	private const float GRACE_PERIOD = 20f;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	/** Starts the game off by resetting all the pertinent values (balls, balls in play, tilts, game state)
	 *
	 */
	public void StartGame() {
		cameraAnim.SetBool("playing", true);
		balls = 3;
		SpawnBall();
	}

	/** Reset all the round's values and adds a ball to the game.
	 * param[instaFlip = false] - if true, adds a ball to the instaflip position and prevents prelaunch music from playing.
	 */
	public void SpawnBall(bool instaFlip = false) {
		Tilt.instance.ResetTilts();
		// Reset the flippers
		Flipper[] flippers = FindObjectsOfType<Flipper>();
		for (int i = 0; i < flippers.Length; i++) {
			flippers[i].SetCanFlip(true);
		}
		Door.instance.Open();
		SetGameState("launch");
		// In launching, set the camera to see the launcher.
		cameraAnim.SetBool("launching", true);
		ballsInPlay++;
		if (instaFlip) {
			Instantiate(ballPrefab, instaFlipPos, Quaternion.identity);
			return;
		} else {
			Instantiate(ballPrefab, instaFlipPos, Quaternion.identity);
		}
		if (balls == 3) {
			MusicPlayer.instance.PlayAudio("Prelaunch");
		}
	}

	/** Destroy the ball that's passed in and reduce the amount of balls in play.
	 * Then, checks if the player's current ball has ended.
	 */
	public void DestroyBall(GameObject ball) {
		Destroy(ball);
		ballsInPlay--;
		EndOfBall();
	}

	/** Either kicks the ball back or sets the game state to 'endofball', docks one ball, and checks if it's game over.
	 * 
	 */
	public void EndOfBall() {
		if (timer <= GRACE_PERIOD && !kickback && ModeManager.instance.GetMode() != "") {
			StartCoroutine(Kickback());
			return;
		} 
		kickback = false;
		SetGameState("endofball");
		ModeManager.instance.EndMode();
		balls--;
		ballsLocked = 0;
		StartCoroutine(CheckGameOver());
		ScoreManager.instance.AddToScore(0);
	}

	public int GetBalls() { return balls; }

	// Can be "over", "game", "launch", "endofball", "menu"
	public string GetGameState() { return gameState; }

	// Can be "over", "game", "launch", "endofball", "menu"
	public void SetGameState(string pGameState) { 
		gameState = pGameState; Debug.Log("GAMESTATE IS: " + gameState);
		if (gameState == "game") {
			cameraAnim.SetBool("launching", false);
		}
	}

	public void ResetTimer() { timer = 0f; }

	public void LockBall() {
		UIManager.instance.GenericMessage("BALL LOCKED");
		ballsLocked++;
		StartCoroutine(Kickback());
		if (ballsLocked >= 2) {
			StartCoroutine(MultiballPour());
		}	
	}

	private IEnumerator MultiballPour() {
		yield return new WaitForSeconds(3f);
		Bucket.instance.Pour();
		yield return new WaitForSeconds(3f);
		Pinball[] ballRA = FindObjectsOfType<Pinball>();
		foreach (Pinball ball in ballRA) {
			ball.Unlock();
		}
		MusicPlayer.instance.PlayAudio("Multiball");
	}

	private void Update() {
		if (gameState == "game") {
			timer += Time.deltaTime;
		}
	}

	private IEnumerator Kickback() {
		kickback = true;
		SpawnBall(true);
		UIManager.instance.Kickback();
		yield return new WaitForSeconds(1f);
		FindObjectOfType<Launcher>().Launch();
	}

	private IEnumerator CheckGameOver() {
		if (balls <= 0 && gameState != "over") { // Game is over
			MusicPlayer.instance.PlayAudio("End Of Ball");
			ScoreManager.instance.EndOfBallBonus();
			yield return new WaitForSecondsRealtime(MusicPlayer.instance.GetSongLength());
			GameOver();
		} else { // Go to next ball
			MusicPlayer.instance.PlayAudio("End Of Ball");
			ScoreManager.instance.EndOfBallBonus();
			yield return new WaitForSecondsRealtime(MusicPlayer.instance.GetSongLength());
			SpawnBall();
		}
	}

	private void GameOver() {
		Debug.Log("GAME OVER");
		cameraAnim.SetBool("playing", false);
		cameraAnim.SetBool("launching", false);
		MusicPlayer.instance.PlayAudio("End Of Game");
		ballsInPlay = 0;
		SetGameState("over");
		MenuManager.instance.EndGame();
		Bucket.instance.Pour();
		ScoreManager.instance.ResetScore();
	}

}
