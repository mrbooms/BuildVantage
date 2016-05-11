using UnityEngine;
using System.Linq;
using System.Collections;

public class BV_ScrollBarMaker : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GameObject[] thumbnails = GameObject.FindGameObjectsWithTag ("THUMBNAILS");
		thumbnails = GameObject.FindGameObjectsWithTag ("THUMBNAILS").OrderBy (go => go.name).ToArray ();

		float i = -850;
		
		foreach (GameObject thumbnail in thumbnails) 
		{
			i = i+72;
			//if(thumbnail.name == name)
			//{
			print (i);
			thumbnail.transform.position = new Vector3(i,
			                                            35f,
			                                            0f);
			//	break;
			//}
		}
	}

}
