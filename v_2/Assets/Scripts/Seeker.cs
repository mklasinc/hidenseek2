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
		if (Input.GetKeyDown (KeyCode.S)) {
			Debug.Log ("S was pressed");
			makeSound ();
		}
	}

	[PunRPC] void makeSound(){
		audioobj.Play ();
		if(photonView.isMine) {
			Debug.Log ("Sending audio to everyone!");
			photonView.RPC ("makeSound", PhotonTargets.OthersBuffered, 0);
		}
	}
}
