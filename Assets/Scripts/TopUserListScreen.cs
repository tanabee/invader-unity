using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopUserListScreen : MonoBehaviour {

	public UserData userData;
	public GameObject userNodePrefab;
	public Transform nodeContainer;


	IEnumerator Start () {
		yield return StartCoroutine (userData.Load());

		foreach (UserData.User user in userData.list) {

			GameObject node = (GameObject)Instantiate (userNodePrefab);
			Transform nodeTf = node.transform;
			nodeTf.SetParent (nodeContainer, false);
			nodeTf.Find ("Ranking").GetComponent<Text> ().text = user.ranking.ToString ();
			nodeTf.Find ("Name").GetComponent<Text> ().text = user.name;
			nodeTf.Find ("Score").GetComponent<Text> ().text = user.score.ToString ();
		}
	}
}
