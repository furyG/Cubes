using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelNumberScript : MonoBehaviour {

	Text getText;
	Coub getNumber;

	Text GOlevel;
	Text GOscore;
	Text GOincrease;
	Text GObestScore;

	float yourScore;
	float bestScore;

	// Use this for initialization
	void Start () {

		getText = GameObject.Find("levelShow").GetComponent<Text>();
		getNumber = GameObject.Find("Main Camera").GetComponent<Coub>();

		GOlevel = GameObject.Find("LWN").GetComponent<Text>();
		GOscore = GameObject.Find("SWN").GetComponent<Text>();
		GOincrease = GameObject.Find("IncreaseNumber").GetComponent<Text>();
		GObestScore = GameObject.Find("BestScoreNumber").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		yourScore = getNumber.levelNumber * (getNumber.levelNumber / 10);

		getText.text = ("Level: " + getNumber.levelNumber);

		GOlevel.text = (" " + getNumber.levelNumber);
		GOscore.text = (" " +yourScore);
		GOincrease.text = (" " + getNumber.levelNumber / 10);

		if(yourScore > bestScore)
        {
			bestScore = yourScore;
        }

		GObestScore.text = (" ") + bestScore;
	}
}
