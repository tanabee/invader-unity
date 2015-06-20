using UnityEngine;
using System.Collections;

/**
 * プレイ画面の壁を管理するクラス
 * 破壊時の処理を行う
 */
public class Wall : MonoBehaviour {

	public AudioClip clip;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "PlayerBeam" || collider.gameObject.tag == "AlienBeam") {
			SoundManager soundManager = GameObject.FindWithTag ("SoundManager").GetComponent<SoundManager> ();
			soundManager.PlayClip (clip);

			Destroy (collider.gameObject);
			Destroy (gameObject);
		}
	}
}
