using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class CanvasManager : Photon.MonoBehaviour {

	public GameObject startUI;
	public GameObject endUI;
	public GameObject timerUI;
	GameObject pointer;

	bool gameOn = false;


	// Use this for initialization
	void Start () {
		pointer = GameObject.FindGameObjectWithTag ("Pointer");
		ShowStartUI (1);
	}
	
	// Update is called once per frame
	void Update () {

//		if (photonView.isMine) {
//			if (Input.GetKeyDown (KeyCode.Space)) {
//				Debug.Log ("switch to end UI");
//				ShowEndUI ();
//			}
//		}
		
	}

	// show start ui

	[PunRPC] public void ShowStartUI(int n){
		Debug.Log ("show start ui!");
		// activate the start ui
		startUI.SetActive (true);
		// show the right text
		Debug.Log ("look for finder and seeker");
		GameObject h = GameObject.Find("Hider(Clone");
		Debug.Log (h);
		GameObject s = GameObject.Find("Seeker(Clone");
		Debug.Log (h);
		string m = "empty string";
		if (h != null) {
			m = "you are a hider!";
		} else if (s != null) {
			m = "you are a seeker!";
		};
		startUI.GetComponentInChildren<Text>().text = m;

		// activate the teleportation pointer
		pointer.SetActive (false);

		if (photonView.isMine) {
//			StartCoroutine(WaitBeforeHidingStartUI(10));
			photonView.RPC("ShowStartUI", PhotonTargets.OthersBuffered,photonView.viewID);
		}
	}

	[PunRPC] public void PlayerReady(bool b){
		


	}

	IEnumerator WaitBeforeHidingStartUI(int s){
		Debug.Log ("waiting ...");
		yield return new WaitForSeconds(s);
		Debug.Log ("calling hide start ui");
		HideStartUI (1);
	}

	[PunRPC] public void HideStartUI(int n){
		if (!gameOn) {
			Debug.Log ("hide start ui");
			startUI.SetActive (false);
			pointer.SetActive (true);
			photonView.RPC("HideStartUI", PhotonTargets.OthersBuffered,photonView.viewID);
			Debug.Log ("we have called the other");
			if (photonView.isMine) {
				Debug.Log ("call other!");
				photonView.RPC("HideStartUI", PhotonTargets.OthersBuffered,photonView.viewID);
			};
			Debug.Log ("activate timer UI!");
			timerUI.SetActive (false);
			GameObject.FindGameObjectWithTag ("Manager").GetComponent<NetworkManager> ().StartGameTimer ();
//			ShowTimerUI ();
			gameOn = true;
		}

//		StartGame ();
	}

	[PunRPC] public void ShowTimerUI(int n){
		timerUI.SetActive (false);
//		if (photonView.isMine) {
//			Debug.Log ("call other!");
//			photonView.RPC("HideStartUI", PhotonTargets.OthersBuffered,photonView.viewID);
//		};
	}

	[PunRPC] public void ShowEndUI(){
		startUI.SetActive (false);
		endUI.SetActive (true);

		if (photonView.isMine) {
			photonView.RPC("ShowEndUI", PhotonTargets.OthersBuffered,photonView.viewID);
		}
	}
}
