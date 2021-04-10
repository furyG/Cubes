using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenBorder : MonoBehaviour {

	[Header("Set In Inspector")]
	public float radius = 1f;

	[Header("Set Dynamically")]
	public float camWidth;
	public float camHeight;

	void Awake()
    {
		camHeight = Camera.main.orthographicSize;
		camWidth = camHeight * Camera.main.aspect;
    }

	void LateUpdate()
    {
		Vector3 pos = transform.position;

		if(pos.x > camWidth - radius)
        {
			pos.x = camWidth - radius;
			border.screenColided = !border.screenColided;
        }

		if (pos.x < -camWidth + radius)
		{
			pos.x = -camWidth + radius;
			border.screenColided = !border.screenColided;
		}

		if (pos.y > camHeight - radius)
		{
			pos.y = camHeight - radius;
			border.screenColided = !border.screenColided;
		}

		if (pos.y < -camHeight + radius)
		{
			pos.y = -camHeight + radius;
			border.screenColided = !border.screenColided;
		}

		transform.position = pos;
	}
}
