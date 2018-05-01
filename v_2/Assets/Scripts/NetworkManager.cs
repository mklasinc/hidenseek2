﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This network manager creates a room, manages player instantiation (finding rig etc) and their spawn positions

public class NetworkManager : MonoBehaviour {

	//Name of room (change it to whatever you'd like to have users see on the GUI screen)
	private const string roomName = "VRlab";
	private RoomInfo[] roomsList;
	//private byte numPlayers = 8;

	//Prefabs for the player, the cube to represent the HTC headset, and the capsule to represent the HTC controllers
	public GameObject playerprefab;
	public GameObject headsetcubeprefab;
	public GameObject capsulehand;
	public GameObject spawnPoint1;
	public GameObject spawnPoint2;


	// spawn hider and seeker prefabs
	public GameObject seekerprefab;
	public GameObject hiderprefab;
	// canvas prefab
	public GameObject canvasprefab;

	//sound manager
	public GameObject soundprefab;


	//Array to hold the spawn points in the scene and an array to see which spawn points have been taken
	public Transform[] spawnPoints;
	public bool[] spawnPointTaken;
	// Use this for initialization
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		PhotonNetwork.autoJoinLobby = true;


        if(spawnPoint1 && spawnPoint2) {
            spawnPoints = new Transform[2];
            spawnPoints[0] = spawnPoint1.transform;
            spawnPoints[1] = spawnPoint2.transform;
        }

		spawnPointTaken = new bool[2];
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.B)) {
			PhotonNetwork.Instantiate ("SoundEffect", Vector3.zero, Quaternion.identity, 0);
		}
	}

	//Photon function for GUI list of available rooms
	void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}
		else if (PhotonNetwork.room == null)
		{
			// Create Room
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				PhotonNetwork.CreateRoom(roomName, new RoomOptions(){MaxPlayers = 6, IsVisible = true}, null);

			// Join Room
			if (roomsList != null)
			{
				Debug.Log ("rooms list length: " + roomsList.Length);
				for (int i = 0; i < roomsList.Length; i++)
				{
					if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].Name))
						PhotonNetwork.JoinRoom(roomsList[i].Name);
				}
			}
		}
	}

	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
	}

	void OnJoinedRoom()
	{

		Debug.Log ("Current number of players is: " + PhotonNetwork.countOfPlayers);
		//Waiting for rig to come into the network and connect the player
		StartCoroutine (WaitForRig ());

		//might need to check photon view is mine here
		if (PhotonNetwork.countOfPlayers > 1) {
			//instantiate sound if there is more than one player
			Vector3 spawnSound = Vector3.zero;
			PhotonNetwork.Instantiate (soundprefab.name, spawnSound, Quaternion.identity,0);
		}

        //Place the rig at a spawn point
        Vector3 spawnLocation;
        if (spawnPoints.Length > 0) {
            
            if (!spawnPointTaken[0]) {
                spawnLocation = spawnPoints[0].position;
                spawnPointTaken[0] = true;
            }
            else {
                spawnLocation = spawnPoints[1].position;
                spawnPointTaken[1] = true;
            }
        }else {
            Debug.Log("No Spawn points assigned! Instantiating at 0, 0, 0");
            spawnLocation = Vector3.zero;
		};

		// instantiate a hider or a seeker
		// if no one is in the room seeker is spawned - else hider is spawn
		if (PhotonNetwork.playerList.Length == 1) {
			GameObject.Instantiate (seekerprefab, spawnLocation, Quaternion.identity);
			Debug.Log ("spawning a seeker!");
			//canvasprefab.GetComponent<CanvasManager>().ShowStartUI(1);
//			PhotonNetwork.Instantiate (canvasprefab.name, spawnLocation, Quaternion.identity,0);

		} else {
			GameObject.Instantiate (hiderprefab, spawnLocation, Quaternion.identity);
			Debug.Log ("spawning a hider!");
			PhotonNetwork.Instantiate (canvasprefab.name, spawnLocation, Quaternion.identity,0);
			// only show the beginning UI if the hider has spawned
//			hiderprefab.GetComponentInChildren<CanvasManager>().ShowStartUI();
		};

		Debug.Log ("Creating new player and spawn position is " + spawnLocation);

		//playerprefab is a camera rig for HTC Vive
//		GameObject.Instantiate (playerprefab, spawnLocation, Quaternion.identity);

	}

	//function to free up spawn points on disconnect
	void onLeftRoom(){
		GameObject playerRemaining = GameObject.FindGameObjectWithTag ("player");
		if (playerRemaining.transform.position == spawnPoint1.transform.position) {
			spawnPointTaken [1] = false;
		} else {
			spawnPointTaken [0] = false;
		}
	}

	// Helper function to find the rig
	IEnumerator WaitForRig(){

		Debug.Log(PhotonNetwork.countOfPlayers);

		yield return new WaitForSeconds (1);

		//Find headset and instaniate cube ON NETWORK -- set headset as cube's parent
		GameObject headset = GameObject.Find ("Camera (eye)");
		GameObject photonCube = PhotonNetwork.Instantiate(headsetcubeprefab.name, headset.transform.position, Quaternion.identity, 0);

		photonCube.transform.SetParent (headset.transform);


		//Find the controllers and instantiate capsules ON NETOWRK -- set controllers as the parents of the capsules
       
        //Find left controller
		GameObject controllerLeft = GameObject.Find ("Controller (left)/Model");
		Debug.Log (controllerLeft);
		GameObject capsuleHandLeft = PhotonNetwork.Instantiate(capsulehand.name, controllerLeft.transform.position, Quaternion.identity, 0);

		capsuleHandLeft.transform.SetParent (controllerLeft.transform);

		//Now for right controller

		GameObject controllerRight = GameObject.Find("Controller (right)/Model");
		GameObject capsuleHandRight = PhotonNetwork.Instantiate(capsulehand.name, controllerRight.transform.position, Quaternion.identity, 0);
		capsuleHandRight.transform.SetParent (controllerRight.transform);

		// if we spawn a hider make the size of the body parts smaller
		if (PhotonNetwork.countOfPlayers > 1) {
			photonCube.transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
			capsuleHandRight.transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
			capsuleHandLeft.transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
		}

	}

	public void PlaySFX(Vector3 sound_position, string sound_name){
		PhotonNetwork.Instantiate (sound_name, sound_position, Quaternion.identity,0);
	}

}
