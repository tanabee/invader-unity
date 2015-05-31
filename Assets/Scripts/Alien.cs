using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy (collider.gameObject);
		Destroy (gameObject);
	}
}
