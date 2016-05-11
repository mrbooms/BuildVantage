using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BV_ThumnailsSpawner : MonoBehaviour 
{

	Button button;
	[SerializeField] private GameObject building;
	bool spawned = false;
	Vector3 MouseCoords;
	float MoveSpeed;
	BV_Buiding buildingScript;

	//SETTERS
	public void setBuilding(GameObject building)
	{
		this.building = building;
	}

	// Use this for initialization
	void Start () 
	{
		MoveSpeed = 3f;
		//setBuilding (Resources.Load ("Prefabs/Buildings/" + name)as GameObject);
		buildingScript = building.GetComponent<BV_Buiding> ();

		GetComponent<Image> ().overrideSprite = Resources.Load<Sprite> ("Thumbnails/" + building.name + "_t");

		//ADD LISTENER TO THE STATE BUTTON
		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (() => {
			clickThumbnail ();});
	}

	public void clickThumbnail()
	{
		print ("HAS CLICKED");
		Instantiate (building);
		building.name = gameObject.name;
		buildingScript.allowToMove = true;
	}



	void Update()
	{
		if (buildingScript.allowToMove == true)
		{
			print ("MOVING");
		}
	}
}
