using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public AudioClip clip;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "AlienBeam" || collider.gameObject.tag == "Alien") {
			SoundManager soundManager = GameObject.FindWithTag ("SoundManager").GetComponent<SoundManager> ();
			soundManager.PlayClip (clip);

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
