using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Photon.MonoBehaviour {
	public AudioSource audioobj;
	public AudioClip steps;

	// Use this for initialization
	void Start () {
		audioobj = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			if(Input.GetKeyDown(KeyCode.S))
				makeSound ();
		}
	}

	[PunRPC] void makeSound(){
		audioobj.Play ();
		if(photonView.isMine) {
			photonView.RPC ("makeSound", PhotonTargets.OthersBuffered, 0);
		}
	}
}
