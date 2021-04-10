using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class border : MonoBehaviour {

	[Header("Set In Inspector")]
	public float rotSpeed;
	public bool rightTurning;
	public bool isLine = false;
	public bool isHorizontal = false;
	public bool isCube;
	public bool isPointer;
	public GameObject trailPrefab;
	public float movingSpeed;
	public float paintTime;

	[Header("Set Dynamically")]
	float startTime;
	Vector2 pos;
	static public bool screenColided = false;
	screenBorder scrBrdr;
	static public bool pointRight;
	static public bool pointLeft;
	Color alphaColor;
	bool trailSpawned = false;
	Color originColor;
	float paintStartTime;
	bool showingTouch;

	void Awake()
    {
		scrBrdr = GetComponent<screenBorder>();
        if (rightTurning)
        {
			rotSpeed = -rotSpeed;
        }
        if (isLine)
        {
			scrBrdr.radius = 2.08f * transform.localScale.y;
        }
    }

	void Start()
    {
		startTime = Time.time;

		originColor = GetComponent<Renderer>().material.color;

		alphaColor = GetComponent<Renderer>().material.color;
		alphaColor.a = 0;

		trailSpawned = false;

		if (isPointer)
		{
			float directionChoose = Random.Range(-1f, 1f);
			if (directionChoose <= 0)
			{
				this.transform.rotation = Quaternion.Euler(0, 0, 180);
				transform.position = new Vector2(-8.5f, 0f);
				pointRight = true;
			}
			else if (directionChoose > 0)
			{
				this.transform.rotation = Quaternion.Euler(0, 0, 0);
				transform.position = new Vector2(8.5f, 0f);
				pointLeft = true;
			}
		}
	}

	// Update is called once per frame
	void Update() {
		pos = transform.position;
		if (!isLine)
		{
			Vector3 rotVector = new Vector3(0f, 0f, 1f) * Time.deltaTime * rotSpeed;
			transform.rotation *= Quaternion.Euler(rotVector);
		}
        if (isLine && !isHorizontal)
        {
            if (screenColided)
            {
				pos.y += movingSpeed * Time.deltaTime;
            }
            else
            {
				pos.y -= movingSpeed * Time.deltaTime;
			}
			transform.position = pos;
        }
		if(isLine && isHorizontal)
        {
			if (screenColided)
			{
				pos.x += movingSpeed * Time.deltaTime;
			}
			else
			{
				pos.x -= movingSpeed * Time.deltaTime;
			}
			transform.position = pos;
		}
        if (GoalScript.Fading)
        {
			GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color,
	alphaColor, 5f * Time.deltaTime);
		}

        if (isPointer && !trailSpawned)
        {
			spawnTrails();
        }
		if(Time.time - paintStartTime > paintTime && showingTouch)
        {
			paintStop();
        }
	}
	void spawnTrails()
    {
		trailSpawned = true;
		for(int i = 0; i < 2; i++)
        {
			GameObject trailSpawn = Instantiate<GameObject>(trailPrefab);
			trailSpawn.transform.position = new Vector2(transform.position.x,
				transform.position.y + i);
        }
		Invoke("spawnTrails", 1f);
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		GameObject collidedWith = coll.gameObject;
		if(collidedWith.tag == "Goal" && isCube)
        {
			Destroy(gameObject);
        }
		if(collidedWith.tag == "Spawn")
        {
			Destroy(gameObject);
        }
		if(collidedWith.tag == "Hero")
        {
			paintBorder();
        }
	}
	void paintBorder()
    {
		GetComponent<Renderer>().material.color = Color.red;
		showingTouch = true;
		paintStartTime = Time.time;
		Debug.Log("touched");
    }
	void paintStop()
    {
		GetComponent<Renderer>().material.color = originColor;
		showingTouch = false;
	}
}
