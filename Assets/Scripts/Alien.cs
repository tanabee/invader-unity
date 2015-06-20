using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * 宇宙人を管理するクラス
 * ビームの発射や撃破を処理する
 */
public class Alien : MonoBehaviour {

	public AudioClip clip;
	public int life;
	public int point;
	public float beamSpeed;
	public GameObject beamPrefab;

	public void ShootBeam() {
		GameObject beam = (GameObject)Instantiate (beamPrefab, transform.position, Quaternion.identity);
		beam.GetComponent<Beam> ().speed = beamSpeed;
	}

	void OnTriggerEnter2D(Collider2D collider) {

		// プレイヤーのビームと衝突した場合
		if (collider.gameObject.tag == "PlayerBeam") {
			
			Destroy (collider.gameObject);

			// ライフを 1 減らす
			life--;

			// まだ生きていたら return
			if (life > 0) {
				return;
			}

			// 死んだら消す
			SoundManager soundManager = GameObject.FindWithTag ("SoundManager").GetComponent<SoundManager> ();
			soundManager.PlayClip (clip);

			ScoreDataManager.instance.score += point;

			Destroy (gameObject);
		}
	}
}
