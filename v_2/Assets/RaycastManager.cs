using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour {

	public int raycastDistance;
	public float end_game_raycast_distance;
	public LayerMask layers;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		Raycast();

	}

	void Raycast() {
		Vector3 forward = transform.forward;
		RaycastHit hit;

		if (Physics.Raycast(transform.position, forward, out hit, raycastDistance, layers)) {
			string tag = hit.collider.gameObject.tag;
			Debug.Log("weve got a hit! object that was hit: " + hit.collider.gameObject.name);
			Debug.Log("the collision object has a tag: " + hit.collider.gameObject.tag);

			// game over
			if (tag == "Player" && hit.distance < end_game_raycast_distance) {
//				GameObject.Find ("TestCube").gameObject.GetComponent<TestCubeBehavior> ().NewRaycastHit ();
				Debug.Log ("we hit the player game over!!!!");
			}
		}
	}
}