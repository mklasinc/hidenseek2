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
			Debug.Log ("start the game!!!!");
			GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
			canvas.GetComponent<PhotonView> ().RequestOwnership ();
			canvas.GetComponent<CanvasManager> ().HideStartUI (1);
			Destroy(this.gameObject);
		}
	}

	public void NewPlayerIsReady(bool b){
		if (b == true) {
			Debug.Log ("one player is ready!");
			countOfPlayersReady++;
			Debug.Log ("personal count of players ready is" + countOfPlayersReady);
			UpdatePlayerReadCounter (countOfPlayersReady);
		}
	}

	[PunRPC] public void UpdatePlayerReadCounter(int n){

		if (photonView.isMine) {
			photonView.RPC ("UpdatePlayerReadCounter", PhotonTargets.OthersBuffered, n);
		} else {
			countOfPlayersReady = n;
			Debug.Log ("global count of players ready is" + countOfPlayersReady);
		}
	}
}
