using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {

	private float speed = 0.2f;

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.up * speed);
	}
}
