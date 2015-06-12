﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayTimer : MonoBehaviour {

	public Text timeLabel;
	private float time = 0;

	void Update () {
		time += Time.deltaTime;
		int minutes = (int)Mathf.Floor(time / 60.0f);
		int seconds = (int)time % 60;
		string timeStr = minutes.ToString () + ":" + seconds.ToString ("00");
		timeLabel.text = timeStr;

		DataManager.instance.playTime = time;
	}
}
