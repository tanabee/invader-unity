using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * プレイ画面のリアルタイムスコア表示の管理クラス
 */
public class ScoreCounter : MonoBehaviour {

	public Text scoreText;

	public void SetScore (int score) {
		scoreText.text = score.ToString ();
	}

	public int GetScore () {
		string score = scoreText.text;
		return int.Parse (score);
	}
}
