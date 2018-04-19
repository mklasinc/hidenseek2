using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Photon.MonoBehaviour {

	public GameObject StartGameUI;
	PhotonView photonView;
	// Use this for initialization
	void Start () {
		StartGameUI.SetActive (false);
		photonView = PhotonView.Get (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
