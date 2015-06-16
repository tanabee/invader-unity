using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class RegisterViewController : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;
	public Text nicknameText;
	public Text errorLabel;
	public Button startButton;

	void Start () {
		googleAnalytics.DispatchHits ();
		googleAnalytics.LogScreen("Register");
	}

	// スタートボタン押下時
	public void StartButtonClicked () {
		string nickname = nicknameText.text;
		StartCoroutine (PostUser(nickname));
		startButton.interactable = false;
	}

	IEnumerator PostUser (string name) {
		var postDict = new Dictionary<string, string> () {
			{"name",name}
		};
		var headers = new Dictionary<string, string> () {
			{"Content-Type", "application/json; charset=UTF-8"}
		};
		string url = "http://invader-api.herokuapp.com/v1/user";
		string postJson = (string)MiniJSON.Json.Serialize (postDict);
		byte[] bytes = Encoding.UTF8.GetBytes(postJson);
		WWW www = new WWW(url, bytes, headers);

		yield return www;

		// 登録成功時
		if (www.error == null && !string.IsNullOrEmpty(www.text)) {

			string json = www.text;
			var dict = Json.Deserialize (json) as Dictionary<string, object>;
			PlayerPrefs.SetString("user_id", (string)dict["_id"]);
			PlayerPrefs.SetString("user_name", (string)dict["name"]);

			Application.LoadLevel ("StageSelect");

		// エラー時
		} else {

			Debug.Log (www.error);
			// FIXME: ステータスコードで分岐できるようにする
			if (www.error == "400 Bad Request\r") {
				errorLabel.text = "please input another name";
			} else {
				errorLabel.text = "connection failed";
			}

		}
	}
}
