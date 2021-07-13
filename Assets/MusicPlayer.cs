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
    public AudioClip hotd;

	public AudioClip wizardIntro, wizard;
	public AudioClip endOfBall;
	public AudioClip endOfGame;

	public AudioClip bmanIntro, bman;

	// Fanfares
	public AudioClip vodFanfare;
	public AudioClip orbFanfare;
	public AudioClip warioFanfare;
	public AudioClip ernieFanfare;
	public AudioClip raidFanfare;
    public AudioClip hotdFanfare;

    //UNUSED
    public AudioClip pokey;

	private float songLength;

	private AudioSource audi;
	private AudioClip prevMain;

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
			prevMain = main1;
			songLength = main1.length + main1Intro.length;
			StartCoroutine(LoopAudio(main1, main1Intro));
		} else if (songName == "Main 2") {
			prevMain = main2;
			songLength = main2.length + main2Intro.length;
			StartCoroutine(LoopAudio(main2, main2Intro));
		} else if (songName == "Wizard") {
			prevMain = wizard;
			songLength = wizard.length + wizardIntro.length;
			StartCoroutine(LoopAudio(wizard, wizardIntro));
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
		} else if (songName == "Bomberman") {
			prevMain = bman;
			songLength = bman.length + bmanIntro.length;
			StartCoroutine(LoopAudio(bman, bmanIntro));
        }  else if (songName == "Pokey") {
			songLength = pokey.length;
			StartCoroutine(LoopAudio(pokey));
        } else if (songName == "HOTD") {
            songLength = hotd.length;
            StartCoroutine(LoopAudio(hotd));
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
		} else if (songName == "SMRPG" || songName == "Raid Fanfare") {
			songLength = raidFanfare.length;
			audi.PlayOneShot(raidFanfare);
			if (!soundTest) {
				StartCoroutine(ResumeMusicDelay(raidFanfare.length));
			}
		} else if (songName == "Ace Of Spades Fanfare") {
			songLength = vodFanfare.length;
			audi.PlayOneShot(vodFanfare);
			if (!soundTest) {
				StartCoroutine(ResumeMusicDelay(vodFanfare.length));
			}
		} else if (songName == "Orb Fanfare") {
			songLength = orbFanfare.length;
			audi.PlayOneShot(orbFanfare);
			if (!soundTest) {
				StartCoroutine(ResumeMusicDelay(orbFanfare.length));
			}
		} else if (songName == "Wario Fanfare") {
			songLength = warioFanfare.length;
			audi.PlayOneShot(warioFanfare);
			if (!soundTest) {
				StartCoroutine(ResumeMusicDelay(warioFanfare.length));
			}
		} else if (songName == "Ernie Fanfare") {
			songLength = ernieFanfare.length;
			audi.PlayOneShot(ernieFanfare);
			if (!soundTest) {
				StartCoroutine(ResumeMusicDelay(ernieFanfare.length));
			}
		} else if (songName == "HOTD Fanfare") {
            songLength = hotdFanfare.length;
            audi.PlayOneShot(hotdFanfare);
            if (!soundTest) {
                StartCoroutine(ResumeMusicDelay(hotdFanfare.length));
            }
        }
    }

	private IEnumerator ResumeMusicDelay(float len) {
		yield return new WaitForSeconds(len);
		ResumeMusic();
	}

	public void ResumeMusic() {
		StopAllCoroutines();
		audi.Stop();
		if (prevMain == main1) {
			songLength = main1.length;
			StartCoroutine(LoopAudio(main1));
		} else {  // prevMain == main2
			songLength = main2.length;
			StartCoroutine(LoopAudio(main2));
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
