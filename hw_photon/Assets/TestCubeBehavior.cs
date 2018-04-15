using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCubeBehavior : MonoBehaviour {
	float new_hit_time = 0F; 
	float time_since_last_hit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//get time elapsed since the last raycast hit
		time_since_last_hit = Time.time - new_hit_time;
		Debug.Log ("time since last hit is!");
		//if the elapsed time is more than a second (we haven't been hit for more than a second) - change the color back to white
		if (time_since_last_hit > 0.5F) {
			gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
		} else {
			gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
		}
	}

	public void NewRaycastHit(){
		Debug.Log ("we got hit again!");
		new_hit_time = Time.time;
	}

}
