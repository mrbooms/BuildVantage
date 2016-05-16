using UnityEngine;
using System.Collections;
using Photon;

public class BV_BuildingManager : Photon.MonoBehaviour {

	//DECIDE OF THE OWNER OF THIS OBJECT
	[SerializeField] private int ownerID;

	[SerializeField] private Vector3 startPos;

	//ALLOW IT TO MOVE IF IN EDITOR
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

	//TO CHECK IF THE STATE HAS CHANGED
	string lastState;

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

	//SETTERS :

	public void setName(string name)
	{
		this.name = name;
	}

	//GETTERS :

	public int getOwnerID()
	{
		return ownerID;
	}

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

	//INITIALIZE THINGS FOR SWAPING FROM BUY TO BUILD STATE
	public void BuyToBuild(int i)
	{
		//INSTANTIATE CHILD SCRIPT
		BV_Terrain terrainScript = childTerrain.GetComponent<BV_Terrain>();
		//CHANGE CHILD COLOR
		terrainScript.changeColor(i);
		terrainScript.sendRPC(i);

		switch(i)
		{
		case 0:
			myOwner = ownerEnum.game;
			myState = stateEnum.Build;
			setValues();
			break;
		case 1:
			myOwner = ownerEnum.playerBlue;
			myState = stateEnum.Build;
			setValues();
			break;
		case 2:
			myOwner = ownerEnum.playerGreen;
			myState = stateEnum.Build;
			setValues();
			break;
		case 3:
			myOwner = ownerEnum.playerOrange;
			myState = stateEnum.Build;
			setValues();
			break;
		case 4:
			myOwner = ownerEnum.playerRed;
			myState = stateEnum.Build;
			setValues();
			break;
		case 5:
			myOwner = ownerEnum.playerYellow;
			myState = stateEnum.Build;
			setValues();
			break;
		}
	}

	//INITIALIZE THINGS FOR SWAPING FROM BUY TO BUILD STATE
	public void BuildToRenovate()
	{
		//START PARTICLES
		instantiateParticles (new Vector3(transform.position.x, 0f, transform.position.z));
		//CHANGE STATE
		myState = stateEnum.Renovate;
		//SWAP COLLIDERS
		gameObject.GetComponent<BoxCollider>().enabled = true;
		childTerrain.GetComponent<BoxCollider>().enabled = false;
		childTerrain.SetActive(false);
		setValues();
	}

	//INITIALIZE THINGS FOR SWAPING FROM BUY TO BUILD STATE
	public void ResetEverything()
	{
		//OWNER ID IS BACK GAME
		ownerID = 0;
		//RESET VALUES
		setValues();
		//RESET OWNER & STATE
		myOwner = ownerEnum.game;
		myState = stateEnum.Buy;

		//SET BACK TO ORIGINAL POS
		transform.position = startPos;

		//INSTANTIATE CHILD SCRIPT
		if (myType == typeEnum.leisure) 
		{
			childTerrain = transform.Find ("BV_Mesh_terrain").gameObject;
			BV_Terrain terrainScript = childTerrain.GetComponent<BV_Terrain>();
			//CHANGE CHILD COLOR
			terrainScript.changeColor(0);

			if(PhotonNetwork.room != null)
			{
				terrainScript.sendRPC(0);
			}

			//SWAP COLLIDERS
			childTerrain.SetActive(true);
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}

	//INITIALIZE ALL VALUES
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

	//DEAL WITH AGING
	public void handleAge()
	{
		if (myState == stateEnum.Renovate) 
		{
			age++;
		}
		
		
		if (age == 10000 && myType == typeEnum.leisure && myState == stateEnum.Renovate) 
		{
			ResetEverything();
			setValues();
			childTerrain.GetComponent<BoxCollider>().enabled = true;
		} 
	}

	//ACTIVATE PARTICLES
	public void instantiateParticles(Vector3 pos)
	{
		Instantiate (particles, pos,Quaternion.identity);
	}

	//FOR MAP EDITOR ########################################################################################
	public void spawnBuilding()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit ();
		
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.gameObject.tag == "MAP" && allowToMove == true) {
				//print (hit.transform.gameObject.tag+" is hit at "+hit.point);
				transform.position = hit.point;
				
				if (Input.GetMouseButtonDown (1)) {
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
	}

	//TAKES CARE OF RPC MESSAGING ########################################################################################
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
		BuyToBuild(i);
	}
	
	public void sendRPCstate()
	{
		this.photonView.RPC("rpcState", PhotonTargets.All);
	}
	
	[PunRPC]
	void rpcState()
	{
		Debug.Log ("TEST");
		BuildToRenovate ();
	}
	//START ########################################################################################
	void Start()
	{
		//RESET ALL VALUES
		startPos = transform.position;
		ResetEverything ();
	}

	//Update ########################################################################################
	void Update()
	{
		if (myState == stateEnum.Renovate) 
		{
			//RISING ANIMATION
			animPosition = new Vector3 (transform.position.x, 0f, transform.position.z);
			transform.position = Vector3.MoveTowards(transform.position, animPosition, 10*Time.deltaTime);
		}

		handleAge ();
	}
}
