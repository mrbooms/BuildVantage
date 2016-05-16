using UnityEngine;
using System.Collections;
using Photon;

public class BV_Buiding : Photon.MonoBehaviour 
{
	[SerializeField] private int ownerID;

	public bool allowToMove;

	//PARTICLES
	public GameObject particles;

	//THIS ARE THE FIELD FOR THE BUILDING DATA
	[SerializeField] private float price;
	[SerializeField] private bool payTaxes;
	[SerializeField] private float taxes;
	[SerializeField] private float popularity;
	[SerializeField] private float influence;
	[SerializeField] private float income;
	[SerializeField] private float expenses;
	[SerializeField] private float total;
	[SerializeField] private Vector3 animPosition;
	[SerializeField] private GameObject childTerrain;
	public float distance;
	public float age;

	string lastState;

	public void setName(string name)
	{
		this.name = name;
	}

	public int getID()
	{
		if(myOwner == ownerEnum.playerBlue)
		{
			ownerID = 1;
		}
		else if(myOwner == ownerEnum.playerGreen)
		{
			ownerID = 2;
		}
		else if(myOwner == ownerEnum.playerOrange)
		{
			ownerID = 3;
		}
		else if(myOwner == ownerEnum.playerRed)
		{
			ownerID = 4;
		}
		else if(myOwner == ownerEnum.playerYellow)
		{
			ownerID = 5;
		}
		else if(myOwner == ownerEnum.game)
		{
			myOwner = ownerEnum.game;
			ownerID = 0;
		}
		return ownerID;
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

	public void setValues ()
	{
		distance = Vector3.Distance (transform.position, Vector3.zero);
		price = 10000 - distance;


		//SET POPULARITY
		if (payTaxes == true) 
		{
			popularity = popularity + 10f;
		}
		else if (payTaxes == false) 
		{
			popularity = popularity - 10f;
		}
		//SET INFLUENCE
		if (distance <= 1000) 
		{
			influence = 1000;
		}
		else if (distance <= 2000 && distance > 1000) 
		{
			influence = 500;
		}
		else if (distance <= 3000 && distance > 2000) 
		{
			influence = 100;
		}
		else if (distance > 3000) 
		{
			influence = 0;
		}
		income = (price/12)+influence+popularity;
		expenses = 30 + age;

		//SET Taxes
		if (payTaxes == true) 
		{
			float netto = income - ((income / 100) * 25);
			taxes = income - netto;
		}
		else if (payTaxes == false) 
		{
			taxes = 0;
		}
		total = income - expenses - taxes + influence;

		if (myState != stateEnum.Renovate) 
		{
			income = 0;
			expenses = 0;
			total = 0;
		}
	}

	void Start()
	{
		distance = 0f;
		ownerID = 0;
		myOwner = ownerEnum.game;
		lastState = myState + "";
		payTaxes = true;
		setValues ();
		if (myType == typeEnum.leisure) 
		{
			childTerrain = transform.Find("BV_Mesh_terrain").gameObject;
			animPosition = new Vector3(transform.position.x,
			                           0f,
			                           transform.position.z);
		}
	}

	//GETTERS
	public bool getPayTaxes()
	{
		return payTaxes;
	}
	public float getTaxes()
	{
		return taxes;
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
		if (myState == stateEnum.Renovate) 
		{
			age++;
		}


		if (age == 500 && myType == typeEnum.leisure && myState == stateEnum.Renovate) {
			myOwner = ownerEnum.game;
			gameObject.GetComponent<Renderer> ().material.EnableKeyword ("_EMISSION");
			gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.white);
			if (gameObject.GetComponent<BoxCollider> ()) {
				gameObject.GetComponent<BoxCollider> ().enabled = false;
			}
			instantiateParticles (transform.position);
		} 

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
			//updateOwner (PhotonNetwork.player.ID);
			updateState ();
			lastState = myState + "";
			setValues ();
		}
		updateState ();
		lastState = myState + "";
		setValues ();
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
	public void updateOwner(int i)
	{
		if(i == 1)
		{
			myOwner = ownerEnum.playerBlue;
			return;
		}
		else if(i == 2)
		{
			myOwner = ownerEnum.playerGreen;
			return;
		}
		else if(i == 3)
		{
			myOwner = ownerEnum.playerOrange;
			return;
		}
		else if(i == 4)
		{
			myOwner = ownerEnum.playerRed;
			return;
		}
		else if(i == 5)
		{
			return;
		}
		else
		{
			myOwner = ownerEnum.game;
			ownerID = 0;
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
