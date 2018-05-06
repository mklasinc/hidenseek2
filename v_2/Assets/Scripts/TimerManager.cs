using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class TimerManager : Photon.MonoBehaviour {

	public float game_max_duration;
	float start_time;
	bool timer_has_started = false;
	GameObject canvas;
	GameObject gameManager;

	//timer time left
	float time_left;

	// Use this for initialization
	void Start () {
		Debug.Log ("start timer!");
		canvas = GameObject.FindGameObjectWithTag ("Canvas"); // find canvas
		gameManager = GameObject.FindGameObjectWithTag ("GameManager"); // find game manager
		time_left = game_max_duration; // set initial timer value
		start_time = SetStartTime (); // set timer's start time

//		PhotonView.Get(this).RPC("SetGameStatus", PhotonTargets.AllBuffered, true); // set game status to true
	}
	
	// Update is called once per frame
	void Update () {
		// if game is on then update the timer
		bool gameOn = gameManager.GetComponent<GameLogicManager> ().IsGameOn ();
		if (gameOn && !timer_has_started) {
			// start the timer
			timer_has_started = true;
		} else if (gameOn && timer_has_started) {
			//update timer
			UpdateTimer ();
		} else if (!gameOn && timer_has_started) {
			// reset timer
			PhotonView.Get(this).RPC("ResetTimer", PhotonTargets.AllBuffered, 1); // game is over
		}
	}

	float SetStartTime(){
		return Time.time;
	}

	void UpdateTimer(){
		// do some time math
		float time_elapsed = Mathf.Floor(Time.time - start_time); //
		time_left = game_max_duration - time_elapsed;
		float minutes_left = time_left % 60;
		float seconds_left = time_left - minutes_left * 60;
		string time_string = "";
		if (seconds_left > 10) {
			time_string = "0" + minutes_left.ToString () + ":0" + seconds_left.ToString ();
		} else {
			time_string = "0" + minutes_left.ToString() + ":" + seconds_left.ToString ();
		};
		canvas.GetComponent<CanvasManager>().UpdateTimerUI(time_string);
		//game over logic
		if (time_left == 0) {
			Debug.Log ("game is over!");
			gameManager.GetComponent<GameLogicManager> ().GameEnd ("hider");
//			PhotonView.Get(this).RPC("SetGameStatus", PhotonTargets.AllBuffered, false); // game is over
//			canvas.GetComponent<CanvasManager>().EndGame("hider");
//			canvasManager.UpdateTimerUI("Game Over");
		}

	}

	[PunRPC] public void ResetTimer(int n){
		time_left = game_max_duration;
	}

	float GetTimerValue(){
		return time_left;
	}

	public void SeekerWon(){
		PhotonView.Get(this).RPC("ResetTimer", PhotonTargets.AllBuffered, 1);	
	}
//	bool GameOn(){
//		return game_status;
//	}

//	[PunRPC] void SetGameStatus(bool b){
//		Debug.Log ("game status has been change to:" + b);
//		game_status = b;
//	}



}
