using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

	private GameObject player;
	public GameObject playerPrefab;
	public GameObject playerContainer;
	public GameObject beamPrefab;
	public Button fireButton;
	private float speed = 5f;

	private Vector2 beginDragPos;
	private bool dragging = false;
	private float dragDirection;

	// Use this for initialization
	void Start () {
		player = (GameObject)Instantiate (playerPrefab, playerContainer.transform.position, Quaternion.identity);
		player.transform.SetParent (playerContainer.transform);

		fireButton.onClick.AddListener (OnButtonClick);
	}

	void Update () {

		if (dragging) {
			// 画面左下のワールド座標をビューポートから取得
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0.05f, 0));
			// 画面右上のワールド座標をビューポートから取得
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (0.95f, 1));

			Vector2 pos = playerContainer.transform.position;
			pos.x += dragDirection * speed * Time.deltaTime;
			pos.x = Mathf.Clamp (pos.x, min.x, max.x);

			playerContainer.transform.position = pos;
		}

		GameObject[] beams = GameObject.FindGameObjectsWithTag ("Beam");
		if (beams.Length == 0) {
			fireButton.interactable = true;
		}
	}

	public void OnBeginDrag (PointerEventData eventData) {
		beginDragPos = (Vector2)eventData.worldPosition;
		dragging = true;
	}

	public void OnDrag (PointerEventData eventData) {
		dragDirection = ((Vector2)eventData.worldPosition - beginDragPos).x > 0 ? 1f : -1f;
	}

	public void OnEndDrag (PointerEventData eventData) {
		dragging = false;
	}

	public void ShootBeam() {
		GameObject beam = (GameObject)Instantiate (beamPrefab, playerContainer.transform.position, Quaternion.identity);
		Destroy (beam, 1f);
	}

	void OnButtonClick () {
		ShootBeam ();

		// ボタン無効化
		fireButton.interactable = false;

//		// 1 秒後に再発射できる
//		StartCoroutine(enableButtonAfterSeconds(1.0f));
	}

//	IEnumerator enableButtonAfterSeconds(float seconds) {
//		yield return new WaitForSeconds(seconds);
//		fireButton.interactable = true;
//	}
}
