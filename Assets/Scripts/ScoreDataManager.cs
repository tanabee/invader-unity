using UnityEngine;
using System.Collections;

/**
 * スコアデータを管理するクラス
 * シーン間でデータを維持できるように単一の instance しか持たない
 */
public class ScoreDataManager : MonoBehaviour {

	// スコア
	public int score;

	// FIXME: ここらへんはスコアのシーンに値渡しできると嬉しい
	// 直近のステージのプレイ時間
	public float playTime;
	// 直近のステージでクリアしたかどうか
	public bool isCleared;

	// 1つしかないインスタンス
	static public ScoreDataManager instance;

	void Awake() {
		// すでに、DataManager が生成されているか
		// 生成されていなかった場合
		if (instance == null) { 
			instance = this;
			// 画面遷移のタイミングで、自分自身が Destroy されるのを防ぐ
			DontDestroyOnLoad (gameObject);

		// 生成されていた場合
		} else {

			instance.score = 0;
			instance.playTime = 0.0f;
			instance.isCleared = false;

			// 自分自身（新規に追加されたオブジェクト）を消す
			Destroy (gameObject);
		}
	}

	// ハイスコアの合計を返す
	public int totalScore () {
		int score = 0;
		// FIXME: level 上限は config から取る
		for (int level = 1; level <= 10; level++) {
			string scoreKey = "score-" + level.ToString();
			score += PlayerPrefs.GetInt (scoreKey);
		}
		return score;
	}
}
