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
		
	}

	public void NewPlayerIsReady(bool b){
		if (b == true) {
			Debug.Log ("one player is ready!");
			countOfPlayersReady++;
			Debug.Log ("new count of players ready is" + countOfPlayersReady);
			UpdatePlayerReadCounter (countOfPlayersReady);
		}
	}

	[PunRPC] public void UpdatePlayerReadCounter(int n){

		if (photonView.isMine) {
			photonView.RPC ("UpdatePlayerReadCounter", PhotonTargets.OthersBuffered, n);
		} else {
			Debug.Log ("new count of players ready is" + countOfPlayersReady);
		}
	}
}
