using UnityEngine;
using System.Collections;

public class StartViewController : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	void Start() {
		googleAnalytics.DispatchHits ();
		googleAnalytics.LogScreen("Start");
	}

	public void TransitionStageSelectScene() {
		// ユーザー登録していなければ登録画面に遷移
		if (PlayerPrefs.GetString ("user_id") == "") {
			Application.LoadLevel ("Register");
			return;
		}
		Application.LoadLevel ("StageSelect");
	}

	public void TransitionLicenseScene() {
		Application.LoadLevel ("License");
	}

	public void Share() {
		string tweet = "#retro_invader_game https://play.google.com/store/apps/details?id=jp.monopocket.Invader";
		Application.OpenURL("https://twitter.com/intent/tweet?text=" + WWW.EscapeURL(tweet));
	}
}
