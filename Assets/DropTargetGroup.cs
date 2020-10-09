using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetGroup : MonoBehaviour {

	public DropTarget[] targets;

	public int allDownScore;

	private void Start() {
		for (int i = 0; i < targets.Length; i++) {
			targets[i].SetGroup(this);
		}
	}

	public void CheckForAllDown() {
		for (int i = 0; i < targets.Length; i++) {
			if (targets[i].GetIsUp()) {
				return;
			}
		}
		PopAllUp();
	}

	private void PopAllUp() {
		for (int i = 0; i < targets.Length; i++) {
			targets[i].Popup();
		}
		ScoreManager.instance.AddToScore(allDownScore);
	}
}
