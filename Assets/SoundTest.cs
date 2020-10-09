using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundTest : MonoBehaviour {

	public List<Mappable> musicList;
	public Text songText;

	private Dictionary<string, string> musicIndex;
	private int index = 0;
	private float scrollTimer = 0;

	private const float SCROLL_TIME = 1/4f;

	[Serializable]
	public struct Mappable {
		public string key; // Song Name -- used to call MusicPlayer
		public string title;
		public string artist;
		public string info;
	}

	private AudioSource audi;

	private void Awake() {
		audi = GetComponent<AudioSource>();
		audi.volume = PlayerPrefs.GetFloat("volume", 0.5f);
		audi.reverbZoneMix = PlayerPrefs.GetFloat("reverb", 0.5f);
	}	

	private void Start() {
		DisplaySong();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.F) || (Input.GetAxis("Horizontal") <= -0.5f && scrollTimer <= 0f)) {
			index--;
			if (index < 0) {
				index = 0;
			}
			scrollTimer = SCROLL_TIME;
			DisplaySong();
		} else if (Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.J) || (Input.GetAxis("Horizontal") >= 0.5f && scrollTimer <= 0f)) {
			index++;
			if (index >= musicList.Count - 1) {
				index = musicList.Count - 1;
			}
			scrollTimer = SCROLL_TIME;
			DisplaySong();
		} else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit")) {
			PlaySong(musicList[index].key);
		} else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel") || Input.GetButtonDown("Pause")) {
			FindObjectOfType<SceneManagerObject>().GoToScene("Cookie Table");
		}
		scrollTimer -= Time.deltaTime;
	}

	private void DisplaySong() {
		songText.text = "(" + (index + 1) + " / " + musicList.Count + " ) " + musicList[index].title + "<size=36>\n" + musicList[index].artist + "</size><size=28>\n" + musicList[index].info + "</size>";
	}

	private void PlaySong(string songTitle) {
		MusicPlayer.instance.PlayAudio(songTitle, true);
	}
}
