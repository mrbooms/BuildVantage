using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BV_ThumnailsSpawner : MonoBehaviour 
{

	Button button;
	[SerializeField] private GameObject building;
	bool spawned = false;

	//SETTERS
	public void setBuilding(GameObject building)
	{
		this.building = building;
	}

	// Use this for initialization
	void Start () 
	{
		//ADD PICTURE
		GetComponent<Image> ().overrideSprite = Resources.Load<Sprite> ("Thumbnails/" + name + "_t");
		//ADD LISTENER TO THE STATE BUTTON
		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (() => {
			clickThumbnail ();});
	}

	public void clickThumbnail()
	{
		building = Resources.Load ("Prefabs/Buildings/" + name) as GameObject;
		GameObject newBuilding = (GameObject)Instantiate (building, Vector3.zero, Quaternion.identity);
		newBuilding.name = name;
		BV_BuildingManager newBuildingScript = newBuilding.GetComponent<BV_BuildingManager> ();
		newBuildingScript.allowToMove = true;
		building = newBuilding;
	}
}
