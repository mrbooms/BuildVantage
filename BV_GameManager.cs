using UnityEngine;
using System.Collections;

public class BV_GameManager : Photon.MonoBehaviour {

	/*
	RaycastHit hit;
	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

	public bool ClickedCollider ()
	{
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		return (Physics.Raycast (ray, out hit));
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			if(ClickedCollider())
			{
				print ("OBJECT CLICKED = "+hit.transform.name);
				print ("PLAYER THAT CLICKED = "+"ID = "+PhotonNetwork.player.ID);
				BV_Terrain script = hit.transform.gameObject.GetComponent<BV_Terrain>();
				script.playerNr = PhotonNetwork.player.ID;
			}
		}
	}*/

    void OnGUI()
    {
        if (PhotonNetwork.room == null) return; //Only display this GUI when inside a room

        if (GUILayout.Button("Leave Room"))
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("OnDisconnectedFromPhoton");
    }   
	
}
