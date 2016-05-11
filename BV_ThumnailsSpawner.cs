using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BV_ThumnailsSpawner : MonoBehaviour {


	// Use this for initialization
	void Start () 
	{



		GetComponent<Image> ().overrideSprite = Resources.Load<Sprite> ("Thumbnails/" + gameObject.name + "_t");
	}

	public void onClicked()
	{
		GameObject newBuilding = Resources.Load("Corner Buildings/"+this.name)as GameObject;
		Instantiate (newBuilding);
	}



}
