using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * 宇宙人全体を管理するクラス
 * 宇宙人群の生成、移動、ビームを打つタイミングなどを制御する
 */
public class AlienController : MonoBehaviour
{

	public GameObject alien01;
	public GameObject alien02;
	public GameObject alien03;
	public GameObject alien04;
	public GameObject alien05;

	public ScoreCounter scoreCounter;
	public GameObject playerContainer;

	private Vector2 startPos;

	// Alien の数
	private int horizontalLength = 10;

	// Alien が動くタイミングを制御するための変数
	public float moveInterval = 0.8f;
	private float moveTimer = 0;
	// ビームを発射するタイミング
	private float minShootInterval = 1f;
	private float maxShootInterval = 3f;
	private float shootTimer = 3f;

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
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Alien");
		if (enemies.Length == 0) {
			ScoreDataManager.instance.isCleared = true;
			Application.LoadLevel ("Score");
		}
		scoreCounter.SetScore (ScoreDataManager.instance.score);

		if (shouldMove ()) {
			Move ();
		}

		if (shouldShoot ()) {
			ShootBeam (enemies);
		}
	}

	// Alien の生成
	void GenerateAliens ()
	{
		StageDataManager.Stage stage = StageDataManager.instance.GetStage ();

		// （横は中心、縦は上）を起点として Alien を配置していく
		for (int x = 1; x <= horizontalLength; x++) {
			for (int y = 1; y <= stage.aliens.Count; y++) {
				GameObject prefab = GetAlienPrefabWithAlien (stage.aliens [y - 1]);
				GameObject alien = (GameObject)Instantiate (prefab);
				Vector2 pos = transform.position;
				pos.x += alien.transform.localScale.x * (x - ((float)(horizontalLength + 1) / 2)) * 1f;
				pos.y -= alien.transform.localScale.y * (y - 1) * 1f;
				alien.transform.position = pos;
				alien.transform.SetParent (transform);
			}
		}
	}

	// Alien の集団の移動
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

	// Alien のレベルに応じてプレハブを返す
	GameObject GetAlienPrefabWithAlien(long level) {
		switch (level) {
		case 1:
			return alien01;
		case 2:
			return alien02;
		case 3:
			return alien03;
		case 4:
			return alien04;
		case 5:
			return alien05;
		default:
			return alien01;
		}
	}

	void ShootBeam(GameObject[] enemies) {

		// x 座標換算で一番近い敵
		GameObject closestEnemy = enemies[0];
		float closestDiffX = Mathf.Abs(closestEnemy.transform.position.x - playerContainer.transform.position.x);

		foreach (GameObject enemy in enemies) {
			float diffX = Mathf.Abs (enemy.transform.position.x - playerContainer.transform.position.x);
			if (diffX < closestDiffX) {
				closestEnemy = enemy;
				closestDiffX = diffX;
			}
		}

		closestEnemy.GetComponent<Alien> ().ShootBeam ();
	}

	// 今動くべきかを返す
	bool shouldMove ()
	{
		moveTimer += Time.deltaTime;

		if (moveTimer > moveInterval) {
			moveTimer = 0;
			return true;
		} else {
			return false;
		}
	}

	// 今打つべきかを返す
	bool shouldShoot ()
	{
		shootTimer -= Time.deltaTime;

		if (shootTimer <= 0.0f) {
			shootTimer = Random.Range (minShootInterval, maxShootInterval);
			return true;
		} else {
			return false;
		}
	}
}
