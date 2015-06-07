using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreViewController : MonoBehaviour {

	public ScoreCounter scoreCounter;
	public Button retryButton;

	// Use this for initialization
	void Start () {
		scoreCounter.SetScore (DataManager.instance.score);
		retryButton.onClick.AddListener (OnButtonClick);
	}

	void OnButtonClick () {
		Application.LoadLevel ("Start");
	}
}
