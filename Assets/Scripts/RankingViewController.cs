using UnityEngine;
using System.Collections;

public class RankingViewController : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	void Start () {
		googleAnalytics.DispatchHits ();
		googleAnalytics.LogScreen("Ranking");
	}

	public void TransitionStageSelectScene() {
		Application.LoadLevel ("StageSelect");
	}
}
