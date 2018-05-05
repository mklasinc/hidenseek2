using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SoundEffectsManager : Photon.MonoBehaviour {

	public GameObject footstepSound;
	public GameObject winnerSound;
	public GameObject loserSound;
	public GameObject spawnP;

	// Use this for initialization
	void Start () {
		
//		InstantiateFootsteps ("footsteps", spawnP.transform.position); // seeker
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InstantiateFootsteps(string sEffect,Vector3 pos){
		if (sEffect == "footsteps") {
			Debug.Log ("instantiate footsteps");
			GameObject effect;
			PhotonNetwork.Instantiate(footstepSound.name, pos, Quaternion.identity, 0);
		}; 

	}
}
