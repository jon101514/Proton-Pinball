using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

	public List<Button> buttons;

	private void OnEnable() {
		buttons[0].Select();
	}
}
