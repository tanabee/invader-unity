using UnityEngine;
using System.Collections;

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
