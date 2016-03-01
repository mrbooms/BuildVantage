using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class BV_RoomMenu : MonoBehaviour
{
	
	void Awake()
	{
		//PhotonNetwork.logLevel = NetworkLogLevel.Full;
		
		//Connect to the main photon server. This is the only IP and port we ever need to set(!)
		if (!PhotonNetwork.connected)
			PhotonNetwork.ConnectUsingSettings("v1.0"); // version of the game/demo. used to separate older clients from newer ones (e.g. if incompatible)
		
		//Load name from PlayerPrefs
		PhotonNetwork.playerName = PlayerPrefs.GetString("playerName", "Guest" + Random.Range(1, 9999));
		
	}
	
	private string roomName = "myRoom";
	private Vector2 scrollPos = Vector2.zero;

	private string oldPlayerName;

	void Update()
	{
		//print ("LENGHT = " + PhotonNetwork.GetRoomList ().Length);

		//UPDATE PLAYER PREFS IF CHANGED
		if (oldPlayerName != GameObject.Find ("Field_PlayerName").GetComponent<InputField>().text) 
		{
			PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
		}
			
		PhotonNetwork.playerName = GameObject.Find ("Field_PlayerName").GetComponent<InputField>().text;
		oldPlayerName = PhotonNetwork.playerName;

		//UPDATE ROOM LIST FIELD
		if (PhotonNetwork.GetRoomList().Length == 0)
		{
			GameObject.Find("Text_RoomListing").GetComponent<Text>().text = ("..no games available..");
		}
		else
		{
			GameObject.Find("Text_RoomListing").GetComponent<Text>().text = "";

			foreach (RoomInfo game in PhotonNetwork.GetRoomList())
			{
				GameObject.Find("Text_RoomListing").GetComponent<Text>().text = GameObject.Find("Text_RoomListing").GetComponent<Text>().text + game.name;
			}
		}
	}

	public void joinRoom()
	{
		if (PhotonNetwork.GetRoomList ().Length != 0) 
		{
			roomName = GameObject.Find ("Field_JoinRoom").GetComponent<InputField>().text;
			PhotonNetwork.JoinRoom(roomName);
			print ("TEST / JOINED ?");
			GameObject.Find ("GameScripts").GetComponent<BV_RoomMenu> ().enabled = false;
			GameObject.Find ("RoomMenu").SetActive (false);
		}
	}

	public void createRoom()
	{
		roomName = GameObject.Find ("Field_CreateRoom").GetComponent<InputField>().text;
		PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 5 }, TypedLobby.Default);
		print ("TEST / CREATED ?");
		GameObject.Find ("GameScripts").GetComponent<BV_RoomMenu> ().enabled = false;
		GameObject.Find ("RoomMenu").SetActive (false);
		GameObject[] terrains = GameObject.FindGameObjectsWithTag ("TERRAIN");

		//ADD AND INITIALIZE COLLIDERS FOR LEISURE TERRAINS FOR SALES
		foreach (GameObject terrain in terrains) 
		{
			terrain.AddComponent<BoxCollider>();
			terrain.GetComponent<BoxCollider>().center = new Vector3(terrain.GetComponent<BoxCollider>().center.x,
			                                                         3,
			                                                         terrain.GetComponent<BoxCollider>().center.z);
			terrain.GetComponent<BoxCollider>().size = new Vector3(terrain.GetComponent<BoxCollider>().size.x,
			                                                         1,
			                                                         terrain.GetComponent<BoxCollider>().size.z);
		}
	}

	public void joinRandom()
	{
		if (PhotonNetwork.GetRoomList ().Length != 0) 
		{
			PhotonNetwork.JoinRandomRoom();
			print ("TEST / RANDOMED ?");
			GameObject.Find ("GameScripts").GetComponent<BV_RoomMenu> ().enabled = false;
			GameObject.Find ("RoomMenu").SetActive (false);
		}
	}

	void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			ShowConnectingGUI();
			return;   //Wait for a connection
		}
		
		
		if (PhotonNetwork.room != null)
			return; //Only when we're not in a Room


	}

	void ShowConnectingGUI()
	{
		GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));
		
		GUILayout.Label("Connecting to Photon server.");
		GUILayout.Label("Hint: This demo uses a settings file and logs the server address to the console.");
		
		GUILayout.EndArea();
	}
	
	public void OnConnectedToMaster()
	{
		// this method gets called by PUN, if "Auto Join Lobby" is off.
		// this demo needs to join the lobby, to show available rooms!
		
		PhotonNetwork.JoinLobby();  // this joins the "default" lobby
	}
}
