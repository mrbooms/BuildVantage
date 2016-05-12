using UnityEngine;
using System.Collections;
using Photon;

public class BV_Buiding : Photon.MonoBehaviour 
{
	public bool allowToMove;

	//PARTICLES
	public GameObject particles;

	//THIS ARE THE FIELD FOR THE BUILDING DATA
	private bool payTaxes;
	private float popularity;
	private float influence;
	private float income;
	private float expenses;
	private float total;
	private Vector3 animPosition;
	private GameObject childTerrain;

	string lastState;

	public void setName(string name)
	{
		this.name = name;
	}

	//THIS INDICATE THE STATE OF THE BUILDING
	public enum stateEnum
	{
		Buy,
		Build,
		Renovate
	}
	public stateEnum myState;

	//THIS INDICATE THE OWNER OF THE BUILDING
	public enum ownerEnum
	{
		game,
		playerBlue,
		playerGreen,
		playerOrange,
		playerRed,
		playerYellow
	}
	public ownerEnum myOwner;

	//THIS INDICATE THE TYPE OF THE BUILDING
	public enum typeEnum
	{
		monument,
		prop,
		corner,
		residential,
		leisure,
	}
	public typeEnum myType;

	void Start()
	{
		myOwner = ownerEnum.game;
		lastState = myState + "";
		payTaxes = true;
		popularity = 10000;
		influence = 0;
		income = -70990;
		expenses = 79900;
		total = 5;

		if (myType == typeEnum.leisure) 
		{
			childTerrain = transform.Find("BV_Mesh_terrain").gameObject;
			animPosition = new Vector3(childTerrain.transform.position.x,
			                           childTerrain.transform.position.y+31f,
			                           childTerrain.transform.position.z);
		}
	}

	//GETTERS
	public bool getPayTaxes()
	{
		return payTaxes;
	}
	public float getPopularity()
	{
		return popularity;
	}
	public float getInfluence()
	{
		return influence;
	}
	public float getIncome()
	{
		return income;
	}
	public float getExpenses()
	{
		return expenses;
	}
	public float getTotal()
	{
		return total;
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit ();



		if (Physics.Raycast (ray, out hit)) 
		{
			if(hit.transform.gameObject.tag == "MAP" && allowToMove == true)
			{
				//print (hit.transform.gameObject.tag+" is hit at "+hit.point);
				transform.position = hit.point;

				if (Input.GetMouseButtonDown(1)) 
				{
					allowToMove = false;
				}

				//HANDLES BUILDING ROTATION
				if (Input.GetKey ("q")) {
					transform.rotation = new Quaternion (transform.rotation.x,
					                                     transform.rotation.y + 0.01f,
					                                     transform.rotation.z,
					                                     transform.rotation.w);
				} else if (Input.GetKey ("e")) {
					transform.rotation = new Quaternion (transform.rotation.x,
					                                     transform.rotation.y - 0.01f,
					                                     transform.rotation.z,
					                                     transform.rotation.w);
				}
			}
		}

		if (lastState != myState + "") 
		{
			//print ("####### STATE CHANGED but not CHANGED !");
			lastState = myState + "";
		}
		updateOwner ();
		updateState ();
	}

	public void updateState()
	{
		if(myState == stateEnum.Buy)
		{
			return;
		}
		else if(myState == stateEnum.Build)
		{
			return;
		}
		else if(myState == stateEnum.Renovate)
		{
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, animPosition, 10*Time.deltaTime);
			gameObject.GetComponent<BoxCollider>().enabled = true;
			childTerrain.GetComponent<BoxCollider>().enabled = false;
			childTerrain.SetActive(false);
			return;
		}
	}

	public void instantiateParticles(Vector3 pos)
	{
		Instantiate (particles, pos,Quaternion.identity);
	}
	public void updateOwner()
	{
		if(PhotonNetwork.player.ID == 1)
		{
			myOwner = ownerEnum.playerBlue;
			return;
		}
		else if(PhotonNetwork.player.ID == 2)
		{
			myOwner = ownerEnum.playerGreen;
			return;
		}
		else if(PhotonNetwork.player.ID == 3)
		{
			myOwner = ownerEnum.playerOrange;
			return;
		}
		else if(PhotonNetwork.player.ID == 4)
		{
			myOwner = ownerEnum.playerRed;
			return;
		}
		else if(PhotonNetwork.player.ID == 5)
		{
			myOwner = ownerEnum.playerYellow;
			return;
		}
		else
		{
			myOwner = ownerEnum.game;
			return;
		}
	}

	public void setState(int i)
	{
		if (i == 1) 
		{
			myState = stateEnum.Buy;
		}
		if (i == 2) 
		{
			myState = stateEnum.Build;
		}
		if (i == 3) 
		{
			myState = stateEnum.Renovate;
			instantiateParticles(childTerrain.transform.position);
		}
	}

	public void setOwner(int i)
	{
		gameObject.GetComponent<Renderer> ().material.EnableKeyword("_EMISSION");
		BV_Terrain terrainScript = childTerrain.GetComponent<BV_Terrain>();
		terrainScript.changeColor(i);
		terrainScript.sendRPC(i);
		
		if (i == 1) 
		{
			myOwner = ownerEnum.playerBlue;
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.blue);
			myState = stateEnum.Build;
		}
		else if (i  == 2)
		{
			myOwner = ownerEnum.playerGreen;
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.green);
			myState = stateEnum.Build;
		}
		else if (i  == 3)
		{
			myOwner = ownerEnum.playerOrange;
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.magenta);
			myState = stateEnum.Build;
		}
		else if (i  == 4)
		{
			myOwner = ownerEnum.playerRed;
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.red);
			myState = stateEnum.Build;
		}
		else if (i  == 5)
		{
			myOwner = ownerEnum.playerYellow;
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.yellow);
			myState = stateEnum.Build;
		}
		else
		{
			myOwner = ownerEnum.game;
		}
	}

	private PhotonView myPhotonView;
	
	void OnJoinedRoom()
	{
		myPhotonView = this.GetComponent<PhotonView> ();
	}
	
	public void sendRPCowner(int i)
	{
		this.photonView.RPC("rpcOwner", PhotonTargets.All, i);
	}
	
	[PunRPC]
	void rpcOwner(int i)
	{
		Debug.Log ("rpcOwner");
		setOwner(i);
	}

	public void sendRPCstate(int i)
	{
		this.photonView.RPC("rpcState", PhotonTargets.All, i);
	}
	
	[PunRPC]
	void rpcState(int i)
	{
		Debug.Log ("TEST");
		setState (i);
	}

}
