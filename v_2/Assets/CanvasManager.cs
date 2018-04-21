using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Photon.MonoBehaviour {

	public GameObject startUI;
	public GameObject endUI;

	PhotonView pv;

	// Use this for initialization
	void Start () {
		pv = PhotonView.Get (this);
//		ShowStartUI ();


	}
	
	// Update is called once per frame
	void Update () {

		if (pv.isMine) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				Debug.Log ("switch to end UI");
				ShowEndUI ();
			}
		}
		
	}

	[PunRPC] public void ShowStartUI(int num){
		Debug.Log ("we are called!");
		startUI.SetActive (true);

		if (pv.isMine) {
			pv.RPC("ShowStartUI", PhotonTargets.OthersBuffered,pv.viewID);
		}
	}

	[PunRPC] public void ShowEndUI(){
		startUI.SetActive (false);
		endUI.SetActive (true);

		if (pv.isMine) {
			pv.RPC("ShowEndUI", PhotonTargets.OthersBuffered,pv.viewID);
		}
	}
}
