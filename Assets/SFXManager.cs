using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

	public static SFXManager instance;

	public List<Mappable> soundList;

	private Dictionary<string, AudioClip> soundIndex;

	[Serializable]
	public struct Mappable {
		public string key;
		public AudioClip value;
	}

	private AudioSource audi;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		audi = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		soundIndex = new Dictionary<string, AudioClip>();
		for (int i = 0; i < soundList.Count; i++) {
			soundIndex[soundList[i].key] = soundList[i].value;
		}
	}

	public void PlaySoundGroup(string groupName) {
		if (groupName == "Superman") {
			Debug.Log("Then there's no time to waste");
			PlaySound("notime");
		}
	}

	public void PlaySound(string soundName) {
		AudioClip sound = soundIndex[soundName];
		if (sound != null) {
			audi.PlayOneShot(sound);
		}
	}
}
