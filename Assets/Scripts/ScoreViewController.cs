using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreViewController : MonoBehaviour {

	public Text enemyScore;
	public Text timeScore;
	public Text totalScore;
	public Button retryButton;

	// Use this for initialization
	void Start () {

		ShowScore ();

		// リトライボタンのイベント設定
		retryButton.onClick.AddListener (OnButtonClick);
	}

	void ShowScore () {

		// 撃破ポイントを表示
		enemyScore.text = DataManager.instance.score.ToString();


		int timeScoreNum = 0;
		if (DataManager.instance.isCleared) {
			// クリアタイムのスコアを表示
			timeScoreNum = (180 - (int)DataManager.instance.playTime) * 10;
			if (timeScoreNum < 0) {
				timeScoreNum = 0;
			}
			timeScore.text = timeScoreNum.ToString ();
		} else {
			timeScore.text = "0";
		}

		// トータルのスコアを表示
		totalScore.text = (DataManager.instance.score + timeScoreNum).ToString();
		
	}

	void OnButtonClick () {
		Application.LoadLevel ("Start");
	}
}
