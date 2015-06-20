using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

/**
 * ユーザー情報の管理クラス
 * ユーザー情報の API 取得、パースの処理を行う
 */
public class UserData : MonoBehaviour {

	string url = "http://invader-api.herokuapp.com/v1/ranking";

	[System.Serializable]
	public class User {
		public string name;
		public int score;
		public int ranking;
	}

	public List<User> list;

	public IEnumerator Load () {
		WWW www = new WWW (url);

		using (www) {
			yield return www;

			if (!string.IsNullOrEmpty (www.error)) {
				Debug.Log (www.error);
				yield break;
			}

			// リスト初期化
			list = new List<User> ();

			var users = Json.Deserialize (www.text) as List<object>;
			int ranking = 1;
			foreach (object obj in users) {
				var userDict = obj as Dictionary<string, object>;

				var user = new User ();
				user.name = (string)userDict["name"];
				user.score = Convert.ToInt32(userDict["score"]);
				user.ranking = ranking;

				list.Add (user);

				ranking++;
			}
			
		}

	}
}
