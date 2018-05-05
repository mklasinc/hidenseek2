using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PlayersReady : Photon.MonoBehaviour {
	int countOfPlayersReady = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (countOfPlayersReady >= 2) {
			Debug.Log ("both players are ready!!!!");
//			GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
//			canvas.GetComponent<PhotonView> ().RequestOwnership ();
//			canvas.GetComponent<CanvasManager> ().HideStartUI (1);
			PhotonView.Get(this).RPC("GameStart", PhotonTargets.AllBuffered, 1);
			Destroy(this.gameObject);
		}
	}

	public void NewPlayerIsReady(bool b){
		if (b == true) {
			Debug.Log ("one player is ready!");
//			countOfPlayersReady++;
//			Debug.Log ("personal count of players ready is" + countOfPlayersReady);
			PhotonView.Get(this).RPC("UpdatePlayerReadCounter", PhotonTargets.MasterClient, 1);
//			PhotonView.Get(this).RPC("UpdatePlayerReadCounter", PhotonTargets.AllBuffered, countOfPlayersReady);
		}
	}

	[PunRPC] public void UpdatePlayerReadCounter(int n){
//		Debug.Log ("update player counter is called!");
//		Debug.Log ("call is mine?" + photonView.isMine);
		countOfPlayersReady++;
//		countOfPlayersReady = n;
//		Debug.Log ("global count of players ready is" + countOfPlayersReady);

//		if (photonView.isMine) {
//			photonView.RPC ("UpdatePlayerReadCounter", PhotonTargets.OthersBuffered, n);
//		} else {
//			countOfPlayersReady = n;
//			Debug.Log ("global count of players ready is" + countOfPlayersReady);
//		}
	}
}
