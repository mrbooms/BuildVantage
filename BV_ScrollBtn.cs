using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BV_ScrollBtn : MonoBehaviour {


	public void MoveBar()
	{
		GameObject Thumbnails = GameObject.Find ("Thumbnails");

		float value = gameObject.GetComponent<Slider> ().value;

		Thumbnails.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (value,
		                                                                          Thumbnails.GetComponent<RectTransform> ().anchoredPosition.y);


	}
}
