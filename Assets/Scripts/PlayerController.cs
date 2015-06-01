using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDragHandler {

	private GameObject player;
	public GameObject playerPrefab;
	public GameObject playerContainer;
	public GameObject beamPrefab;
	public Button fireButton;

	// Use this for initialization
	void Start () {
		player = (GameObject)Instantiate (playerPrefab, playerContainer.transform.position, Quaternion.identity);
		player.transform.SetParent (playerContainer.transform);

		fireButton.onClick.AddListener (OnButtonClick);
	}

	void Update () {
	}

	public void OnDrag (PointerEventData eventData) {
		Vector2 moveVector = new Vector2 (eventData.delta.x / 60, 0);
		playerContainer.transform.Translate (moveVector);
	}

	public void ShootBeam() {
		GameObject beam = (GameObject)Instantiate (beamPrefab, playerContainer.transform.position, Quaternion.identity);
		Destroy (beam, 3f);
	}

	void OnButtonClick () {
		ShootBeam ();

		// ボタン無効化
		fireButton.interactable = false;

		// 1 秒後に再発射できる
		StartCoroutine(enableButtonAfterSeconds(1.0f));
	}

	IEnumerator enableButtonAfterSeconds(float seconds) {
		yield return new WaitForSeconds(seconds);
		fireButton.interactable = true;
	}
}
