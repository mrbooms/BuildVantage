using UnityEngine;
using System.Collections;

public class BV_BuildingShaderer : MonoBehaviour {

	//SHADER VARIABLES
	Material myMaterial;
	//FOR CHECKING IF OWNER HAS CHaNgeD
	string lastOwner;

	void Start()
	{
		//GETS THE NAME OF THE PREFAB AND THEN THE BUILDING BEHAVIOUR SCRIPT
		BV_BuildingManager myScript = gameObject.GetComponent<BV_BuildingManager> ();

		//INITIALIZE LAST OWNER
		lastOwner = myScript.myOwner + "";

		//CREATE A MATERIAL AND A SHADER
		myMaterial = new Material(Shader.Find("Standard (Specular setup)"));

		//MAKE THE TRANSPARENT PARTS (ALPHA CHANNEL) OF THE .PNG _d ALSO TRANSPARENT IN UNITY
		presetAlphaChannel ();

		//LOAD THE MATERIAL INTO THE GAME OBJECT
		gameObject.GetComponent<Renderer> ().material = myMaterial;
		
		//LOAD TEXTURE FILE IF EXISTS
		loadTexture ();

	}


	void Update()
	{
		//CREATE THE SCRIPT INSTANCE TO RETRIEVE ITS VALUES
		BV_BuildingManager myScript = gameObject.GetComponent<BV_BuildingManager> ();

		//ALOOW US TO MODIFY THE EMISSION PART OF THE SHADER
		gameObject.GetComponent<Renderer> ().material.EnableKeyword("_EMISSION");

		if (lastOwner != myScript.myOwner + "" && myScript.myType == BV_BuildingManager.typeEnum.leisure) 
		{
			print ("####### COLOR CHANGED !");
			//updateColor(myScript);
			lastOwner = myScript.myOwner + "";
		}
	}


