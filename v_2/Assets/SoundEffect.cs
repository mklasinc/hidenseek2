using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {
<<<<<<< HEAD


	// Use this for initialization
	void Start () {
		StartCoroutine (DestroyObject(GetComponent<AudioSource>().clip.length));
=======
	// Use this for initialization
	void Start () {
		StartCoroutine (DestroyObject (GetComponent<AudioSource> ().clip.length));
>>>>>>> 837df0784b4e187169dafb98ba0c6a22499ccd89
	}

	IEnumerator DestroyObject(float l){
		yield return new WaitForSeconds (l);
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
<<<<<<< HEAD
=======

>>>>>>> 837df0784b4e187169dafb98ba0c6a22499ccd89
