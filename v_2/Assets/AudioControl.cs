using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : Photon.MonoBehaviour {

	PhotonView photonView;
	public AudioSource audio;

	// Use this for initialization
	void Start () {
		photonView = PhotonView.Get (this);
		if (Input.GetKeyDown(KeyCode.S)){
			playBGSound();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			playBGSound ();
		}
	}
	[PunRPC] void playBGSound(){
		audio.Play ();
		if (photonView.isMine) {
			photonView.RPC ("playBGSound", PhotonTargets.Others, 0);
		}
	}
}

