using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	public static MusicPlayer instance;

	public AudioClip prelaunch;
	public AudioClip intermission;
	public AudioClip main1Intro, main1;
	public AudioClip main2Intro, main2;
	public AudioClip orb3d;

	public AudioClip saveVod;
	public AudioClip wario;
	public AudioClip countermeasuresIntro, countermeasures;
	public AudioClip ernie, ernieIntro;
	public AudioClip multiball, multiballIntro;
	public AudioClip ronaldinho;

	public AudioClip wizard;
	public AudioClip endOfBall;
	public AudioClip endOfGame;

	//UNUSED
	public AudioClip pokey, smrpg;

	private float songLength;

	private AudioSource audi;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		audi = GetComponent<AudioSource>();
		// PlayAudio("Prelaunch");
	}

	public void PlayAudio(string songName, bool soundTest = false) {
		StopAllCoroutines();
		audi.Stop();
		if (songName == "Prelaunch") {
			songLength = prelaunch.length;
			StartCoroutine(LoopAudio(prelaunch));
		} else if (songName == "Main" || songName == "Main 1") {
			songLength = main1.length + main1Intro.length;
			StartCoroutine(LoopAudio(main1, main1Intro));
		} else if (songName == "Main 2") {
			songLength = main2.length + main2Intro.length;
			StartCoroutine(LoopAudio(main2, main2Intro));
		} else if (songName == "Wizard") {
			songLength = wizard.length;
			StartCoroutine(LoopAudio(wizard));
		} else if (songName == "Intermission") {
			songLength = intermission.length;
			StartCoroutine(LoopAudio(intermission));
		} else if (songName == "Orb 3D") {
			songLength = orb3d.length;
			StartCoroutine(LoopAudio(orb3d));
		} else if (songName == "Save The VOD") {
			songLength = saveVod.length;
			StartCoroutine(LoopAudio(saveVod));
		} else if (songName == "Wario") {
			songLength = wario.length;
			StartCoroutine(LoopAudio(wario));
		} else if (songName == "Countermeasures") {
			songLength = countermeasures.length + countermeasuresIntro.length;
			StartCoroutine(LoopAudio(countermeasures, countermeasuresIntro));
		} else if (songName == "Ernie") {
			songLength = ernie.length + ernieIntro.length;
			StartCoroutine(LoopAudio(ernie, ernieIntro));
		} else if (songName == "Multiball") {
			songLength = multiball.length + multiballIntro.length;
			StartCoroutine(LoopAudio(multiball, multiballIntro));
		} else if (songName == "Pokey") {
			songLength = pokey.length;
			StartCoroutine(LoopAudio(pokey));
		}
		// One-Shots
		if (songName == "End Of Ball") {
			songLength = endOfBall.length;
			audi.PlayOneShot(endOfBall);
			if (!soundTest) {
				StartCoroutine(LoopAudio(intermission, null, endOfBall.length));
			}
		} else if (songName == "End Of Game") {
			songLength = endOfGame.length;
			audi.PlayOneShot(endOfGame);
		} else if (songName == "Ronaldinho") {
			songLength = ronaldinho.length;
			audi.PlayOneShot(ronaldinho);
		} else if (songName == "SMRPG") {
			songLength = smrpg.length;
			audi.PlayOneShot(smrpg);
		}

	}

	public float GetSongLength() {
		return songLength;
	}

	public void PauseAndUnpause(bool isPause) {
		if (isPause) {
			audi.Pause();
		} else {
			audi.UnPause();
		}
	}

	private IEnumerator LoopAudio(AudioClip song, AudioClip intro = null, float waitTime = 0f) {
		yield return new WaitForSeconds(waitTime);
		if (intro) {
			audi.PlayOneShot(intro);
			yield return new WaitForSeconds(intro.length);
		}
		while (true) {
			audi.PlayOneShot(song);
			yield return new WaitForSeconds(song.length);
		}
	}
}
