using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * プレイヤー（自機）を管理するクラス
 * ドラッグによる移動制御を行う
 */
public class PlayerController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

	public GoogleAnalyticsV3 googleAnalytics;

	private GameObject player;
	public GameObject playerPrefab;
	public GameObject playerContainer;
	public GameObject beamPrefab;
	public Button fireButton;
	private float speed = 4f;

	private Vector2 beginDragPos;
	private bool dragging = false;
	private float dragDirection;

	// Use this for initialization
	void Start () {
		player = (GameObject)Instantiate (playerPrefab, playerContainer.transform.position, Quaternion.identity);
		player.transform.SetParent (playerContainer.transform);

		fireButton.onClick.AddListener (OnButtonClick);

		googleAnalytics.DispatchHits ();
		googleAnalytics.LogScreen("Play-Level" + StageDataManager.instance.GetStage ().level);
	}

	void Update () {

		if (dragging) {
			// 画面左下のワールド座標をビューポートから取得
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0.15f, 0));
			// 画面右上のワールド座標をビューポートから取得
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (0.85f, 1));

			Vector2 pos = playerContainer.transform.position;
			pos.x += dragDirection * speed * Time.deltaTime;
			pos.x = Mathf.Clamp (pos.x, min.x, max.x);

			playerContainer.transform.position = pos;
		}

		GameObject[] beams = GameObject.FindGameObjectsWithTag ("PlayerBeam");
		if (beams.Length == 0) {
			fireButton.interactable = true;
		}
	}

	// ドラッグ開始位置を保持
	public void OnBeginDrag (PointerEventData eventData) {
		beginDragPos = (Vector2)eventData.worldPosition;
		dragging = true;
	}

	// ドラッグ中
	public void OnDrag (PointerEventData eventData) {
		dragDirection = ((Vector2)eventData.worldPosition - beginDragPos).x > 0 ? 1f : -1f;
	}

	// ドラッグ終了
	public void OnEndDrag (PointerEventData eventData) {
		dragging = false;
	}

	public void ShootBeam() {
		Instantiate (beamPrefab, playerContainer.transform.position, Quaternion.identity);
	}

	// FIXME: このイベントハンドリングと処理は別クラス化したほうが良さそう
	void OnButtonClick () {
		ShootBeam ();

		// ボタン無効化
		fireButton.interactable = false;
	}
}