	void updateColor(BV_BuildingManager myScript)
	{
		//CHANGE TO THE CORRECT COLOR DEPENDING OF THE GAME OWNER
		if (myScript.myOwner == BV_BuildingManager.ownerEnum.game ) 
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.clear);
		}
		else if (myScript.myOwner == BV_BuildingManager.ownerEnum.playerBlue)
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.blue);
		}
		else if (myScript.myOwner == BV_BuildingManager.ownerEnum.playerGreen )
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.green);
		}
		else if (myScript.myOwner == BV_BuildingManager.ownerEnum.playerOrange)
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.magenta);
		}
		else if (myScript.myOwner == BV_BuildingManager.ownerEnum.playerRed)
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.red);
		}
		else if (myScript.myOwner == BV_BuildingManager.ownerEnum.playerYellow) 
		{
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", Color.yellow);
		}
	}

	public void loadTexture()
	{
		if (Resources.Load ("Import/Textures/" + name + "_d") as Texture) 
		{
			Texture _d = Resources.Load ("Import/Textures/" + name + "_d") as Texture;
			gameObject.GetComponent<Renderer> ().material.mainTexture = _d;
		} 
		else if (Resources.Load ("Import/Textures/" + name + "_d") as Texture != true)
		{
			print ("CANNOT FIND ALBEDO MAP FOR "+name);
		}
		if (Resources.Load ("Import/Textures/" + name + "_s") as Texture) 
		{
			Texture _s = Resources.Load ("Import/Textures/" + name + "_s") as Texture;
			gameObject.GetComponent<Renderer> ().material.EnableKeyword("_SPECGLOSSMAP");
			gameObject.GetComponent<Renderer> ().material.SetTexture("_SpecGlossMap", _s);
		} 
		else if (Resources.Load ("Import/Textures/" + name + "_s") as Texture != true)
		{
			print ("CANNOT FIND SPECULAR MAP");
		}
		if (Resources.Load ("Import/Textures/" + name + "_n") as Texture) 
		{
			Texture _n = Resources.Load ("Import/Textures/" + name + "_n") as Texture;
			gameObject.GetComponent<Renderer> ().material.EnableKeyword("_NORMALMAP");
			gameObject.GetComponent<Renderer> ().material.SetTexture("_BumpMap", _n);
			gameObject.GetComponent<Renderer> ().material.SetFloat("_BumpScale", 0.2f);
		} 
		else if (Resources.Load ("Import/Textures/" + name + "_n") as Texture != true)
		{
			print ("CANNOT FIND NORMAL MAP");
		}
		if (Resources.Load ("Import/Textures/" + name + "_c") as Texture) 
		{
			Texture _c = Resources.Load ("Import/Textures/" + name + "_c") as Texture;
			gameObject.GetComponent<Renderer> ().material.EnableKeyword("_EMISSION");
			gameObject.GetComponent<Renderer> ().material.SetTexture("_EmissionMap", _c);
			Color transparent = new Color(0.001f,0.001f,0.010f,0.001f);
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", transparent);
		} 
		else if (Resources.Load ("Import/Textures/" + name + "_c") as Texture != true)
		{
			print ("CANNOT FIND EMISSION (COLOR) MAP");
			gameObject.GetComponent<Renderer> ().material.EnableKeyword("_EMISSION");
			Color transparent = new Color(0.010f,0.001f,0.001f,0.001f);
			gameObject.GetComponent<Renderer> ().material.SetColor("_EmissionColor", transparent);
		}
	}

	public void loadTextureEditor()
	{
		if (Resources.Load ("Import/Textures/" + name + "_d") as Texture) 
		{
			Texture _d = Resources.Load ("Import/Textures/" + name + "_d") as Texture;
			gameObject.GetComponent<Renderer> ().sharedMaterial.mainTexture = _d;
		} 
		else if (Resources.Load ("Import/Textures/" + name + "_d") as Texture != true)
		{
			print ("CANNOT FIND ALBEDO MAP");
		}
		if (Resources.Load ("Import/Textures/" + name + "_s") as Texture) 
		{
			Texture _s = Resources.Load ("Import/Textures/" + name + "_s") as Texture;
			gameObject.GetComponent<Renderer> ().sharedMaterial.EnableKeyword("_SPECGLOSSMAP");
			gameObject.GetComponent<Renderer> ().sharedMaterial.SetTexture("_SpecGlossMap", _s);
		} 
		else if (Resources.Load ("Import/Textures/" + name + "_s") as Texture != true)
		{
			print ("CANNOT FIND SPECULAR MAP");
		}
		if (Resources.Load ("Import/Textures/" + name + "_n") as Texture) 
		{
			Texture _n = Resources.Load ("Import/Textures/" + name + "_n") as Texture;
			gameObject.GetComponent<Renderer> ().sharedMaterial.EnableKeyword("_NORMALMAP");
			gameObject.GetComponent<Renderer> ().sharedMaterial.SetTexture("_BumpMap", _n);
			gameObject.GetComponent<Renderer> ().sharedMaterial.SetFloat("_BumpScale", 0.2f);
		} 
		else if (Resources.Load ("Import/Textures/" + name + "_n") as Texture != true)
		{
			print ("CANNOT FIND NORMAL MAP");
		}
		if (Resources.Load ("Import/Textures/" + name + "_c") as Texture) 
		{
			Texture _c = Resources.Load ("Import/Textures/" + name + "_c") as Texture;
			gameObject.GetComponent<Renderer> ().sharedMaterial.EnableKeyword("_EMISSION");
			gameObject.GetComponent<Renderer> ().sharedMaterial.SetTexture("_EmissionMap", _c);
			Color transparent = new Color(0.001f,0.001f,0.010f,0.001f);
			gameObject.GetComponent<Renderer> ().sharedMaterial.SetColor("_EmissionColor", transparent);
		} 
		else if (Resources.Load ("Import/Textures/" + name + "_c") as Texture != true)
		{
			print ("CANNOT FIND EMISSION (COLOR) MAP");
			gameObject.GetComponent<Renderer> ().sharedMaterial.EnableKeyword("_EMISSION");
			Color transparent = new Color(0.010f,0.001f,0.001f,0.001f);
			gameObject.GetComponent<Renderer> ().sharedMaterial.SetColor("_EmissionColor", transparent);
		}
	}

	void presetAlphaChannel()
	{
		myMaterial.SetFloat("_Mode", 1);
		myMaterial.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		myMaterial.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
		myMaterial.SetInt ("_ZWrite", 1);
		myMaterial.EnableKeyword ("_ALPHATEST_ON");
		myMaterial.DisableKeyword ("_ALPHABLEND_ON");
		myMaterial.DisableKeyword ("_ALPHAPREMULTIPLY");
		myMaterial.renderQueue = 2450;
	}
}
