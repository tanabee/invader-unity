using UnityEngine;
using System.Collections;

public class ScoreViewController : MonoBehaviour {

	public ScoreCounter scoreCounter;

	// Use this for initialization
	void Start () {
		scoreCounter.SetScore (DataManager.instance.score);
	}
}
