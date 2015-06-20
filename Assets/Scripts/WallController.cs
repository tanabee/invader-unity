using UnityEngine;
using System.Collections;

/**
 * プレイ画面の壁の生成をするクラス
 */
public class WallController : MonoBehaviour {

	public GameObject wallPrefab;

	// 壁生成のための変数群
	private int groupCount = 3;
	private int verticalLengthPerGroup = 3;
	private int horizontalLengthPerGroup = 3;
	private float horizontalPaddingGroup = 3.0f;

	void Start () {
		GenerateWalls ();
	}

	// 壁の生成を行う
	void GenerateWalls () {

		// 壁グループごとにループ
		for (int g = 1; g <= groupCount; g++) {

			// （横は中心、縦は上）を起点として Alien を配置していく
			for (int x = 1; x <= horizontalLengthPerGroup; x++) {
				for (int y = 1; y <= verticalLengthPerGroup; y++) {
					GameObject wall = (GameObject)Instantiate (wallPrefab);
					Vector2 pos = transform.position;
					pos.x += wall.transform.localScale.x * (x - ((float)(horizontalLengthPerGroup + 1) / 2)) * 0.032f;
					pos.x += (g - ((float)(groupCount + 1) / 2)) * horizontalPaddingGroup;
					pos.y -= wall.transform.localScale.y * (y - 1) * 0.04f;
					wall.transform.position = pos;
					wall.transform.SetParent (transform);
				}
			}

		}

	}
}
