using UnityEngine;
using System.Collections;

/**
 * ビームを管理するクラス
 * ビームの発射と削除の処理をする
 */
public class Beam : MonoBehaviour {

	public float speed;
	public bool isPlayer;
	public AudioClip clip;

	void Start () {
		SoundManager soundManager = GameObject.FindWithTag ("SoundManager").GetComponent<SoundManager> ();
		soundManager.PlayClip (clip);
	}

	// Update is called once per frame
	void Update () {
		float direction = isPlayer ? 1 : -1;
		transform.Translate (new Vector2(0, direction) * speed);

		// カメラ外に出たら削除
		// 画面左下のワールド座標をビューポートから取得
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0.05f, 0));
		// 画面右上のワールド座標をビューポートから取得
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (0.95f, 1));

		Vector2 pos = transform.position;
		if (pos.x > max.x || pos.y > max.y ||
		    pos.x < min.x || pos.y < min.y) {
			Destroy (gameObject);
		}
	}
}
