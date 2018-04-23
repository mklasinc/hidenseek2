using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Photon.MonoBehaviour {

	public AudioClip officeAudio;
	public AudioSource sourceAudio;

	// Use this for initialization
	void Start () {
		sourceAudio = GetComponent<AudioSource> ();


	}
	
	// Update is called once per frame
	void Update () {
		if (!sourceAudio.isPlaying) {
			playBGMusic ();
		}
			
	}

	//function to play background music 
	[PunRPC] public void playBGMusic(){
		sourceAudio.Play ();
		if(photonView.isMine) {
			photonView.RPC ("playBGMusic", PhotonTargets.OthersBuffered, 0);
		}
	}
}
