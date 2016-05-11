using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BV_clickBtn : MonoBehaviour {

	Button button = null;
	string tag;

	void Start()
	{

		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (() => {
			Click ();});
		tag = transform.tag;
	}

	void Click()
	{
		print ("CLICKED FROM CLICKBTN");
		BV_GameData gameData = GameObject.Find ("GameScripts").GetComponent<BV_GameData> ();
		gameData.myContext = BV_GameData.context.closing;
		gameData.setHasClicked (true);
	}
}
