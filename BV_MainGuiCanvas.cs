using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BV_MainGuiCanvas : Photon.MonoBehaviour {

	//PANEL
	GameObject MainGuiPanel;

	//GUI STATS PANNEL
	public GameObject MainDataPanel;
	public BV_GameDataGui panelScript;
	
	//BUTTONS
	public Button influenceBtn;
	public Button moneyBtn;
	public Button statsBTn;
	public Button quitBtn;
	public Button reloadBtn;

	//NOTIFICATIONS
	public Text notifications;
	
	
	
	// Use this for initialization
	void Start () 
	{
		MainGuiPanel = GameObject.Find ("MainGui_Panel");
		//print ("GAME PANEL =" + MainGuiPanel.name);
		MainDataPanel = GameObject.Find ("GUI_GameData").transform.Find ("GamePanel").gameObject;
		
		foreach (Transform btn in MainGuiPanel.transform) {
			switch (btn.name) 
			{
			case "MainGui_InfluenceIcn":
				influenceBtn = btn.GetComponent<Button> ();
				panelScript = MainDataPanel.transform.parent.GetComponent<BV_GameDataGui> ();
				influenceBtn.onClick.AddListener (() => {
					clickInfluence ();});
				break;
			case "MainGui_MoneyIcn":
				moneyBtn = btn.GetComponent<Button> ();
				panelScript = MainDataPanel.transform.parent.GetComponent<BV_GameDataGui> ();
				moneyBtn.onClick.AddListener (() => {
					clickMoney ();});
				break;
			case "MainGui_QuitIcn":
				quitBtn = btn.GetComponent<Button> ();
				quitBtn.onClick.AddListener (() => {
					clickQuit ();});
				break;
			case "MainGui_ReloadIcn":
				reloadBtn = btn.GetComponent<Button> ();
				reloadBtn.onClick.AddListener (() => {
					clickReload ();});
				break;
			case "MainGui_StatsIcn":
				statsBTn = btn.GetComponent<Button> ();
				panelScript = MainDataPanel.transform.parent.GetComponent<BV_GameDataGui> ();
				statsBTn.onClick.AddListener (() => {
					clickStats ();});
				break;
			case "Text":
				notifications = btn.GetComponent<Text>();
				notifications.text = " PLEASE WAIT UNTIL WE CONNECT YOU TO THE ROOM ";
			break;
			}
		}
		MainGuiPanel.SetActive (false);
		MainDataPanel.SetActive (false);
	}

	public void Update()
	{
		if (PhotonNetwork.room != null) 
		{
			MainGuiPanel.SetActive (true);
			notifications.text = "WELCOME IN ROOM "+PhotonNetwork.room;
		}
	}

	public void clickInfluence()
	{
		MainDataPanel.SetActive (false);
		MainDataPanel.SetActive (true);
		panelScript.state = BV_GameDataGui.stateEnum.influence;
	}
	public void clickMoney()
	{
		MainDataPanel.SetActive (false);
		MainDataPanel.SetActive (true);
		panelScript.state = BV_GameDataGui.stateEnum.money;
	}
	public void clickStats()
	{
		MainDataPanel.SetActive (false);
		MainDataPanel.SetActive (true);
		panelScript.state = BV_GameDataGui.stateEnum.stats;
	}
	public void clickReload()
	{
		Application.LoadLevel (0);
	}
	public void clickQuit()
	{
		Application.Quit();
	}
}
