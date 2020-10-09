using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public int points = 1000;
	public string callFunction;
	public MetalPole pole;

	private void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag.Equals("Ball")) {

			ScoreManager.instance.AddToScore(points);

			if (callFunction == "Multiball") {
				Bucket.instance.Pour();
			} else if (callFunction == "Wobble") {
				pole.Wobble();
			} else if (callFunction == "Jet Hit") {
				ScoreManager.instance.JetHit();
			}
		}
	}

	private void OnTriggerEnter (Collider coll) {
		if (coll.gameObject.tag.Equals("Ball")) {

			ScoreManager.instance.AddToScore(points);

			if (callFunction == "Multiball") {
				Bucket.instance.Pour();
			} else if (callFunction == "Wobble") {
				pole.Wobble();
			} else if (callFunction == "Jet Hit") {
				ScoreManager.instance.JetHit();
			}
		}
	}
}
