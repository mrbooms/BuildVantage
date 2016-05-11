using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BV_closeBtn : MonoBehaviour {

	Button button = null;
	
	void Start()
	{
		
		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (() => {
			Click ();});
	}
	
	void Click()
	{
		BV_GameData myGameData = GameObject.Find ("GameScripts").GetComponent<BV_GameData> ();
		//THIS IS FOR TESTING
		myGameData.swapCollider("TERRAIN", true);
		myGameData.swapCollider("LEISURE", true);
		myGameData.getWindowChild().SetActive(false);
		//
		myGameData.myContext = BV_GameData.context.closing;
		//myGameData.setHasClicked (true);
	}
}
