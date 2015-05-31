using UnityEngine;
using System.Collections;

public class AlienManager : MonoBehaviour
{

	public GameObject alienPrefab;
	private int horizontalLength = 8;
	private int verticalLength = 5;

	// Use this for initialization
	void Start ()
	{
		GenerateAliens ();
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	// Alien の生成
	void GenerateAliens () {
		// （横は中心、縦は上）を起点として Alien を配置していく
		for (int x = 1; x <= horizontalLength; x++) {
			for (int y = 1; y <= verticalLength; y++) {
				GameObject alien = (GameObject)Instantiate (alienPrefab);
				Vector2 pos = transform.position;
				pos.x += alien.transform.localScale.x * (x - ((float)(horizontalLength + 1) / 2)) * 1f;
				pos.y -= alien.transform.localScale.y * (y - 1) * 1f;
				Debug.Log (pos);
				alien.transform.position = pos;
				alien.transform.SetParent (transform);
			}
		}
	}
}
