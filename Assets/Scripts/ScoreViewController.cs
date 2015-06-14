using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreViewController : MonoBehaviour {

	public Text enemyScoreLabel;
	public Text timeScoreLabel;
	public Text totalScoreLabel;
	public Text highScoreLabel;
	public Button retryButton;

	private float time = 0.0f;
	private int enemyScore;
	private int timeScore;
	private int totalScore;
	private int highScore;
	private bool isNewScore;
	private string scoreKey;

	// Use this for initialization
	void Start () {
		scoreKey = "score-" + StageDataManager.instance.currentLevel.ToString ();

		// スコア計算
		CalculateScore ();

		// リトライボタンのイベント設定
		retryButton.onClick.AddListener (OnButtonClick);
	}

	void Update () {
		ShowScore ();
	}

	void ShowScore () {
		time += Time.deltaTime;

		// 撃破ポイントを表示
		enemyScoreLabel.text = ((int)(Mathf.Lerp(0, (float)enemyScore, time))).ToString ();

		// タイムスコアを表示
		timeScoreLabel.text = ((int)Mathf.Lerp(0, (float)timeScore, time - 1.0f)).ToString ();

		// トータルスコアを表示
		totalScoreLabel.text = ((int)Mathf.Lerp(0, (float)totalScore, time - 2.0f)).ToString ();

		// トータルスコア表示が完了している場合
		if (int.Parse (totalScoreLabel.text) == totalScore) {
			// ハイスコア更新の場合
			if (isNewScore) {
				highScoreLabel.text = totalScore.ToString ();
			}
		}
	}

	// スコア計算
	void CalculateScore () {

		enemyScore = DataManager.instance.score;

		if (DataManager.instance.isCleared) {
			// クリアタイムのスコアを表示
			timeScore = (180 - (int)DataManager.instance.playTime) * 10;
			if (timeScore < 0) {
				timeScore = 0;
			}
		} else {
			timeScore = 0;
		}

		// トータルのスコアを計算
		totalScore = enemyScore + timeScore;

		// ハイスコア
		int highScore = PlayerPrefs.GetInt (scoreKey);
		highScoreLabel.text = highScore.ToString ();

		// ハイスコアだったら
		if (totalScore > highScore) {
			isNewScore = true;

			// ローカル保存
			PlayerPrefs.SetInt (scoreKey, totalScore);
		}
	}

	void OnButtonClick () {
		Application.LoadLevel ("Start");
	}
}
