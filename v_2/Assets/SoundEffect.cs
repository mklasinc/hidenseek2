using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {




	// Use this for initialization
	void Start () {
		StartCoroutine (DestroyObject (GetComponent<AudioSource> ().clip.length));
	}

	IEnumerator DestroyObject(float l){
		yield return new WaitForSeconds (l);
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}