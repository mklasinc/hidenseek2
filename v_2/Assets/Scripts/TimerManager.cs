using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

	public float game_max_duration;
	float start_time;
	bool game_status;
//	public GameObject canvas;
//	GameObject canvas
	Text timer_text;

	//timer time left
	float time_left;

	// Use this for initialization
	void Start () {
		time_left = game_max_duration; // set initial timer value
		start_time = StartTimer ();
		SetGameStatus (true);
		Debug.Log ("timer has been instantiated!");
//		timer_text = canvas.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameOn()) {
			UpdateTimer ();
		}
	}

	float StartTimer(){
		return Time.time;
	}

	void UpdateTimer(){
		float time_elapsed = Mathf.Floor(Time.time - start_time);
		time_left = game_max_duration - time_elapsed;
		Debug.Log ("time left is:" + time_left);
		timer_text.text = time_left.ToString();
		//game over logic
		if (time_left == 0) {
			Debug.Log ("game is over!");
			SetGameStatus (false);
			timer_text.text = "Game Over";
		}

	}

	public float GetTimerValue(){
		return time_left;
	}

	bool GameOn(){
		return game_status;
	}

	void SetGameStatus(bool b){
		game_status = b;
	}



}
