using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderSoundManager : MonoBehaviour {

	AudioSource s;
	// Use this for initialization
	void Start () {
		s = gameObject.GetComponent<AudioSource> ();
		s.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
