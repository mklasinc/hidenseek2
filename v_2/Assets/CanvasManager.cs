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

	[PunRPC] public void ShowStartUI(){
		startUI.SetActive (true);

		if (pv.isMine) {
			photonView.RPC("ShowStartUI", PhotonTargets.OthersBuffered,photonView.viewID);
		}
	}

	[PunRPC] public void ShowEndUI(){
		startUI.SetActive (false);
		endUI.SetActive (true);

		if (pv.isMine) {
			photonView.RPC("ShowEndUI", PhotonTargets.OthersBuffered,photonView.viewID);
		}
	}
}
