using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelectViewController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject[] buttons = GameObject.FindGameObjectsWithTag ("StageButton");
		foreach (GameObject button in buttons) {
			Text buttonText = button.GetComponentInChildren<Text>();
			int buttonNum = int.Parse (buttonText.text);

			if (PlayerPrefs.GetInt ("cleared-" + buttonNum.ToString ()) == 1) {
				buttonText.color = Color.yellow;
			}
		}
	}
}
