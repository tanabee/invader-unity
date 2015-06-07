using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {

	// スコア
	public int score;

	// 1つしかないインスタンス
	static public DataManager instance;

	void Awake() {
		// すでに、DataManager が生成されているか
		// 生成されていなかった場合
		if (instance == null) { 
			instance = this;
			// 画面遷移のタイミングで、自分自身が Destroy されるのを防ぐ
			DontDestroyOnLoad (gameObject);

		// 生成されていた場合
		} else {

			instance.score = 0;

			// 自分自身（新規に追加されたオブジェクト）を消す
			Destroy (gameObject);
		}
	}
}
