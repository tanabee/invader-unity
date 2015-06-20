using UnityEngine;
using System.Collections;

/**
 * ランキングシーン全般の管理クラス
 */
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
