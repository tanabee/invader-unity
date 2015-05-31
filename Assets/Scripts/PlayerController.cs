using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDragHandler {

	public GameObject playerPrefab;
	public GameObject playerStart;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = (GameObject)Instantiate (playerPrefab, playerStart.transform.position, Quaternion.identity);
	}

	public void OnDrag (PointerEventData eventData) {
		Vector2 moveVector = new Vector2 (eventData.delta.x / 20, 0);

		player.transform.Translate (moveVector);
	}
}
