using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BV_GameClicks : MonoBehaviour {/*

	public GameObject window;
	RaycastHit hitInfo;
	//float x = 0;
	private int click = 0;

	void Start()
	{
		//x = window.GetComponent<RectTransform>().localPosition.x - 300f;
	}

	// Update is called once per frame
	void Update () 
	{


		//&& hitInfo.transform.tag == "TERRAIN") 

		/*
		print (click);

		hitInfo = new RaycastHit();

		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)
		    && Input.GetMouseButtonDown (0)
		    && hitInfo.transform.tag == "TERRAIN") 
		{
			GameObject[] terrains = GameObject.FindGameObjectsWithTag("TERRAIN");

			foreach(GameObject terrain in terrains)
			{
				terrain.GetComponent<BoxCollider>().enabled = false;
			}

			//THIS PART TAKES CARE OF INSTANTIATING THE BUILDING WINDOW
			spawnBuildingGUI ();
			
			//THIS INITIALIZE BUILDING GUI VALUES
			spawnGuiValues (hitInfo);
		} 
	}
	
	public void spawnGuiValues(RaycastHit hitInfo)
	{

		print ("PROCESSING : "+hitInfo.transform.gameObject.name);
		GameObject children = window.transform.Find ("GameGuiPannel").gameObject;

		foreach(Transform child in children.transform)
		{
			print ("######### NEW PROCESS ########");
			print ("PROCESSING : "+child.name);

			//CHANGE THUMBNAIL DEPENDING OF THE BUILDING TYPE
			if(child.name == "RawImage")
			{
				Texture NewTexture = Resources.Load<Texture>("Thumbnails/" + hitInfo.transform.parent.name + "_t");
				child.GetComponent<RawImage>().texture = NewTexture;
				print ("SUCCESSFULLY LOADED TEXTURE : "+hitInfo.transform.parent.name);
			}

			//GET SCRIPT FROM SELECTED TERRAIN
			BV_Buiding buildingScript = hitInfo.transform.parent.GetComponent<BV_Buiding>();

			if(child.name == "StateBtn")
			{
				print ("STATE = "+buildingScript.myState);
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
				print ("POPULARITY = "+s);
				Text newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "InfValue")
			{
				float value = buildingScript.getInfluence();
				string s = buildingScript.getInfluence()+"";
				print ("INFLUENCE = "+s);
				Text newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "IncValue")
			{
				float value = buildingScript.getIncome();
				string s = buildingScript.getIncome()+"";
				print ("INCOME = "+s);
				Text newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "ExpValue")
			{
				float value = buildingScript.getExpenses();
				string s = buildingScript.getExpenses()+"";
				print ("EXPENSES = "+s);
				Text newtext = child.GetComponent<Text>();
				newtext.text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "TotValue")
			{
				float value = buildingScript.getTotal();
				string s = buildingScript.getTotal()+"";
				print ("TOTAL = "+s);
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
	


	void spawnBuildingGUI()
	{
		if(GameObject.FindGameObjectsWithTag("BUILDINGUI").Length <= 3)
		{
			Instantiate(window);
			
			window.transform.Find("GameGuiPannel").GetComponent<RectTransform>().localPosition = new Vector3 (0f,
			                                                                                                  121f,
			                                                                                                  0f);
			//x = x + 200;
		}
	}

	public void StateBtn()
	{
		print ("CLICK STATE BTN");
		click = 111;
	}

*/
}
