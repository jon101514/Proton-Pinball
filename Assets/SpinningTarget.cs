using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTarget : MonoBehaviour {

	public int pointsPerSpin;

	private float degreesToSpin = 360f;
	private float prevXRot;
	private int loops = 0;

	public int GetLoops() { return loops; }

	public void ResetLoops() { loops = 0; }

	private void FixedUpdate() {
		degreesToSpin -= Mathf.Abs(transform.rotation.eulerAngles.x - prevXRot);
		prevXRot = transform.rotation.eulerAngles.x;
		if (degreesToSpin <= 0) { // Adds points to score every revolution
			ScoreManager.instance.AddToScore(pointsPerSpin);
			degreesToSpin = 360f + (degreesToSpin);
			loops++;
		}
	}
	/*
	private void OnCollisionExit(Collision coll) {
		if (coll.gameObject.tag == "Ball") {
			StopAllCoroutines();
			StartCoroutine(SpinReset());
		}
	}

	private IEnumerator SpinReset() {
		yield return new WaitForSecondsRealtime(1.0f);
		anim.SetBool("reset", true);
		yield return new WaitForSecondsRealtime(1.0f);
		anim.SetBool("reset", false);
	}
	*/
}
