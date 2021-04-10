using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour {

	[Header("Set Dynamically")]
	public Vector3 mouse_pos;
	//public Transform targetT;
	public Vector3 object_pos;
	float angle;
	Vector3 pos;
	Hero heroFind;

	// Use this for initialization
	void Start () {
		heroFind = GameObject.Find("Hero(Clone)").GetComponent<Hero>();
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;

		mouse_pos = Input.mousePosition;
		mouse_pos.z = 5.23f;

		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mouse_pos.x -= objectPos.x;
		mouse_pos.y -= objectPos.y;

        if (!timerScript.gameEnd)
        {
			float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90f;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
			transform.position = heroFind.pos;
		}
	}
}
