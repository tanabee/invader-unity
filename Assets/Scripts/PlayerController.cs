using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDragHandler {

	public GameObject playerPrefab;
	public GameObject playerContainer;
	private GameObject player;
	public GameObject beamPrefab;

	// Use this for initialization
	void Start () {
		player = (GameObject)Instantiate (playerPrefab, playerContainer.transform.position, Quaternion.identity);
		player.transform.SetParent (playerContainer.transform);
	}

	public void OnDrag (PointerEventData eventData) {
		Vector2 moveVector = new Vector2 (eventData.delta.x / 40, 0);

		playerContainer.transform.Translate (moveVector);
	}

	public void ShootBeam() {
		GameObject beam = (GameObject)Instantiate (beamPrefab, playerContainer.transform.position, Quaternion.identity);
		Destroy (beam, 3f);
	}
}
