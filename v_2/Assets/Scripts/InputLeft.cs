using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script manages controller input. Here we use trigger or press to move a game object.
// Attach this script to each controller (Controller Left or Controller Right)
public class InputLeft : Photon.MonoBehaviour {
	// Getting a reference to the controller GameObject
	private SteamVR_TrackedObject trackedObj;
	// Getting a reference to the controller Interface
	private SteamVR_Controller.Device Controller;

	// manager
	GameObject gameManager;

	// player ready variable
	bool playerIsReady = false;

	void Start(){
	}

	void Awake()
	{
		// initialize the trackedObj to the component of the controller to which the script is attached
		trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
		gameManager = GameObject.FindGameObjectWithTag ("Manager");
	}

	// Update is called once per frame
	void Update () {
		Controller = SteamVR_Controller.Input((int)trackedObj.index);

		// Getting the Touchpad Axis
		if (Controller.GetAxis() != Vector2.zero)
		{
			Debug.Log(gameObject.name + Controller.GetAxis());
		}

		// Getting the Trigger press
		if (Controller.GetHairTriggerDown())
		{


		}

		// Getting the Trigger Release
		if (Controller.GetHairTriggerUp())
		{
			
		}

		// Getting the Grip Press
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{

		}

		// Getting the Grip Release
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
	
		}
	}
}
