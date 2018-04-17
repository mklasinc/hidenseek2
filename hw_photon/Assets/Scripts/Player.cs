using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Photon.MonoBehaviour {


	public float speed = 10f;
	public AudioClip myClip;

	PhotonView photonView;
	public GameObject emptyGameObject;

	void Start(){
		photonView = PhotonView.Get (this);
	}

	void Update()
	{
		if (!photonView.isMine) {
			SyncedMovement();
		}
	}

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (stream.isWriting)
		{
			stream.SendNext(rb.position);
			stream.SendNext(rb.velocity);
		}
		else
		{
			Vector3 syncPosition = (Vector3)stream.ReceiveNext();
			Vector3 syncVelocity = (Vector3)stream.ReceiveNext();

			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;

			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rb.position;
		}
	}

	private void SyncedMovement()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		syncTime += Time.deltaTime;
		rb.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	[PunRPC] public void playSound(){
		//Instansiate over network
		GameObject audioHolder = PhotonNetwork.Instantiate(emptyGameObject.name,gameObject.transform.position,Quaternion.identity,0);
		AudioSource asource = audioHolder.AddComponent<AudioSource> ();
		asource.clip = myClip;
		audioHolder.GetComponent<AudioSource> ().Play ();
		//Debug.Log ("my parent is " + gameObject.transform.parent.transform.parent.transform.parent);
		
		if (photonView.isMine)
			photonView.RPC ("playSound",PhotonTargets.AllBuffered,photonView.viewID);
	}
}
