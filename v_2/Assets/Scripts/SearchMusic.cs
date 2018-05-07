using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SearchMusic : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void KillSearchSound(){
		Destroy (this.gameObject);
	}
}
