using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEngine.UI;
using System.Collections.Generic;

public class BV_MainMenuScript : MonoBehaviour 
{
    //FOR LATER ENABLING THE ROOM MENU & SCRIPT
	private GameObject roomMenu;
	private BV_RoomMenu roomMenuScript;

	//TEST
	public string test;

	//FIELDS VARIABLES
	private string username;
	private string password;
	private string[][] profiles;

	void Start()
	{
		roomMenuScript = GameObject.Find("GameScripts").GetComponent<BV_RoomMenu> ();
		roomMenuScript.enabled = false;
		roomMenu = GameObject.Find ("RoomMenu");
		//roomMenu.SetActive (false);

		//INITIALIZE PROFILES[][] AT START BY GETTING ALL THE DATA OF DB
		string url = "http://localhost:8080/ProfileService_v2/resources/profiles/raw";
		WWW www = new WWW(url);
		StartCoroutine(Get(www));
	}

	void OnGUI()
	{
/*
		string testPlayBtn = GameObject.Find ("PlayBtn").GetComponent<Image> ().enabled + "";
		string testPlayBtn02 = GameObject.Find("PlayBtn").GetComponent<Button>().enabled + "";

		GUILayout.Label("TEST = "+test
		                +"\n"+"MAIN MENU BTN"
		                +"\n"+"PLAY BTN ENABLEd ? = " + testPlayBtn
		                +"\n"+"PLAY IMAGE ENABLED ? = "+ testPlayBtn02);*/
	}

	public void loginBtn()
	{
		//RETRIEVE DATA FROM GUI
		username = GameObject.Find ("LoginUsername").GetComponent<InputField> ().text;
		password = GameObject.Find ("LoginPassword").GetComponent<InputField> ().text;

		//COMPARE DATA FROM GUI AND FROM PROFILES[][] (DATABASE)
		if (checkIfExists () == true) 
		{
			test = ("GET : SUCCESSFULLY LOGGED IN !");
			print("GET : SUCCESSFULLY LOGGED IN !");
			switchMenus();
		}
		else if (checkIfExists () == false) 
		{
			test = ("GET : FAILED LOGGED IN !");
			print("GET : FAILED LOGGED IN !");
		}

		/*
		print ("TEST LOGIN ##########");
		print ("USERNAME = "+username);
		print ("PASSWORD = "+password);*/
	}

	public void createProfile()
	{
		//RETRIEVE DATA FROM GUI
		username = GameObject.Find ("CreateUsername").GetComponent<InputField> ().text;
		password = GameObject.Find ("CreatePassword").GetComponent<InputField> ().text;

		//CHECK IF THIS DATA EXISTS ALREADY IN THE PROFILES[][] 
		if (checkIfExists () == true) 
		{
			test = ("POST : PROFILE EXISTS ALREADY !");
			print("POST : PROFILE EXISTS ALREADY !");
			return;
		}

		test = ("POST : PROFILE DOES NOT EXISTS");
		print ("POST : PROFILE DOES NOT EXISTS");


		//THIS PART POST A NEW PROFILE FOR POST NEW PROFILE IF PROFILE DOES NOT EXISTS
		string url = "http://localhost:8080/ProfileService_v2/resources/profiles/create";
		WWWForm form = new WWWForm ();
		form.AddField ("username", username);
		form.AddField ("password", password);
		form.AddField ("email", "JohnDoe.com");
		form.AddField ("registrationDate", "5/5/2015");
		WWW www = new WWW (url, form);

		//UPDATE PROFILES[][]
		test = ("POST : PROFILES LENGTH = " + profiles.Length);
		print ("POST : PROFILES LENGTH = " + profiles.Length);
		string[] newProfile = new string[]{"0",username, password, "",""};
		profiles.SetValue (newProfile, profiles.Length - 1);
		printJaggedArray (profiles);

		//START COROUTINE
		StartCoroutine (Post (www));

		//EXIT LOGIN MENU
		switchMenus ();

		//UPDATE PLAYER PREFS
		//

		/*
		print ("TEST CREATE PROFILE ##########");
		print ("USERNAME = "+username);
		print ("PASSWORD = "+password);*/
	}

	public void starGame()
	{
		//
		roomMenu.SetActive (true);
		roomMenuScript.enabled = true;
		//
	}

