using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class CanvasManager : Photon.MonoBehaviour {

	public GameObject startUI;
	public GameObject endUI;
	GameObject pointer;
	public bool myPlayerReady = false;
	public bool otherPlayerReady = false;

	// Use this for initialization
	void Start () {
		pointer = GameObject.FindGameObjectWithTag ("Pointer");
		ShowStartUI (1);
	}
	
	// Update is called once per frame
	void Update () {

		if (photonView.isMine) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				Debug.Log ("switch to end UI");
				ShowEndUI ();
			}
		}
		
	}

	[PunRPC] public void ShowStartUI(int n){
		Debug.Log ("we are called!");
		startUI.SetActive (true);

		Debug.Log ("our pointer is" + pointer);
		pointer.SetActive (false);

		if (photonView.isMine) {
			StartCoroutine(WaitBeforeHidingStartUI(10));
			photonView.RPC("ShowStartUI", PhotonTargets.OthersBuffered,photonView.viewID);
		}
	}

	[PunRPC] public void PlayerReady(bool b){
		




		if (photonView.isMine) {
			myPlayerReady = b;
			Debug.Log ("my player is ready");
			if (myPlayerReady && otherPlayerReady) {
				Debug.Log ("both players are ready!");
			} else {
				photonView.RPC ("PlayerReady", PhotonTargets.OthersBuffered, b);
			}

		} else {
			Debug.Log ("other player is ready!");
			otherPlayerReady = b;
		};


	}

	IEnumerator WaitBeforeHidingStartUI(int s){
		Debug.Log ("waiting ...");
		yield return new WaitForSeconds(s);
		Debug.Log ("calling hide start ui");
		HideStartUI (1);
	}

	[PunRPC] public void HideStartUI(int n){
		Debug.Log ("hide start ui");
		startUI.SetActive (false);
		pointer.SetActive (true);

		if (photonView.isMine) {
			photonView.RPC("HideStartUI", PhotonTargets.OthersBuffered,photonView.viewID);
		}
	}

	[PunRPC] public void ShowEndUI(){
		startUI.SetActive (false);
		endUI.SetActive (true);

		if (photonView.isMine) {
			photonView.RPC("ShowEndUI", PhotonTargets.OthersBuffered,photonView.viewID);
		}
	}
}
