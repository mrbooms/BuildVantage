using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class BV_GameDataGui : MonoBehaviour 
{
	//PANEL
	public GameObject panel;

	//VALUES
	public List<GameObject> ownedBuilding = new List<GameObject>();
	public float stats_popularity;
	public float stats_influence;
	public float stats_income;
	public float stats_buildingCount;
	public float stats_taxesCount;
	public float stats_illegalCount;
	public float money_income;
	public float money_expenses;
	public float money_taxes;
	public float money_acount;
	public float money_total;
	public BV_BuildingManager buildingScript;

	public float dayCount;

	public enum stateEnum
	{
		nothing,
		influence,
		stats,
		money
	}
	public stateEnum state;

	// Use this for initialization
	void Start () 
	{
		panel = GameObject.Find ("GamePanel");
		money_acount = 10000;
	}
	void Update()
	{
		updateInfluence ();
		updateGUI ();

		dayCount++;
		if (dayCount >= 100) 
		{
			updateValues();
		}
	}

	public void updateInfluence()
	{
		foreach (GameObject building in ownedBuilding) 
		{
			BV_BuildingManager thisScript = building.GetComponent<BV_BuildingManager>();

			if(state == stateEnum.influence)
			{
				print ("INFLUENCE = "+thisScript.getInfluence());

				if(stats_influence >= 0 && stats_influence < 50)
				{
					building.GetComponent<Renderer> ().material.color = new Color32(255,255,255,255);
				}
				if(stats_influence >= 50 && stats_influence < 100)
				{
					building.GetComponent<Renderer> ().material.color = new Color32(255,200,255,255);
				}
				if(stats_influence >= 100 && stats_influence < 150)
				{
					building.GetComponent<Renderer> ().material.color = new Color32(255,150,255,255);
				}
				if(stats_influence >= 150 && stats_influence < 200)
				{
					building.GetComponent<Renderer> ().material.color = new Color32(255,50,255,255);
				}
				if(stats_influence >= 200)
				{
					building.GetComponent<Renderer> ().material.color = new Color32(255,0,255,255);
				}

			}
			else
			{
				building.GetComponent<Renderer> ().material.color = Color.white;
				BV_BuildingShaderer shaderer = building.GetComponent<BV_BuildingShaderer>();
				shaderer.updateColor();
			}
		}
	}

	public void close()
	{
		state = stateEnum.nothing;
		panel.SetActive (false);
	}

	public void updateGUI()
	{
		string s;
		Text newtext;

		foreach (Transform item in panel.transform) 
		{
			switch (item.name) 
			{
			case "Close":
				item.GetComponent<Button>().onClick.AddListener (() => {
					close ();});
				break;
			case "Icon":
				if(state == stateEnum.influence)
				{
					Texture NewTexture = Resources.Load<Texture>("Sprites/BV_GameGui_Influence");
					item.GetComponent<RawImage>().texture = NewTexture;
				}
				if(state == stateEnum.money)
				{
					Texture NewTexture = Resources.Load<Texture>("Sprites/BV_GameGui_Money");
					item.GetComponent<RawImage>().texture = NewTexture;
				}
				if(state == stateEnum.stats)
				{
					Texture NewTexture = Resources.Load<Texture>("Sprites/BV_GameGui_Stats");
					item.GetComponent<RawImage>().texture = NewTexture;
				}
				break;
			case "STATS_Stats":
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_pop_title":
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_pop_text":

				s = stats_popularity+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = fontColorChanger(stats_popularity);
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_inf_title":
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_inf_text":

				s = stats_influence+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = fontColorChanger(stats_influence);
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_inc_title":
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_inc_text":
				s = stats_income+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = fontColorChanger(stats_income);
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_count_title":
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_count_text":

				s = stats_buildingCount+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = fontColorChanger(stats_buildingCount);
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_taxes_title":
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_taxes_text":
				s = stats_taxesCount+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = Color.red;
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_illegal_title":
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "STATS_illegal_text":
				s = stats_illegalCount+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = Color.red;
				if(state == stateEnum.stats)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "MONEY_PROFITS":
				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "MONEY_income_title":
				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "MONEY_income_text":
				s = money_income+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = fontColorChanger(money_income);
				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "MONEY_expenses_title":

				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "MONEY_expense_text":
				s = money_expenses+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = Color.red;
				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "MONEY_taxes_title":
				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "MONEY_taxes_text":
				s = money_taxes+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = Color.red;
				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "MONEY_account_title":
				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "MONEY_acount_text":
				s = money_acount+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = fontColorChanger(money_acount);
				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
				break;
			case "MONEY_total_title":
				s = money_total+"";
				newtext = item.GetComponent<Text>();
				newtext.text = s;
				item.GetComponent<Text>().color = fontColorChanger(money_total);
				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
			break;
			case "MONEY_total_text":

				if(state == stateEnum.money)
				{
					item.GetComponent<Text>().enabled = true;
				}
				else
				{
					item.GetComponent<Text>().enabled = false;
				}
					break;
			}
		}
	}

	public void updateValues()
	{
		dayCount = 0;

		//CLEAN EVERYTHING
		ownedBuilding.Clear();
		ownedBuilding.Clear();
		stats_popularity = 0;
		stats_influence = 0;
		stats_income = 0;
		stats_buildingCount = 0;
		stats_taxesCount = 0;
		stats_illegalCount = 0;
		money_income = 0;
		money_expenses = 0;
		money_taxes = 0;
		money_total = 0;
		
		if(PhotonNetwork.room != null && PhotonNetwork.room.playerCount > 1)
		{
			GameObject[] buildings = GameObject.FindGameObjectsWithTag("BUILDING");
			
			foreach (GameObject building in buildings)
			{
				buildingScript = building.GetComponent<BV_BuildingManager>();
				print ("BUILDING ID ="+buildingScript.getOwnerID());
				
				if(buildingScript.getOwnerID() == PhotonNetwork.player.ID)
				{
					buildingScript.setValues();
					ownedBuilding.Add(building);
					//REFACTOR
					stats_popularity = stats_popularity+buildingScript.getPopularity();
					stats_influence = stats_influence+buildingScript.getInfluence();
					stats_income = stats_income+buildingScript.getIncome();
					stats_buildingCount = buildings.Length;
					if(buildingScript.getPayTaxes() == true)
					{
						stats_taxesCount++;
					}
					if(buildingScript.getPayTaxes() == false)
					{
						stats_illegalCount++;
					}
					money_income = stats_income;
					money_expenses = money_expenses + buildingScript.getExpenses();
					money_taxes = money_taxes + buildingScript.getTaxes();
				}
			}
			money_acount = money_acount;
			money_total = money_acount - money_expenses + money_income;
			stats_buildingCount = stats_buildingCount + ownedBuilding.Count();
			print ("YOU OWNN"+ownedBuilding.Count());
		}
	}
	//CHANGE FONT COLOR DEPENDING ON VALUE
	public Color fontColorChanger(float value)
	{
		Color result = Color.yellow;
		
		if (value < 0) {result = Color.red;}
		else if (value == 0) {result = Color.yellow;}
		else if (value > 0) {result = Color.green;}
		
		return result;
	}

}
