using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour {

	Image timerFind;
	static public float startTimer;
	Coub levelNumberFind;
	static public bool gameEnd;

	// Use this for initialization
	void Start () {
		timerFind = GameObject.Find("Timer").GetComponent<Image>();
		levelNumberFind = Camera.main.GetComponent<Coub>();
	}
	
	// Update is called once per frame
	void Update () {
		float levelDoneTime = 5f + levelNumberFind.levelNumber * 0.25f;
		if (Time.time - startTimer < levelDoneTime && !GoalScript.Fading)
        {
			timerFind.fillAmount = (Time.time - startTimer) / levelDoneTime;
        }
        else if(Time.time - startTimer > levelDoneTime && !GoalScript.Fading)
        {
			GameEnd();
        }
		//timerFind.fillAmount = Time.time - startTimer / levelDoneTime;
	}
	void GameEnd()
    {
		gameEnd = true;
	}
}
