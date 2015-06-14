using UnityEngine;
using System.Collections;

public class UFOController : MonoBehaviour {

	public GameObject UFOPrefab;
	public float appearanceTime = 10.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(GenerateUFOAfterSeconds(appearanceTime));
	}

	IEnumerator GenerateUFOAfterSeconds(float seconds) {
		yield return new WaitForSeconds(seconds);

		GameObject UFO = (GameObject)Instantiate (UFOPrefab, transform.position, Quaternion.identity);
		UFO.transform.SetParent (transform);
	}
}
