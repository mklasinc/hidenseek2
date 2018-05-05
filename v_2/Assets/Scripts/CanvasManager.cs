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
		GameObject h = GameObject.Find("Hider(Clone)");
		Debug.Log (h);
		GameObject s = GameObject.Find("Seeker(Clone)");
		Debug.Log (h);
		string m = "empty string";
		if (h != null) {
			m = "you are a hider!";
		} else if (s != null) {
			m = "Oh no! \nYou brought your mischevious daughter to work, but she escaped the kids' room during lunch break. Make sure to seek her out before your boss gets mad at you";
		};
		startUI.GetComponentInChildren<Text>().text = m;

		// activate the teleportation pointer
		pointer.SetActive (false);

//		if (photonView.isMine) {
//			StartCoroutine(WaitBeforeHidingStartUI(10));
//			photonView.RPC("ShowStartUI", PhotonTargets.OthersBuffered,photonView.viewID);
//		}
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
//			photonView.RPC("HideStartUI", PhotonTargets.AllBuffered,photonView.viewID);
			Debug.Log ("activate timer UI!");
			timerUI.SetActive (true);
//			GameObject.FindGameObjectWithTag ("Manager").GetComponent<NetworkManager> ().StartGameTimer ();
//			ShowTimerUI ();
			gameOn = true;
		}

//		StartGame ();
	}

	public void UpdateTimerUI(string m){
		timerUI.GetComponentInChildren<Text> ().text = m;
//		if (photonView.isMine) {
//			Debug.Log ("call other!");
//			photonView.RPC("HideStartUI", PhotonTargets.OthersBuffered,photonView.viewID);
//		};
	}

	// END GAME UI

	[PunRPC] public void ShowEndUI(string winner){
		// find the hider and the seeker
		GameObject h = GameObject.Find("Hider(Clone)");
		GameObject s = GameObject.Find("Seeker(Clone)");

		// hide timer ui and show end ui
		timerUI.SetActive (false);
		endUI.SetActive (true);

		// ui text dipsplay logic
		if (winner == "seeker") {
			endUI.GetComponentInChildren<Text> ().text = "seeker won!";
		} else if (winner == "hider") {
			endUI.GetComponentInChildren<Text> ().text = "hider won!";
		} else {
			
		}
			
	}

//	public void EndGame(string w){
//		Debug.Log ("someone is saying that the game is over!");
//		if (gameOn) {
//			PhotonView.Get(this).RPC("ShowEndUI", PhotonTargets.AllBuffered, w);
//		}
//
//	}
}
