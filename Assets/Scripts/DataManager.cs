using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {

	// スコア
	public int score;

	// 1つしかないインスタンス
	static public DataManager instance;

	void Awake() {
		// すでに、DataManager が生成されているか
		if (instance == null) { 
			// 生成されていなかったら、
			instance = this;
			// 画面遷移のタイミングで、自分自身が Destroy されるのを防ぐ
			DontDestroyOnLoad (gameObject);
		} else {
			// 生成されていた
			// 自分自身（新規に追加されたオブジェクト）を消す
			Destroy (gameObject);
		}
	}
}
