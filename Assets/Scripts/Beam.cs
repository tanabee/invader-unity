using UnityEngine;
using System.Collections;

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
	}
}
