using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Alien : MonoBehaviour {

	public AudioClip clip;
	public int life;
	public int point;

	void OnTriggerEnter2D(Collider2D collider) {

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

			DataManager.instance.score += point;

			Destroy (gameObject);
		}
	}
}
