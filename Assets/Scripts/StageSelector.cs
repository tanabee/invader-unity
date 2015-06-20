using UnityEngine;
using System.Collections;

/**
 * ステージ選択を管理するクラス
 */
public class StageSelector : MonoBehaviour {

	// ステージ選択時
	public void Select(int level) {
		// 選択したレベルを DataManager にセット
		StageDataManager.instance.currentLevel = (long)level;
		// プレイ画面に遷移
		Application.LoadLevel ("Play");
	}
}
