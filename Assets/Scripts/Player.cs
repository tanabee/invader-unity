using UnityEngine;
using System.Collections;

/**
 * プレイヤー（自機）の管理クラス
 * 撃破時の処理をする
 */
public class Player : MonoBehaviour {

	public AudioClip clip;
	public GameObject destroyPrefab;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "AlienBeam" || collider.gameObject.tag == "Alien") {
			SoundManager soundManager = GameObject.FindWithTag ("SoundManager").GetComponent<SoundManager> ();
			soundManager.PlayClip (clip);

			Instantiate (destroyPrefab, transform.position, Quaternion.identity);

			// 自機を透明に
			// gameObject を Destroy すると遷移の処理が実行されないため、透明処理のみ
			var spriteRenderer = GetComponent<SpriteRenderer> ();
			var color = spriteRenderer.color;
			color.a = 0.1f;
			spriteRenderer.color = color;

			Destroy (collider.gameObject);

			StartCoroutine(LoadScoreAfterSeconds(1.0f));
		}
	}

	IEnumerator LoadScoreAfterSeconds(float seconds) {
		yield return new WaitForSeconds(seconds);
		Application.LoadLevel ("Score");
		Destroy (gameObject);
	}

}
