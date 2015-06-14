using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour {

	// 出現時の音
	public AudioClip appearanceClip;

	// 撃破時の音
	public AudioClip hitClip;

	private int point = 1000;
	private float speed = 1.5f;

	SoundManager soundManager;

	void Start () {
		Debug.Log ("start");
		soundManager = GameObject.FindWithTag ("SoundManager").GetComponent<SoundManager> ();
		soundManager.PlayBgClip (appearanceClip);
	}
	
	void Update () {
		Vector2 pos = transform.position;
		pos.x -= Time.deltaTime * speed;
		transform.position = pos;
	}

	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.gameObject.tag == "PlayerBeam") {
			
			soundManager.PlayClip (hitClip);
			soundManager.StopBgClip ();

			DataManager.instance.score += point;

			Destroy (collider.gameObject);
			Destroy (gameObject);
		}
	}
}
