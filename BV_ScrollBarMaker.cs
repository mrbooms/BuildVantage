using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEngine.UI;

public class BV_ScrollBarMaker : MonoBehaviour {

	private Vector2 canvasSize;
	private Vector2 panelSize;
	private Vector2 buttonSize;

	void Start()
	{
		//INITIALIZE THUMBNAILS  IN A SORTED ARRAY
		GameObject[] thumbnails = GameObject.FindGameObjectsWithTag ("THUMBNAILS");
		thumbnails = GameObject.FindGameObjectsWithTag ("THUMBNAILS").OrderBy (go => go.name).ToArray ();


		//DECLARE CANVAS; PANEL; THUMBNAIL BUTTONS
		canvasSize = GameObject.Find ("EditorGui").GetComponent<RectTransform> ().sizeDelta;
		float canvasX = canvasSize.x;
		float canvasY = canvasSize.y;
		float panelX = canvasX * 3;
		float buttonX = panelX / 55;
		float buttonY = buttonX;
		float panelY = buttonY;
		panelSize = new Vector2 (panelX, panelY);
		buttonSize = new Vector2 (buttonX, buttonY);

		//TEST
		print ("CANVAS = "+canvasSize);
		print ("THUMBNAILS = "+panelSize);
		print ("BUTTON = "+buttonSize);

		//RESIZE PANEL & THUMBNAILS
		GameObject.Find ("Thumbnails").GetComponent<RectTransform> ().sizeDelta = panelSize;

		//GET SLIDER RECT VALUES
		//INIATIALIZE SLIDER
		GameObject.Find ("Slider").GetComponent<RectTransform> ().sizeDelta = new Vector2 (canvasX, panelY);
		//SAME FOR ALL SLIDER KIDS
		GameObject.Find ("Background").GetComponent<RectTransform> ().offsetMin = new Vector2 (0,20f);
		GameObject.Find ("Background").GetComponent<RectTransform> ().offsetMax = new Vector2 (0, -20f);
		GameObject.Find ("Fill Area").GetComponent<RectTransform> ().offsetMin = new Vector2 (0,20f);
		GameObject.Find ("Fill Area").GetComponent<RectTransform> ().offsetMax = new Vector2 (0, -20f);
		GameObject.Find ("Handle Slide Area").GetComponent<RectTransform> ().offsetMin = new Vector2 (10,20f);
		GameObject.Find ("Handle Slide Area").GetComponent<RectTransform> ().offsetMax = new Vector2 (-10,20f);

		//SET SLIDER BOUNDARIES
		GameObject.Find ("Slider").GetComponent<Slider> ().minValue = -(panelX-(panelX/3)-(panelX/3));
		GameObject.Find ("Slider").GetComponent<Slider> ().maxValue = (panelX-(panelX/3)-(panelX/3));

		//SET POSITIONS
		float height = buttonX / 2;

		GameObject.Find ("Thumbnails").GetComponent<RectTransform> ().anchoredPosition = new Vector3 (0,
		                                                                                      height);
		GameObject.Find ("Slider").GetComponent<RectTransform> ().anchoredPosition = new Vector3 (0,
		                                                                                  buttonX);
		//PLACE BUTTONS
		float positionX = -(panelX/2) + (buttonX/2);
		foreach (GameObject thumbnail in thumbnails) 
		{

			thumbnail.GetComponent<RectTransform> ().anchoredPosition = new Vector3(positionX,
			                                                                        0);
			positionX = positionX + buttonX;
		}
	}
}
