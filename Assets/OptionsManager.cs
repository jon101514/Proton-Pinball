using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {

	public Slider volSlider;
	public Slider revSlider;

	private AudioSource[] audis;

	private void Awake() {
		volSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
		revSlider.value = PlayerPrefs.GetFloat("reverb", 0.5f);
		audis = FindObjectsOfType<AudioSource>();
	}

	private void Start() {
		volSlider.onValueChanged.AddListener(delegate {VolumeChanged();});
		revSlider.onValueChanged.AddListener(delegate {ReverbChanged();});
		foreach (AudioSource audi in audis) {
			audi.volume = volSlider.value;
			audi.reverbZoneMix = revSlider.value;
		}
	}

	private void VolumeChanged() {
		PlayerPrefs.SetFloat("volume", volSlider.value);
		foreach (AudioSource audi in audis) {
			audi.volume = volSlider.value;
		}
	}

	private void ReverbChanged() {
		PlayerPrefs.SetFloat("reverb", revSlider.value);
		foreach (AudioSource audi in audis) {
			audi.reverbZoneMix = revSlider.value;
		}
	}
}
