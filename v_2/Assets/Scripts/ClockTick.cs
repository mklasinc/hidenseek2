using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class ClockTick : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void KillClockTick(){
		Destroy (this.gameObject);
	}
}
