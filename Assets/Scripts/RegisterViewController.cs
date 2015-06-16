using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RegisterViewController : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;
	public Text nicknameText;

	void Start () {
		googleAnalytics.DispatchHits ();
		googleAnalytics.LogScreen("Register");
	}

	// スタートボタン押下時
	public void StartButtonClicked () {
		Debug.Log (nicknameText.text);
	}
}
