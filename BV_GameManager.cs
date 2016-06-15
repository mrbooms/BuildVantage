using UnityEngine;
using System.Collections;



public class BV_GameManager : Photon.MonoBehaviour {

    [SerializeField]
    private GameObject map;

    void Start()
    {
        Instantiate(map, new Vector3(-4000, 0, -4000), Quaternion.identity);
    }

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
