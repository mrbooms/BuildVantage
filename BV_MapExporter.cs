using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BV_MapExporter : MonoBehaviour 
{
	//HOW MANY ASSETS IN THE DATABASE
	private int DBsize;

	//CREATE THE BUTN
	private Button button;

	//VARIABLES OF THE DATABASE IN THE REST WB
	private string buildingName;
	private float buildingPosX;
	private float buildingPosY;
	private float buildingPosZ;
	private float buildingRotX;
	private float buildingRotY;
	private float buildingRotZ;
	private float buildingRotW;

	// Use this for initialization
	void Start () 
	{
		//ONLY FOR TESTING PURPOSES
		/*
		buildingName = "test";
		buildingPosX = 1f;
		buildingPosY = 2f;
		buildingPosZ = 3f;
		buildingRotX = 4f;
		buildingRotY = 5f;
		buildingRotZ = 6f;
		buildingRotW = 7f;*/

		//ADD LISTENER TO THE STATE BUTTON
		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (() => {
			clickBtn ();});
	
	}


	public void clickBtn()
	{
		StartCoroutine (DeleteAll());

		GameObject[] buildings = GameObject.FindGameObjectsWithTag ("BUILDING");

		foreach (GameObject building in buildings) 
		{
			buildingName = building.name;
			buildingPosX = truncate(building.transform.position.x, 4);
			buildingPosY = truncate(building.transform.position.y, 4);
			buildingPosZ = truncate(building.transform.position.z, 4);
			buildingRotX = truncate(building.transform.rotation.x, 4);
			buildingRotY = truncate(building.transform.rotation.y, 4);
			buildingRotZ = truncate(building.transform.rotation.z, 4);
			buildingRotW = truncate(building.transform.rotation.w, 4);

			StartCoroutine (Post());
		}

	}

	public static float truncate(float value, int digits)
	{
		double mult = Math.Pow (10.0, digits);
		double result = Math.Truncate (mult * value) / mult;
		return(float)result;
	}

	IEnumerator Post()
	{
		//CONNECTS TO WEBSERVICE
		string url = "http://localhost:8080/AssetService/resources/assets/create";
		WWWForm form = new WWWForm ();
		form.AddField ("name", buildingName);
		form.AddField ("posX", buildingPosX+"");
		form.AddField ("posY", buildingPosY+"");
		form.AddField ("posZ", buildingPosZ+"");
		form.AddField ("rotX", buildingRotX+"");
		form.AddField ("rotY", buildingRotY+"");
		form.AddField ("rotZ", buildingRotZ+"");
		form.AddField ("rotW", buildingRotW+"");
		WWW www = new WWW (url, form);

		print ("POST : POSTING DATA TO DATABASE...");
		
		yield return www;
		
		if (www.error == null) 
		{
			print ("POST : DATA SUCCESSFULY POSTED");
			
		} 
		else 
		{
			print ("POST : FAILED POSTING DATA");
			print ("POST : WWW ERROR : "+www.error);
		}
		
	}

	IEnumerator DeleteAll()
	{
		WWW w = new WWW("http://localhost:8080/AssetService/resources/assets/delete/all");
		yield return w;

	}

}
