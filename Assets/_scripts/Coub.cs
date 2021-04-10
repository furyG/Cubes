using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Coub : MonoBehaviour {

	static Dictionary<BorderType, BorderDefinition> borderDict;

	[Header("Set In Inspector")]
	public BorderDefinition[] borderDefs;
	public Vector2[,] spawnCords;
	public GameObject[] currentBorders;

	public int bordersSpawnNumber = 0;

	public GameObject HeroPrefab;
	public GameObject levelZeroPrefab;
	public GameObject goalPrefab;
	public GameObject spotPrefab;
	public Vector3 heroSpawnPos;
	public GameObject findGOpanel;

	[Header("Set Dynamically")]
	public float levelNumber = 0f;
	static public int GameStartTime;

	GameObject levelSpawn;
	GameObject heroSpawn;
	GameObject brdrSpawn;
	Hero heroScript;
	GameObject heroFind;
	int brdrSpawnX;
	int brdrSpawnY;
	Vector3 lineAngle;

	// Use this for initialization
	void Start () {
		levelNumber = 1f;

		findGOpanel = GameObject.Find("gameOverPanel");
		findGOpanel.SetActive(false);

		heroFind = GameObject.Find("Hero");
		if(heroFind == null)
        {
			heroSpawn = Instantiate<GameObject>(HeroPrefab);
			heroSpawn.transform.position = heroSpawnPos;
		}
        else
        {
			Debug.Log("Tryed to spawn another hero");
        }

		borderDict = new Dictionary<BorderType, BorderDefinition>();
		foreach(BorderDefinition brdr in borderDefs)
        {
			borderDict[brdr.type] = brdr;
        }


		vec2ArrayCreate();
		CreateLevel();
	}

	void vec2ArrayCreate()
	{
		spawnCords = new Vector2[15, 9];
		Vector2 cordsTemp;
		//Vector2 cordsTempY;
		foreach (Vector2 cords in spawnCords)
		{
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					cordsTemp.x = -7 + i;
					cordsTemp.y = 4f - j;
					spawnCords[i, j] = cordsTemp;
				}
			}
            //GameObject spot = Instantiate<GameObject>(spotPrefab);
            //spot.transform.position = cords;
        }
	}

	void CreateLevel()
    {
		timerScript.startTimer = Time.time;
		levelNumber += 1f;
		if(levelSpawn!= null)
        {
			Destroy(levelSpawn);
        }
        else
        {
			Debug.Log("level is not spawned yet");
        }
		findGOpanel.SetActive(false);

		heroSpawn.transform.position = spawnCords[7, 5];

		Debug.Log(" vremya starta: " + timerScript.startTimer);

		float bordersCount = Mathf.Round(levelNumber / 1.99f);
		currentBorders = new GameObject[(int)bordersCount];

		bordersSpawnNumber = 0;

		levelSpawn = Instantiate<GameObject>(levelZeroPrefab);
		float calculateNumber = Random.Range(0.1f+(levelNumber/70), 1f+(levelNumber/70));
		Debug.Log(" " + calculateNumber);

		foreach (BorderDefinition brdr in borderDefs)
		{
			if (bordersSpawnNumber < bordersCount)
			{
				if (calculateNumber > brdr.spawnChance)
				{
					cordGenerate();

					brdrSpawn = Instantiate<GameObject>(brdr.borderPrefab);
					brdrSpawn.transform.position = spawnCords[brdrSpawnX, brdrSpawnY];
					brdrSpawn.transform.parent = levelSpawn.transform;
					bordersSpawnNumber += 1;
					border getBrdrScript = brdrSpawn.GetComponent<border>();
					float rotDirection = Random.Range(-3f, 3f);
					getBrdrScript.rotSpeed *= rotDirection;


					for (int i = 0; i < bordersSpawnNumber; i++)
					{
						if (brdrSpawn != null)
						{
							currentBorders[i] = brdrSpawn;
						}
					}
				}
			}
		}

		float chooseGoalPos = Random.Range(0,bordersCount);

		GameObject goalSpawn = Instantiate<GameObject>(goalPrefab);
		goalSpawn.transform.position = brdrSpawn.transform.position;
		if(brdrSpawn.name == "borderLine(Clone)")
        {
			goalSpawn.transform.position = new Vector2(brdrSpawn.transform.position.x+1f,
				brdrSpawn.transform.position.y);
        }
		goalSpawn.transform.parent = levelSpawn.transform;

		for (int i = 0; i < bordersCount; i++)
		{
			if (currentBorders[i] == null)
			{
				cordGenerate();
				GameObject cubeSpawn = Instantiate<GameObject>(borderDict[BorderType.Cube].borderPrefab);
				cubeSpawn.transform.position = spawnCords[brdrSpawnX, brdrSpawnY];
				cubeSpawn.transform.parent = levelSpawn.transform;

				border getBrdrScript = cubeSpawn.GetComponent<border>();
				float rotDirectChoose = Random.Range(-1f, 1f);
				getBrdrScript.rotSpeed += levelNumber * rotDirectChoose;

				Vector2 toGoalVector = cubeSpawn.transform.position - goalSpawn.transform.position;
				float goalDistnce = toGoalVector.magnitude;
				if(goalDistnce <= 2f)
                {
					Destroy(cubeSpawn);
                }
			}
		}
	}
	void Update()
    {
        if (Hero.goalMet)
        {
			DestroyLevel();
        }
        if (timerScript.gameEnd)
        {
			findGOpanel.SetActive(true);
        }
        else
        {
			findGOpanel.SetActive(false);
		}
    }

	void DestroyLevel()
    {
		if (levelSpawn != null)
		{
			Hero.goalMet = false;
			border.pointLeft = false;
			border.pointRight = false;
			Invoke("CreateLevel", 1f);
		}
		else
		{
			//Debug.Log("level is not created");
		}
    }
	void cordGenerate()
    {
		brdrSpawnX = Random.Range(0, 15);
		brdrSpawnY = Random.Range(0, 9);
		if(brdrSpawnX == 7 || brdrSpawnY == 5)
        {
			cordGenerate();
        }
	}
	public void GameRestart()
    {
		timerScript.gameEnd = false;
		timerScript.startTimer = Time.time;
		levelNumber = 0;
		Destroy(levelSpawn);
		DestroyLevel();
	}
	public void Exit()
    {
		Application.Quit();
    }
}
