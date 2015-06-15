using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreViewController : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	public AudioClip countUpClip;

	public Text enemyScoreLabel;
	public Text timeScoreLabel;
	public Text totalScoreLabel;
	public Text highScoreLabel;
	public Text newScoreLabel;
	public Button retryButton;

	private int enemyScore;
	private int timeScore;
	private int totalScore;
	private int highScore;
	private bool isNewScore;
	private string scoreKey;
	private string clearedKey;

	private SoundManager soundManager;

	// スコアアニメーションの状態
	private enum ScoreAnimationStatus {
		NotStarted,
		EnemyScore,
		TimeScore,
		TotalScore,
		HighScore,
		Ended
	}
	private ScoreAnimationStatus scoreAnimationStatus = ScoreAnimationStatus.NotStarted;
	// スコアアニメーションを制御するための時間
	private float animationTime = 0.0f;

	// Use this for initialization
	void Start () {
		scoreKey = "score-" + StageDataManager.instance.currentLevel.ToString ();
		clearedKey = "cleared-" + StageDataManager.instance.currentLevel.ToString ();
		soundManager = GameObject.FindWithTag ("SoundManager").GetComponent<SoundManager> ();

		// スコア計算
		CalculateScore ();

		// リトライボタンのイベント設定
		retryButton.onClick.AddListener (OnButtonClick);

		googleAnalytics.DispatchHits ();
		googleAnalytics.LogScreen("Score");
	}

	void Update () {
		ShowScoreWithAnimation ();
	}

	void ShowScoreWithAnimation () {
		animationTime += Time.deltaTime;
		// ラベル毎に要するアニメーションの時間
		float animationTimePerLabel = 0.6f;
		float animationRatio = animationTime / animationTimePerLabel;

		switch (scoreAnimationStatus) {

		case ScoreAnimationStatus.NotStarted:
			animationTime = 0.0f;
			scoreAnimationStatus = ScoreAnimationStatus.EnemyScore;

			if (enemyScore != 0) {
				soundManager.PlayClip (countUpClip);
			}
			break;

		case ScoreAnimationStatus.EnemyScore:
			// 撃破ポイントを表示
			enemyScoreLabel.text = ((int)(Mathf.Lerp (0, (float)enemyScore, animationRatio))).ToString ();
			if (animationRatio >= 1.0f) {
				animationTime = 0.0f;
				scoreAnimationStatus = ScoreAnimationStatus.TimeScore;

				if (timeScore != 0) {
					soundManager.PlayClip (countUpClip);
				}
			}
			break;

		case ScoreAnimationStatus.TimeScore:
			// タイムスコアを表示
			timeScoreLabel.text = ((int)Mathf.Lerp(0, (float)timeScore, animationRatio)).ToString ();
			if (animationRatio >= 1.0f) {
				animationTime = 0.0f;
				scoreAnimationStatus = ScoreAnimationStatus.TotalScore;

				if (totalScore != 0) {
					soundManager.PlayClip (countUpClip);
				}
			}
			break;

		case ScoreAnimationStatus.TotalScore:
			// トータルスコアを表示
			totalScoreLabel.text = ((int)Mathf.Lerp(0, (float)totalScore, animationRatio)).ToString ();
			if (animationRatio >= 1.0f) {
				animationTime = 0.0f;
				scoreAnimationStatus = ScoreAnimationStatus.HighScore;
				if (isNewScore) {
					soundManager.PlayClip (countUpClip);
				}
			}
			break;

		case ScoreAnimationStatus.HighScore:
			// ハイスコア更新の場合
			if (isNewScore) {
				highScoreLabel.text = ((int)Mathf.Lerp((float)highScore, (float)totalScore, animationRatio)).ToString ();
				if (animationRatio >= 1.0f) {
					highScoreLabel.color = Color.yellow;
					newScoreLabel.gameObject.SetActive(true);
					scoreAnimationStatus = ScoreAnimationStatus.Ended;
				}
			}
			break;

		case ScoreAnimationStatus.Ended:
			return;
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

			PlayerPrefs.SetInt (clearedKey, 1);

		} else {
			timeScore = 0;
		}

		// トータルのスコアを計算
		totalScore = enemyScore + timeScore;

		// ハイスコア
		highScore = PlayerPrefs.GetInt (scoreKey);
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
