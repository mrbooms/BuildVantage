using UnityEngine;
using System.Collections;

public class BV_GameGuiManager : MonoBehaviour {


	private Ray ray;
	private RaycastHit hit;
	//[SerializeField] private BV_GameData gameData;
	[SerializeField] private GameObject buildingGui;

	public void setBuildingGui(GameObject buildingGui)
	{
		this.buildingGui = buildingGui;
	}

	public enum typeEnum
	{
		leisure,
		terrain
	}
	public typeEnum type;

	void Start()
	{
		hit = new RaycastHit ();
		//gameData = GameObject.Find ("GameScripts").GetComponent<BV_GameData> ();
		setBuildingGui (GameObject.Find ("BuildingPannel"));
		buildingGui.SetActive(false);
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown (0) && PhotonNetwork.room != null) 
		{
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if(Physics.Raycast (ray, out hit))
			{
				buildingGui.SetActive(true);
				//buildingGui.SetActive(true);
				BV_BuildingGui buildingGuiScript = buildingGui.GetComponent<BV_BuildingGui>();

				type = returnType(hit.transform.gameObject);
				swapCollider(false);

				switch(type)
				{
					case typeEnum.terrain:
					{
					print ("SELECTED A TERRAIN");
					buildingGuiScript.setBuilding(hit.transform.parent.gameObject);
					buildingGuiScript.setValues();
					break;
					}
					case typeEnum.leisure:
					{
					print ("SELECTED A LEISURE");
					buildingGuiScript.setBuilding(hit.transform.gameObject);
					buildingGuiScript.setValues();
					break;
					}
				}
			}
		}
	}

	
	public void swapCollider(bool value)
	{
		GameObject[] assets = GameObject.FindGameObjectsWithTag ("CLICKABLE");
		
		foreach (GameObject asset in assets) 
		{
			asset.GetComponent<BoxCollider> ().enabled = value;
		}
	}

	public typeEnum returnType(GameObject obj)
	{
		typeEnum temp;
		temp = typeEnum.terrain;

		if(obj.transform.parent == null)
		{
			temp = typeEnum.leisure;
		}
		return temp;
	}

}
