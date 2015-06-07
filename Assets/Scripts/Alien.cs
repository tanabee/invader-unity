using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Alien : MonoBehaviour {

	public AudioClip clip;

	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log ("triggerEnter");
		Debug.Log (collider);
		Debug.Log (collider.gameObject.tag);
		if (collider.gameObject.tag == "PlayerBeam") {
			Debug.Log (collider.gameObject.tag == "PlayerBeam");
			SoundManager soundManager = GameObject.FindWithTag ("SoundManager").GetComponent<SoundManager> ();
			soundManager.PlayClip (clip);

			DataManager.instance.score += 50;

			Destroy (collider.gameObject);
			Destroy (gameObject);
		}
	}
}
