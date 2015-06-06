using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	// 画面をロードする
	public void Load (string sceneName) {
		Application.LoadLevel (sceneName);
	}
}
