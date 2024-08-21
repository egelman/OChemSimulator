using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using VELConnect;
using VelNet;
using VELShareUnity;
using VelUtils;
using VelUtils.VRInteraction;

public class GameManager : MonoBehaviour
{
	public Rig rig;
	public OVRHand trackedHandLeft;
	public OVRHand trackedHandRight;
	public string roomToJoin = "default";
	public Player playerPrefab;
	public Player player;
	public TMP_Text infoText;
	public List<VRGrabbableHand> grabbers = new List<VRGrabbableHand>();
	// Start is called before the first frame update
	IEnumerator Start()
	{
		bool isLoggedIn = false;
		VelNetManager.OnLoggedIn += () =>
		{
			isLoggedIn = true;
		};

		while(!isLoggedIn)
		{
			yield return null;
		}

		VelNetManager.OnJoinedRoom += (room) =>
		{
			//instantiate the player prefab
			player = VelNetManager.NetworkInstantiate(playerPrefab.name).GetComponent<Player>();
			player.r = rig;
			player.trackedHandLeft.setOVRHand(trackedHandLeft);
			player.trackedHandRight.setOVRHand(trackedHandRight);
		};

		VelNetManager.JoinRoom(roomToJoin);

		while (!VelNetManager.InRoom)
		{
			yield return null;
		}

		Debug.Log("joined room " + VelNetManager.Room);
		MoleculeManager mg = GameObject.FindObjectOfType<MoleculeManager>();

		foreach(VRGrabbableHand grabber in grabbers)
		{
			grabber.OnGrab += (grabbable) =>
			{
				grabbable.GetComponent<NetworkObject>()?.TakeOwnership();
				if (grabbable.tag == "Carbon" || grabbable.tag == "Nitrogen" || grabbable.tag == "Oxygen")
				{
					mg.networkObject.TakeOwnership();
				}
			};
		}
		VelNetManager.RoomDataReceived += message =>
		{
			Debug.Log(message.room);
		};

		StartCoroutine(GetRoomList());

	}

	IEnumerator GetRoomList()
	{
		while (true)
		{
			VelNetManager.GetRooms();
			yield return new WaitForSeconds(1.0f);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
