using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerDetector : MonoBehaviour {

	public Text controllerText;

	private string[] joysticks;
	private bool allBlank = true;
	private bool eligible = false;

	private const string X360_NAME = "Controller (XBOX 360 For Windows)";
	
	private void Start() {
		InvokeRepeating("CheckControllers", 1f, 1f);
		controllerText.text = "Try plugging in an XBOX 360 controller to play!";
	}

	private void CheckControllers() {
		joysticks = Input.GetJoystickNames();

		if (joysticks.Length >= 1) {
				allBlank = true;
				eligible = false;
				foreach (string joystick in joysticks) {
					
					if (joystick == X360_NAME) {
						controllerText.text = "You have a compatible controller plugged in!";
						eligible = true;
						return;
					} 
					if (joystick != "") {
						allBlank = false;
					}


				}
				if (allBlank) {
					controllerText.text = "Try plugging in an XBOX 360 controller to play!";
				} else if (!eligible) {
					controllerText.text = "Compatibility is not guaranteed with your controller(s).";
				}
			
			else {
				controllerText.text = "Try plugging in an XBOX 360 controller to play!";
			}
		} 
	}
}
