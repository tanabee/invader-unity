using UnityEngine;
using System.Collections;

public class StartViewController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void transitionPlayScene () {
		Application.LoadLevel ("Play");
	}
}
