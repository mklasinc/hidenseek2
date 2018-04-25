using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class CanvasManager : Photon.MonoBehaviour {

	public GameObject startUI;
	public GameObject endUI;


	// Use this for initialization
	void Start () {
		ShowStartUI ();
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

	[PunRPC] public void ShowStartUI(){
		Debug.Log ("we are called!");
		startUI.SetActive (true);

		if (photonView.isMine) {
			StartCoroutine(WaitBeforeHidingStartUI(5));
			photonView.RPC("ShowStartUI", PhotonTargets.OthersBuffered,photonView.viewID);
		}
	}

	IEnumerator WaitBeforeHidingStartUI(int s){
		yield return new WaitForSeconds(s);
		HideStartUI ();
	}

	[PunRPC] public void HideStartUI(){
		Debug.Log ("hide start ui");
		startUI.SetActive (false);

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
