using UnityEngine;
using System.Collections;
using Photon;

public class BV_Terrain : Photon.MonoBehaviour
{
	//THIS INDICATE IF THE TERRAIN IS SELECTED
	public bool selected = false;

	//CHANGE TO THE CORRECT COLOR DEPENDING OF THE GAME OWNER
	public void changeColor(int i)
	{

		if (i == 1) 
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.blue);
		}
		else if (i  == 2)
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.green);
		}
		else if (i  == 3)
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.magenta);
		}
		else if (i  == 4)
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.red);
		}
		else if (i  == 5)
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.yellow);
		}
		else
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.white);
		}
	}

	//TO DELETE

	private PhotonView myPhotonView;

	void OnJoinedRoom()
	{
		myPhotonView = this.GetComponent<PhotonView> ();

		if (PhotonNetwork.isMasterClient)
		{
			myPhotonView.viewID = PhotonNetwork.AllocateSceneViewID();
		}
	}

	public void sendRPC(int i)
	{
		this.photonView.RPC("Test", PhotonTargets.All, i);
	}

	[PunRPC]
	void Test(int i)
	{
		Debug.Log ("TEST");
		changeColor (i);
	}
}
