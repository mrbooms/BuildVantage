using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BV_GameClicks : MonoBehaviour {

	public GameObject window;
	float x;

	void Start()
	{
		x = window.GetComponent<RectTransform>().localPosition.x - 300f;
	}
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit hitInfo = new RaycastHit();

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "TERRAIN")
			{
				//THIS PART TAKES CARE OF INSTANTIATING THE BUILDING WINDOW
				spawnBuildingGUI();

				//THIS INITIALIZE BUILDING GUI VALUES
				spawnGuiValues(hitInfo);


			}
		}
	}

	public void spawnGuiValues(RaycastHit hitInfo)
	{
		print ("SELECTED ITEM IS : "+hitInfo.transform.parent.name);
		print ("CHILD 1 : "+window.transform.Find("GameGuiPannel").name);
		GameObject child1 = window.transform.Find("GameGuiPannel").gameObject;
		print ("CHILD 2 : "+child1.name);
		
		foreach(Transform child in child1.transform)
		{
			print ("PROCESSING : "+child.name);
			//CHANGE THUMBNAIL DEPENDING OF THE BUILDING TYPE
			if(child.name == "RawImage")
			{
				Texture NewTexture = Resources.Load<Texture>("Thumbnails/" + hitInfo.transform.parent.name + "_t");
				
				child.GetComponent<RawImage>().texture = NewTexture;
			}
			//GET THE SCRIPT OF THE SELECTED TERRAIN PARENT (BUILDING)
			BV_Buiding buildingScript = hitInfo.transform.parent.GetComponent<BV_Buiding>();
			
			if(child.name == "StateBtn")
			{
				child.transform.Find("StateText").GetComponent<Text>().text = buildingScript.myState+"";
			}
			if(child.name == "PopValue")
			{
				float value = buildingScript.getPopularity();
				string s = buildingScript.getPopularity()+"";
				child.GetComponent<Text>().text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "InfValue")
			{
				float value = buildingScript.getInfluence();
				string s = buildingScript.getInfluence()+"";
				child.GetComponent<Text>().text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "IncValue")
			{
				float value = buildingScript.getIncome();
				string s = buildingScript.getIncome()+"";
				child.GetComponent<Text>().text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "ExpValue")
			{
				float value = buildingScript.getExpenses();
				string s = buildingScript.getExpenses()+"";
				child.GetComponent<Text>().text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "TotValue")
			{
				float value = buildingScript.getTotal();
				string s = buildingScript.getTotal()+"";
				child.GetComponent<Text>().text = s;
				child.GetComponent<Text>().color = fontColorChanger(value);
			}
			if(child.name == "Toggle")
			{
				child.GetComponent<Toggle>().isOn = buildingScript.getPayTaxes();
			}
		}
		

	}

	public Color fontColorChanger(float value)
	{
		Color result = Color.yellow;

		if (value < 0) {result = Color.red;}
		else if (value == 0) {result = Color.yellow;}
		else if (value > 0) {result = Color.green;}

		return result;
	}

	void spawnBuildingGUI()
	{
		if(GameObject.FindGameObjectsWithTag("BUILDINGUI").Length <= 3)
		{
			Instantiate(window);
			
			window.transform.Find("GameGuiPannel").GetComponent<RectTransform>().localPosition = new Vector3 (x,
			                                                                                                  121f,
			                                                                                                  0f);
			x = x + 200;
		}
	}
}
