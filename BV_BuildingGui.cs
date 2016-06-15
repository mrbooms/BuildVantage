using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class BV_BuildingGui : Photon.MonoBehaviour {

	//BUTTON FOR CHANGING STATES IN BUILDING
	public Button stateBtn;
	public Button closeBtn;

	//SELECTED GAME OBJECT
	[SerializeField] private GameObject building;

	BV_BuildingManager buildingScript;

	//SETTER
    
	public void setBuilding(GameObject building)
	{
		this.building = building;
	}

	//CLOSE BUTTON METHOD
	void clickCloseBtn()
	{
		transform.gameObject.SetActive (false);
	}

	//STATE BUTTON METHOD
	void clickStateBtn()
	{
		print (PhotonNetwork.player.ID);

		switch(stateBtn.transform.Find("StateText").GetComponent<Text>().text)
		{

		case "Buy":
		{
			print ("PLAYER ID = "+PhotonNetwork.player.ID);
			buildingScript.BuyToBuild(PhotonNetwork.player.ID);
			buildingScript.sendRPCowner(PhotonNetwork.player.ID);
			transform.gameObject.SetActive (false);
			break;
		}
		case "Build":
		{
			buildingScript.BuildToRenovate();
			buildingScript.sendRPCstate();
			transform.gameObject.SetActive (false);
			break;
		}
		case "Renovate":
		{
			transform.gameObject.SetActive (false);
			break;
		}
		}
	}

	//UPDATE GUI OF BUILDING WINDOW
	public void setValues()
	{
        //GET SCRIPT FROM SELECTED TERRAIN
        //
        //GameObject guimanager = GameObject.Find("GameScripts").GetComponent<BV_GameGuiManager>();
       // print("TST RAYCAST");
        //
		buildingScript = building.GetComponent<BV_BuildingManager>();
		string s;
		Text newtext;

		print ("_______________________________________________________________________");
		print ("BV_BuildingGui : Updating values with data from : "+building.name);
		print ("_______________________________________________________________________");

		foreach(Transform child in transform)
		{		
			switch(child.name)
			{
			case "RawImage":
			{
				Texture NewTexture = Resources.Load<Texture>("Thumbnails/" + building.name + "_t");
				child.GetComponent<RawImage>().texture = NewTexture;
				print ("BV_BuildingGui : succeeded changing thumbnail"+building.name);
				break;
			}
			case "StateBtn":
			{
				stateBtn = child.GetComponent<Button>();
				s = buildingScript.myState+"";
				newtext = child.transform.Find("StateText").GetComponent<Text>();
				newtext.text = s;
				print ("BV_BuildingGui : succeeded changing state btn text");
                        
				//ADD LISTENER TO THE STATE BUTTON
				stateBtn.onClick.AddListener (() => {
					clickStateBtn ();});
				break;
			}
			case "PopValue":
			{
				float value = buildingScript.getPopularity();
				s = buildingScript.getPopularity()+"";
				newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
				print ("BV_BuildingGui : succeeded changing popularity value");
				break;
			}
			case "InfValue":
			{
				float value = buildingScript.getInfluence();
				s = buildingScript.getInfluence()+"";
				newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
				print ("BV_BuildingGui : succeeded changing influence value");
				break;
			}
			case "IncValue":
			{
				float value = buildingScript.getIncome();
				s = buildingScript.getIncome()+"";
				newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
				print ("BV_BuildingGui : succeeded changing income value");
				break;
			}
			case "ExpValue":
			{
				float value = buildingScript.getExpenses();
				s = buildingScript.getExpenses()+"";
				newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
				print ("BV_BuildingGui : succeeded changing expenses value");
				break;
			}
			case "TotValue":
			{
				float value = buildingScript.getTotal();
				s = buildingScript.getTotal()+"";
				newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
				print ("BV_BuildingGui : succeeded changing total value");
				break;
			}
			case "Toggle":
			{
				child.GetComponent<Toggle>().isOn = buildingScript.getPayTaxes();
				print ("BV_BuildingGui : succeeded changing toggle value");
				child.GetComponent<Toggle>().onValueChanged.AddListener((value)=>{
					updateTaxeBool(value);
				});

				break;
			}
			case "Close":
			{
				closeBtn = child.GetComponent<Button>();
				//ADD LISTENER TO THE CLOSE BUTTON
				closeBtn.onClick.AddListener (() => {
					clickCloseBtn ();});
				break;
			}
			}
		}
	}

	public void updateTaxeBool (bool value)
	{
		buildingScript.setPayTaxes (value);
		buildingScript.setValues ();

	}
	//CHANGE FONT COLOR DEPENDING ON VALUE
	public Color fontColorChanger(float value)
	{
		Color result = Color.yellow;
		
		if (value < 0) {result = Color.red;}
		else if (value == 0) {result = Color.yellow;}
		else if (value > 0) {result = Color.green;}
		
		return result;
	}
}
