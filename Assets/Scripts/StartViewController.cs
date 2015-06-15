using UnityEngine;
using System.Collections;

public class StartViewController : MonoBehaviour {

	public void TransitionStageSelectScene() {
		Application.LoadLevel ("StageSelect");
	}

	public void TransitionLicenseScene() {
		Application.LoadLevel ("License");
	}

	public void Share() {
		string tweet = "invader game";// TODO: 公開後に URL セット
		Application.OpenURL("https://twitter.com/intent/tweet?text=" + WWW.EscapeURL(tweet));
	}
}
