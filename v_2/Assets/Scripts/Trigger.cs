using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	GameObject netman;

	public string sfx_name;
	void OnTriggerEnter (Collider col){
		//call network manager and give position and type(what kind of sound)
		//find network manager
		Debug.Log ("A collision was detected");
		if(col.gameObject.name == "hsCube(Clone)")
		{
			netman = GameObject.Find ("Manager");
			netman.GetComponent<NetworkManager> ().PlaySFX (col.gameObject.transform.position, sfx_name);
		}

	}
}
