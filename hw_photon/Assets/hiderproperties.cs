using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hiderproperties : MonoBehaviour {

	public GameObject body;

	// Use this for initialization
	void Start () {
		body = GameObject.Find ("hsCube(Clone)");
		body.GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
