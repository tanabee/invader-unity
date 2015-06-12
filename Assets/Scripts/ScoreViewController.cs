using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreViewController : MonoBehaviour {

	public Text enemyScoreLabel;
	public Text timeScoreLabel;
	public Text totalScoreLabel;
	public Text highScoreLabel;
	public Button retryButton;

	// Use this for initialization
	void Start () {

		// スコア表示
		ShowScore ();

		// リトライボタンのイベント設定
		retryButton.onClick.AddListener (OnButtonClick);
	}

	// スコア表示
	void ShowScore () {

		// ハイスコア表示
		int highScore = PlayerPrefs.GetInt ("score");
		highScoreLabel.text = highScore.ToString();

		// 撃破ポイントを表示
		enemyScoreLabel.text = DataManager.instance.score.ToString();


		int timeScore = 0;
		if (DataManager.instance.isCleared) {
			// クリアタイムのスコアを表示
			timeScore = (180 - (int)DataManager.instance.playTime) * 10;
			if (timeScore < 0) {
				timeScore = 0;
			}
			timeScoreLabel.text = timeScore.ToString ();
		} else {
			timeScoreLabel.text = "0";
		}

		// トータルのスコアを表示
		int totalScore = DataManager.instance.score + timeScore;
		totalScoreLabel.text = (totalScore).ToString();

		// ハイスコアだったら
		if (totalScore > highScore) {
			// ローカル保存
			PlayerPrefs.SetInt ("score", totalScore);
		}
	}

	void OnButtonClick () {
		Application.LoadLevel ("Start");
	}
}
