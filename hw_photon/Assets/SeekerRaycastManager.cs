﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerRaycastManager : MonoBehaviour {

    public int raycastDistance;
    public float end_game_raycast_distance;
    public LayerMask layers;
    // Use this for initialization
    void Start () {
		
	}

    void Update() {
        Raycast();

    }

    void Raycast() {
        Vector3 forward = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, forward, out hit, raycastDistance, layers)) {
            string tag = hit.collider.gameObject.tag;
            Debug.Log("weve got a hit!");
            Debug.Log(hit.collider.gameObject.name);

            // game over
            if (tag == "Interactable" && hit.distance < end_game_raycast_distance) {
                Debug.Log("the distance from the cube is:" + hit.distance);
                GameObject.Find("TestCube").gameObject.GetComponent<TestCubeBehavior>().NewRaycastHit();

            }
        }
    }
}
