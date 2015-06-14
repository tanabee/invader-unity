using UnityEngine;
using System.Collections;

public class StartViewController : MonoBehaviour {

	public void TransitionStageSelectScene() {
		Application.LoadLevel ("StageSelect");
	}

	public void TransitionLicenseScene() {
		Application.LoadLevel ("License");
	}
}
