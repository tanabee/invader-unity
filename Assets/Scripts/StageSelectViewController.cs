using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

/**
 * ステージ選択ページ全般の管理クラス
 */
public class StageSelectViewController : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;
	public Text totalScoreLabel;
	public Text rankingLabel;

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

		googleAnalytics.DispatchHits ();
		googleAnalytics.LogScreen("StageSelect");

		totalScoreLabel.text = ScoreDataManager.instance.totalScore ().ToString ();
		rankingLabel.text = PlayerPrefs.GetInt ("ranking").ToString();

		StartCoroutine (LoadRankingMe());
	}

	// 自分のランキングを取得して表示
	public IEnumerator LoadRankingMe () {

		string url = "http://invader-api.herokuapp.com/v1/ranking/me?score=" + ScoreDataManager.instance.totalScore().ToString();

		using (WWW www = new WWW (url)) {

			yield return www;

			if (!string.IsNullOrEmpty(www.error)) {
				Debug.Log (www.error);
				yield break;
			}

			// パース
			var dict = Json.Deserialize(www.text) as Dictionary<string, object>;
			int ranking = Convert.ToInt32(dict["ranking"]);

			rankingLabel.text = ranking.ToString();
			PlayerPrefs.SetInt ("ranking", ranking);
		}
	}

	public void TransitionRankingScene() {
		Application.LoadLevel ("Ranking");
	}
}
