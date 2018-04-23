using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Photon.MonoBehaviour {
	PhotonView photonView;
	public AudioSource audioobj;
	public AudioClip steps;

	// Use this for initialization
	void Start () {
		photonView = PhotonView.Get (this);
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
	}
}
