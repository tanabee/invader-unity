using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

/**
 * ステージ情報を管理するクラス
 * シーン間でデータを共有できるように単一のインスタンスしか持たない
 */
public class StageDataManager : MonoBehaviour {

	private List<Stage> stages;
	static public StageDataManager instance;
	public long currentLevel;

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

	// 現在のステージ情報を返す
	public Stage GetStage() {
		foreach (Stage stage in stages) {
			if (stage.level == currentLevel) {
				return stage;
			}
		}
		return null;
	}

	// ステージ情報を JSON ファイルから取得
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

			// フレンドデータをリストに追加
			stages.Add (stage);
		}
	}
}