	public void mapEditor()
	{
		Application.LoadLevel ("MapEditor");
	}

	public void quitGame()
	{
		Application.Quit();
	}

	//THIS CONNECTS TO WEBSERVICE TO GET ALL PROFILES FROM DB
	IEnumerator Get(WWW www)
	{
		test = ("GET : GETTING DATA FROM DATABASE...");
		//print ("GET : GETTING DATA FROM DATABASE...");

		yield return www;

		if (www.error == null) 
		{
		    profiles = toArray (www.text);
			test = ("GET : DATA SUCCESSFULLY GOT IN PROFILES[][]");
			//print ("GET : DATA SUCCESSFULLY GOT IN PROFILES[][]");
		} 
		else 
		{
			test = ("GET : FAILED GETTING DATA");
			test = ("GET : WWW ERROR : "+www.error);
			//print ("GET : FAILED GETTING DATA");
			//print ("GET : WWW ERROR : "+www.error);
		}
	}

	//THIS CONNECTS TO WEBSERVICE TO POST A NEW PROFILE IN DATABASE
	IEnumerator Post(WWW www)
	{
		test = ("POST : POSTING DATA TO DATABASE...");
		//print ("POST : POSTING DATA TO DATABASE...");

		yield return www;

		if (www.error == null) 
		{
			test = ("POST : DATA SUCCESSFULY POSTED");
			//print ("POST : DATA SUCCESSFULY POSTED");
			
		} 
		else 
		{
			test = ("POST : FAILED POSTING DATA");
			test = ("POST : WWW ERROR : "+www.error);
			//print ("POST : FAILED POSTING DATA");
			//print ("POST : WWW ERROR : "+www.error);
		}

	}

	//THIS CONVERTS THE RAW STRING INTO A STRING[][]
	public string[][] toArray (string s)
	{
		string[] temp = s.Split('#');
		profiles = new string[temp.Length-1][];

		for(int i=1; i < temp.Length; i++)
		{
			//CUT THE RAW STRING INTO EACH PROFILES
			string temp2 = temp.GetValue(i) as string;
			//CUT THE PROFILES INTO EACH VALUES
			string[] profile = temp2.Split(',');
			//ADD VALUES TO AN ARRAY OF ARRAYS
			profiles.SetValue(profile, i-1);
		}

		return profiles;
	}

	//THIS CHECK IF THE USERNAME AND PASSWORD EXIST IN THE DATABASE
	public bool checkIfExists ()
	{
		if (profiles != null) 
		{
			foreach (string[] profile in profiles) 
			{
				if(username == (profile.GetValue(1) as string) && password == (profile.GetValue(2) as string))
				{
					return true;
				}
			}
		}

		return false;
	}

	public void printJaggedArray(string[][] s)
	{
		if (s != null) {
			print ("TEST");
			
			foreach (string[] item in s) {
				//print (" ");
				//print ("username = " + item.GetValue (1));
				//print ("password = " + item.GetValue (2));
			}
		} else {
			test = ("PROFILES IS NULL !");
			//print ("PROFILES IS NULL !");
		}
	}

	public void switchMenus()
	{

		test = "YOU HAVE ENTERED THE SWITCH MENU METHOD";

		//ENABLE MAIN MENU
		GameObject.Find ("PlayBtn").GetComponent<Image> ().enabled = true;
		GameObject.Find ("PlayBtn").GetComponent<Button> ().enabled = true;
		GameObject.Find ("MapEditorBtn").GetComponent<Image> ().enabled = true;
		GameObject.Find ("MapEditorBtn").GetComponent<Button> ().enabled = true;
		GameObject.Find ("QuitBtn").GetComponent<Image> ().enabled = true;
		GameObject.Find ("QuitBtn").GetComponent<Button> ().enabled = true;

		//KILL LOGIN MENU
		GameObject.Find ("LoginMenu").SetActive (false);
		GameObject.Find ("LoginUsername").SetActive (false);
		GameObject.Find ("LoginPassword").SetActive (false);
		GameObject.Find ("CreateUsername").SetActive (false);
		GameObject.Find ("CreatePassword").SetActive (false);
		GameObject.Find ("LoginBtn").SetActive (false);
		GameObject.Find ("CreateProfileBtn").SetActive (false);


	}
}
