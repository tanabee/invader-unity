using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlienController : MonoBehaviour
{

	public GameObject alienPrefab;
	public Text gameScore;

	private Vector2 startPos;

	// Alien の数
	private int horizontalLength = 8;
	private int verticalLength = 5;

	// Alien が動くタイミングを制御するための変数
	private float moveInterval = 1f;
	private float currentTime = 0;

	// 一度に移動する距離
	private float verticalDistance = 0.2f;
	private float horizontalDistance = 0.2f;

	// 横の移動幅の上限
	private float horizontalDistanceMax = 2f;

	// 移動方向
	private bool directionIsRight = true;

	// Use this for initialization
	void Start ()
	{
		startPos = transform.position;
		GenerateAliens ();
	}

	// Update is called once per frame
	void Update () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (enemies.Length == 0) {
			Application.LoadLevel ("Score");
		}
		gameScore.text = ((horizontalLength * verticalLength - enemies.Length) * 100).ToString ();

		if (shouldMove ()) {
			Move ();
		}
	}

	// Alien の生成
	void GenerateAliens ()
	{
		// （横は中心、縦は上）を起点として Alien を配置していく
		for (int x = 1; x <= horizontalLength; x++) {
			for (int y = 1; y <= verticalLength; y++) {
				GameObject alien = (GameObject)Instantiate (alienPrefab);
				Vector2 pos = transform.position;
				pos.x += alien.transform.localScale.x * (x - ((float)(horizontalLength + 1) / 2)) * 1f;
				pos.y -= alien.transform.localScale.y * (y - 1) * 1f;
				alien.transform.position = pos;
				alien.transform.SetParent (transform);
			}
		}
	}

	void Move ()
	{

		// 方向を変えない場合に、次に移動する位置を求める
		Vector2 assumedPos = transform.position;
		assumedPos.x += directionIsRight ?
						horizontalDistance :
						horizontalDistance * (-1);

		// 初期位置からの移動ベクトル
		Vector2 diffPos = assumedPos - startPos;

		// 次に端を越える場合
		if (Mathf.Abs(diffPos.x) > horizontalDistanceMax) {

			// 下に移動
			Vector2 nextPos = transform.position;
			nextPos.y -= verticalDistance;
			transform.position = nextPos;

			// 移動方向を左に変更
			directionIsRight = !directionIsRight;

		// 端を越えない場合
		} else {

			transform.position = assumedPos;

		}
	}

	// 今動くべきかを返す
	bool shouldMove ()
	{
		currentTime += Time.deltaTime;

		if (currentTime > moveInterval) {
			currentTime = 0;
			return true;
		} else {
			return false;
		}
	}
}
