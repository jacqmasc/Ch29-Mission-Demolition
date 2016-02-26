using UnityEngine;
using System.Collections;

public enum GameMode
{
	idle, playing, levelEnd
}


public class MissionDemolition : MonoBehaviour {

	static public MissionDemolition S;

	//fields that can be set in Unity
	public GameObject[] castles;
	public GUIText gtLevel;
	public GUIText gtScore;
	public Vector3 castlePos;

	public bool ____________________________;

	//dynamic fields
	public int level;
	public int levelMax;
	public int shotsTaken;
	public GameObject castle;
	public GameMode mode = GameMode.idle;
	public string showing = "Slingshot";

	void Start()
	{
		S = this;
		level = 0;
		levelMax = castles.Length;
		StartLevel();
	}

	void StartLevel()
	{
		//get rid of old castle
		if (castle != null) 
		{ Destroy(castle);}

		//get rid of projectiles
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Projectile");
		foreach (GameObject pTemp in gos) 
		{ Destroy(pTemp);}

		//Instantiate new castle
		castle = Instantiate (castles[level]) as GameObject;
		castle.transform.position = castlePos;
		shotsTaken = 0;

		//Reset camera
		SwitchView ("Both");
		ProjectileLine.S.Clear();

		//Reset Goal
		Goal.goalMet = false;

		ShowGT();

		mode = GameMode.playing;
	}

	void ShowGT()
	{
		//Show data in GUIText
		gtLevel.text = "Level: " + (level + 1) + "/" + levelMax;
		gtScore.text = "Shots Taken: " + shotsTaken;
	}

	void Update()
	{
		ShowGT();

		//check for level end
		if (mode == GameMode.playing && Goal.goalMet) 
		{
			mode = GameMode.levelEnd;
			SwitchView("Both");
			Invoke("NextLevel", 2f);
		}
	}

	void NextLevel()
	{
		level++;
		if (level == levelMax) 
		{ level = 0;}
		StartLevel();
	}

	void OnGUI()
	{
		//draw GUI for view swiching
		Rect buttonRect = new Rect ((Screen.width / 2) - 50, 10, 100, 24);

		switch (showing) {
		case "Slingshot":
			if(GUI.Button(buttonRect, "Show Castle"))
			{ SwitchView("Castle");}
			break;

		case "Castle":
			if(GUI.Button(buttonRect, "Show Both"))
			{ SwitchView("Both");}
			break;

		case "Both":
			if(GUI.Button(buttonRect, "Show Slingshot"))
			{ SwitchView("Slingshot");}
			break;
		}
	}

	static public void SwitchView(string eView)
	{
		S.showing = eView;
		switch (S.showing) {
		case "Slingshot":
			FollowCam.S.poi = null;
			break;

		case "Castle":
			FollowCam.S.poi = S.castle;
			break;

		case "Both":
			FollowCam.S.poi = GameObject.Find("ViewBoth");
			break;
		}
	}

	public static void ShotFired()
	{
		S.shotsTaken++;
	}


}
