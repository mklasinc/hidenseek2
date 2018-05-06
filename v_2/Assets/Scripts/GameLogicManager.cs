using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;



public class GameLogicManager : Photon.MonoBehaviour {

	bool game_status;
	public GameObject timerprefab;
	public GameObject clockprefab;
	bool crunchTimeSoundIsOn = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[PunRPC] public void GameStart(int n){
		GameObject timer = GameObject.FindGameObjectWithTag ("Timer");
		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
		//hide start ui
		canvas.GetComponent<CanvasManager>().HideStartUI(1);
//		PhotonView.Get(this).RPC("HideStartUI", PhotonTargets.AllBuffered,photonView.viewID);	
		//start timer
		Debug.Log("will try to instantiate a timer object!");
		PhotonNetwork.Instantiate (timerprefab.name, Vector3.zero, Quaternion.identity,0);
		//update game status
		PhotonView.Get(this).RPC("SetGameStatus", PhotonTargets.AllBuffered, 1); // set game status to game over
//		SetGameStatus(true);
	}
		

	[PunRPC] public void GameEnd(string winner){
		Debug.Log ("game ended, winner is:" + winner);
		PhotonView.Get(this).RPC("SetGameStatus", PhotonTargets.AllBuffered, 0); // set game status to game over
//		PhotonView.Get(this).RPC("ShowEndUI", PhotonTargets.AllBuffered, winner); // game is over
		GameObject timer = GameObject.FindGameObjectWithTag ("Timer");
		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
		canvas.GetComponent<CanvasManager> ().GameEnd (winner);
		KillCrunchTime (); // kill crunch time sound

		PhotonView.Get(this).RPC("PlayEndGameSound", PhotonTargets.AllBuffered, winner);

//		PhotonView.Get(this).RPC("Test2", PhotonTargets.AllBuffered, 2);

	}

	[PunRPC] public void PlayEndGameSound(string winner){
		GameObject seeker = GameObject.Find ("Seeker(Clone)");
		GameObject hider = GameObject.Find ("Hider(Clone)");
		GameObject sEffectManager = GameObject.FindGameObjectWithTag("SoundEffects");

		// reset timer
		if (winner == "seeker") {
			GameObject timer = GameObject.FindGameObjectWithTag ("Timer");
			timer.GetComponent<TimerManager> ().SeekerWon (); // reset timer
			if (seeker != null) {
				// seeker plays winning sound
				sEffectManager.GetComponent<SoundEffectsManager>().InstantiateFootsteps("winner",Vector3.zero);
			} else if(hider != null){
				sEffectManager.GetComponent<SoundEffectsManager>().InstantiateFootsteps("loser",Vector3.zero);
			}
		} else if (winner == "hider") {
			if (seeker != null) {
				// seeker plays winning sound
				sEffectManager.GetComponent<SoundEffectsManager>().InstantiateFootsteps("loser",Vector3.zero);
			} else if(hider != null){
				sEffectManager.GetComponent<SoundEffectsManager>().InstantiateFootsteps("winner",Vector3.zero);
			}

		}
	
	}

	[PunRPC] public void SetGameStatus(int n){
		Debug.Log ("game status has been change to:" + n);
		if (n == 0) {
			game_status = false;
		} else {
			game_status = true;
		}
//		game_status = b;
	}

	public void StartCrunchTime(){
			GameObject.Instantiate (clockprefab, Vector3.zero, Quaternion.identity);
//			PhotonView.Get(this).RPC("CrunchTimeInstantiate", PhotonTargets.MasterClient, 1);
	}

	public void KillCrunchTime(){
		PhotonView.Get(this).RPC("CrunchTimeKill", PhotonTargets.MasterClient, 1);
	}


	[PunRPC] public void CrunchTimeInstantiate(int n){

		PhotonNetwork.Instantiate (clockprefab.name, Vector3.zero, Quaternion.identity,0);
	}

	[PunRPC] public void CrunchTimeKill(int n){
		GameObject clock = GameObject.FindGameObjectWithTag ("ClockTick");
		if (clock != null) {
			clock.GetComponent<ClockTick> ().KillClockTick ();
		}
	}

	public bool IsGameOn(){
		return game_status;
	}

	[PunRPC] public void Test2(int b){
		Debug.Log ("test 2 succeeded!");
	}
}
