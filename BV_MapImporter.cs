using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BV_MapImporter : MonoBehaviour {

	private int DBsize;

	//VARIABLES OF THE DATABASE IN THE REST WB
	private string buildingName;
	private float buildingPosX;
	private float buildingPosY;
	private float buildingPosZ;
	private float buildingRotX;
	private float buildingRotY;
	private float buildingRotZ;
	private float buildingRotW;

	//CREATE THE BUTN
	private Button button;

	// Use this for initialization
	void Start () 
	{
		if (Application.loadedLevelName == "test01") {
			StartCoroutine (GetAll ());
		} else {
			//ADD LISTENER TO THE STATE BUTTON
			button = gameObject.GetComponent<Button> ();
			button.onClick.AddListener (() => {
				clickBtn ();});
		}
		
	}

	public void clickBtn()
	{
		StartCoroutine (GetAll());
	}

	IEnumerator GetAll()
	{
		WWW w = new WWW("http://localhost:8080/AssetService/resources/assets");
		
		yield return w;
		
		while (w.text == "")
		{
			yield return null;
		}
		
		//DELETE THE BRACKETS
		string result = w.text+"";
		string[] answer = result.Split('#');

		foreach (string item in answer) 
		{
			string[] values = item.Split ('/');

			//print ("VALUES #######");
			int i = 0;

			foreach(string value in values)
			{
				switch (i)
				{
				case 0 :
					//ID
					i++;
					break;
				case 1 :
					//NAME
					buildingName = value;
					//print ("SUCCEED LOAD NAME :"+buildingName);
					i++;
					break;
				case 2 :
					buildingPosX = float.Parse(value);
					//print ("SUCCEED LOAD POSX :"+buildingPosX);
					//posX
					i++;
					break;
				case 3 :
					//posY
					buildingPosY = float.Parse(value);
					//print ("SUCCEED LOAD POSY :"+buildingPosY);
					i++;
					break;
				case 4 :
					//posZ
					buildingPosZ = float.Parse(value);
					//print ("SUCCEED LOAD POSZ :"+buildingPosZ);
					i++;
					break;
				case 5 :
					//rotX
					buildingRotX = float.Parse(value);
					//print ("SUCCEED LOAD RotX :"+buildingRotX);
					i++;
					break;
				case 6 :
					//rotY
					buildingRotY = float.Parse(value);
					//print ("SUCCEED LOAD RotY :"+buildingRotY);
					i++;
					break;
				case 7 :
					//rotZ
					buildingRotZ = float.Parse(value);
					//print ("SUCCEED LOAD RotZ :"+buildingRotZ);
					i++;
					break;
				case 8 :
					//rotW
					buildingRotW = float.Parse(value);
					//print ("SUCCEED LOAD RotW :"+buildingRotW);
					i++;
					break;
				}
			}

			if(Resources.Load ("Prefabs/Buildings/" + buildingName))
			{
				Vector3 position = new Vector3(buildingPosX,buildingPosY,buildingPosZ);
				Quaternion rotation = new Quaternion(buildingRotX, buildingRotY, buildingRotZ, buildingRotW);
				GameObject building = Resources.Load ("Prefabs/Buildings/" + buildingName) as GameObject;
				GameObject newBuilding = (GameObject)Instantiate (building, position, rotation);
				newBuilding.name = buildingName;
				BV_BuildingManager newBuildingScript = newBuilding.GetComponent<BV_BuildingManager> ();
				newBuildingScript.allowToMove = false;
				building = newBuilding;
			}
			else
			{
				//print ("ERROR###############DIDNt FIND BUILDING PREFAB FOR "+buildingName);
			}
		}

		DBsize = answer.Length-1;
		//print ("DB SIZE = " + DBsize);
	}
}
