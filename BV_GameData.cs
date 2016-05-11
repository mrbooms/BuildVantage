using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BV_GameData : MonoBehaviour 
{
	public GameObject currentOpenWindow;
	//[SerializeField] private GameObject windowChild;
	[SerializeField] private GameObject selectedObject;
	[SerializeField] private GameObject parentObject;

	//SETTERS
	public void setSelectedObject(GameObject selectedObject)
	{
		this.selectedObject = selectedObject;
	}
	
	public void setParentObject(GameObject parentObject)
	{
		this.parentObject = parentObject;
	}

	//GETTERS
	public GameObject getSelectedObject()
	{
		return selectedObject;
	}
	
	public GameObject getParentObject()
	{
		return parentObject;
	}

	/*
	Camera myCam;
	//Input myInput;
	public GameObject currentOpenWindow;


	[SerializeField] private Ray ray;
	[SerializeField] private RaycastHit hit;
	[SerializeField] private bool hasClicked;
	[SerializeField] private bool hasClosed;

	public enum context
	{
		clicking,
		closing
	}
	public context myContext;

	void OnJoinedRoom()
	{
		myCam = Camera.main;
	}

	void Start()
	{
		selectedObject = null;
		//hit = new RaycastHit ();

		//INSTANTIATE WINDOW
		if(GameObject.FindGameObjectsWithTag("BUILDINGUI").Length <= 3)
		{
			currentOpenWindow = GameObject.Find("GameUI");
			setWindowChild(currentOpenWindow.transform.Find("GameGuiPannel").gameObject);
			windowChild.GetComponent<RectTransform>().localPosition = new Vector3 (0f,
			                                                                                                             121f,
			                                                                                                             0f);
			windowChild.SetActive(false);
		}
	}
	
	void Update()
	{
		if (Input.GetMouseButtonDown (0) && PhotonNetwork.connectionStateDetailed == PeerState.Joined) 
		{
			ray = myCam.ScreenPointToRay (Input.mousePosition);

			if(Physics.Raycast (ray, out hit))
			{
				//print ("RAYCAST = "+hit.transform.gameObject.name);
				
				if(hit.transform.tag == "TERRAIN" || hit.transform.tag == "LEISURE")
				{
					setSelectedObject(hit.transform.gameObject);
					//print ("SELECTED OBJECT = "+selectedObject.name);



					if(hit.transform.tag == "TERRAIN")
					{
						setParentObject(hit.transform.parent.gameObject);
						//print ("SELECTED PARENT = "+parentObject.name);
					}

					//DISABLE OTHER TERRAIN FROM CLICKING
					swapCollider("TERRAIN", false);
					swapCollider("LEISURE", false);
					

					//INITIALIZE WINDOW
					spawnGuiValues (hit);

					//READY TO SPAWN WINDOW
					windowChild.SetActive(true);
	
				}
			}
		}

		if (hasClicked == true) 
		{

			
			hasClicked = false;
			//print ("HIT ="+hit.point);
			//print ("HIT GAME OBJECT="+selectedObject.name);

				if(selectedObject.tag == "TERRAIN")
				{
					BV_Terrain terrainScript = selectedObject.GetComponent<BV_Terrain>();
					BV_Buiding buildingScript = parentObject.GetComponent<BV_Buiding>();
					
					if(buildingScript.myState == BV_Buiding.stateEnum.Buy)
					{
						buildingScript.setOwner(PhotonNetwork.player.ID);
						buildingScript.sendRPCowner(PhotonNetwork.player.ID);
						terrainScript.changeColor(PhotonNetwork.player.ID);
						terrainScript.sendRPC(PhotonNetwork.player.ID);

						print ("RPC ="+buildingScript.myOwner);
						print ("RPC ="+buildingScript.myState);
					}
					else if (buildingScript.myState == BV_Buiding.stateEnum.Build)
					{
						buildingScript.setState(3);
						buildingScript.sendRPCstate(3);
						buildingScript.instantiateParticles(new Vector3(selectedObject.transform.position.x,
						                                                selectedObject.transform.position.y+31f,
						                                                selectedObject.transform.position.z));
					}
				}
				else
				{
					BV_Buiding buildingScript = selectedObject.GetComponent<BV_Buiding>();
			}

				swapCollider("TERRAIN", true);
				swapCollider("LEISURE", true);
				
			windowChild.SetActive(false);
		}
	}

	public void spawnGuiValues(RaycastHit hitInfo)
	{
		
		//print ("PROCESSING : "+hitInfo.transform.gameObject.name);
		GameObject children = currentOpenWindow.transform.Find ("GameGuiPannel").gameObject;
		
		foreach(Transform child in children.transform)
		{
			//print ("######### NEW PROCESS ########");
			//print ("PROCESSING : "+child.name);

			//GET SCRIPT FROM SELECTED TERRAIN
			BV_Buiding buildingScript = getScript(selectedObject);

			//CHANGE THUMBNAIL DEPENDING OF THE BUILDING TYPE
			if(child.name == "RawImage")
			{
				//print ("SELECTED OBJECT = "+selectedObject);

				if(selectedObject.tag == "TERRAIN")
				{
					Texture NewTexture = Resources.Load<Texture>("Thumbnails/" + parentObject.name + "_t");
					child.GetComponent<RawImage>().texture = NewTexture;
					//print ("SUCCESSFULLY LOADED TEXTURE : "+parentObject.name);
				}
				else
				{
					Texture NewTexture = Resources.Load<Texture>("Thumbnails/" + selectedObject.name + "_t");
					child.GetComponent<RawImage>().texture = NewTexture;
					//print ("SUCCESSFULLY LOADED TEXTURE : "+selectedObject.name);
				}
			}

			if(child.name == "StateBtn")
			{
				//print ("STATE = "+buildingScript.myState);
				Button stateBtn = child.GetComponent<Button>();
				string s = buildingScript.myState+"";
				Text newtext = child.transform.Find("StateText").GetComponent<Text>();
				newtext.text = s;
				//stateBtn.onClick.AddListener(() => {clickStateBtn();});
			}
			if(child.name == "PopValue")
			{
				float value = buildingScript.getPopularity();
				string s = buildingScript.getPopularity()+"";
				//print ("POPULARITY = "+s);
				Text newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "InfValue")
			{
				float value = buildingScript.getInfluence();
				string s = buildingScript.getInfluence()+"";
				//print ("INFLUENCE = "+s);
				Text newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "IncValue")
			{
				float value = buildingScript.getIncome();
				string s = buildingScript.getIncome()+"";
				//print ("INCOME = "+s);
				Text newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "ExpValue")
			{
				float value = buildingScript.getExpenses();
				string s = buildingScript.getExpenses()+"";
				//print ("EXPENSES = "+s);
				Text newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "TotValue")
			{
				float value = buildingScript.getTotal();
				string s = buildingScript.getTotal()+"";
				//print ("TOTAL = "+s);
				Text newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "Toggle")
			{
				child.GetComponent<Toggle>().isOn = buildingScript.getPayTaxes();
			}
		}
	}

	public BV_Buiding getScript(GameObject obj)
	{

		//print ("ARE YOU BUILDING A SCRIPT =?");
		//print ("TAG =?"+obj.tag);
		
		if (obj.tag != "LEISURE") 
		{
			BV_Buiding parentScript = obj.transform.parent.GetComponent<BV_Buiding> ();
			return parentScript;
		} 
		else if (obj.tag == "LEISURE") 
		{
			//print ("LEISURE BUILDING");
			BV_Buiding buildingScript = obj.GetComponent<BV_Buiding>();
			//print ("TYPE IS : "+buildingScript.myType);
			return buildingScript;
		}
		return null;
	}
	
	public void setHasClicked(bool b)
	{
		hasClicked = b;
	}



	public void setWindowChild(GameObject b)
	{
		windowChild = b;
	}



	public Color fontColorChanger(float value)
	{
		Color result = Color.yellow;
		
		if (value < 0) {result = Color.red;}
		else if (value == 0) {result = Color.yellow;}
		else if (value > 0) {result = Color.green;}
		
		return result;
	}

	public GameObject getSelected()
	{
		return selectedObject;
	}

	public GameObject getParent()
	{
		return parentObject;
	}

	public GameObject getWindowChild()
	{
		return windowChild;
	}*/
}
