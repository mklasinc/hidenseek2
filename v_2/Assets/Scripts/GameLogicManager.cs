using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;



public class GameLogicManager : Photon.MonoBehaviour {

	bool game_status;
	public GameObject timerprefab;

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
		PhotonView.Get(this).RPC("SetGameStatus", PhotonTargets.AllBuffered, false); // set game status to game over
		PhotonView.Get(this).RPC("ShowEndUI", PhotonTargets.AllBuffered, winner); // game is over

		// reset timer
		if(winner == "seeker"){
			PhotonView.Get(this).RPC("ResetTimer", PhotonTargets.AllBuffered, 1); // game is over
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

	public bool IsGameOn(){
		return game_status;
	}
}
