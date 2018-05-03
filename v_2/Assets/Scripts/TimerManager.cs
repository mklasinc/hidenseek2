using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class TimerManager : Photon.MonoBehaviour {

	public float game_max_duration;
	float start_time;
	bool game_status;
//	public GameObject canvas;
	CanvasManager canvasManager;

	//timer time left
	float time_left;

	// Use this for initialization
	void Start () {
		canvasManager = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<CanvasManager> ();
		time_left = game_max_duration; // set initial timer value
		start_time = StartTimer ();
		Debug.Log ("start timer!");
		PhotonView.Get(this).RPC("SetGameStatus", PhotonTargets.AllBuffered, true); // start timer
//		Debug.Log ("timer has been instantiated!");
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
		canvasManager.UpdateTimerUI(time_left.ToString());
		//game over logic
		if (time_left == 0) {
			Debug.Log ("game is over!");
			PhotonView.Get(this).RPC("SetGameStatus", PhotonTargets.AllBuffered, false); // start timer
			canvasManager.UpdateTimerUI("Game Over");
		}

	}

	public float GetTimerValue(){
		return time_left;
	}

	bool GameOn(){
		return game_status;
	}

	[PunRPC] void SetGameStatus(bool b){
		Debug.Log ("game status has been change to:" + b);
		game_status = b;
	}



}
