using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour {

	[Header("Set In Inspector")]
	public float lifeTime;

	[Header("Set Dynamically")]
	public float bornTime;

	float speed;
	float Yaxis;
	Vector3 moveRight;

	// Use this for initialization
	void Start () {
		speed = Random.Range(0.5f, 1f);
		Yaxis = Random.Range(-1f, 1f);

		moveRight = new Vector3(0.3f * speed, 1 * Yaxis, 0f);

		bornTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		Yaxis = Random.Range(-1f, 1f);

		if (border.pointRight)
		{
			transform.position += moveRight;
		}
		if (border.pointLeft)
		{
			transform.position -= moveRight;
		}

		if(Time.time - bornTime > lifeTime)
        {
			Destroy(gameObject);
        }
	}
}
