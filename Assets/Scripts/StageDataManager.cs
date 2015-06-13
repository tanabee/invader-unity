using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class StageDataManager : MonoBehaviour {

	public List<Stage> stages;

	static public StageDataManager instance;

	// ステージの設定データ
	[System.Serializable]
	public class Stage {
		// ステージのレベル（ステージ番号）
		public long level;
		// 敵のレベル
		public List<long> aliens;
	}

	void Awake() {

		// すでに、DataManager が生成されているか
		// 生成されていなかった場合
		if (instance == null) { 
			instance = this;

			LoadStages ();

			// 画面遷移のタイミングで、自分自身が Destroy されるのを防ぐ
			DontDestroyOnLoad (gameObject);

		// 生成されていた場合
		} else {

			// 自分自身（新規に追加されたオブジェクト）を消す
			Destroy (gameObject);
		}
	}

	void LoadStages () {

		stages = new List<Stage> ();

		TextAsset textAsset = Resources.Load ("Config/stage") as TextAsset;
		string json = textAsset.text;
		var dict = Json.Deserialize (json) as Dictionary<string, object>;
		var stageDictionaries = dict["stages"] as List<object>;
		foreach (object obj in stageDictionaries) {
			var stageDict = obj as Dictionary<string, object>;

			var stage = new Stage ();
			stage.level = (long)stageDict["level"];
			stage.aliens = new List<long> ();
			foreach (object alienObj in (List<object>)stageDict["aliens"]) {
				var alien = (long)alienObj;
				stage.aliens.Add(alien);
			}

			Debug.Log (stage.level);
			Debug.Log (stage.aliens);

			// フレンドデータをリストに追加
			stages.Add (stage);
		}
	}
}
