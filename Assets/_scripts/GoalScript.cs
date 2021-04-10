using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {

	Behaviour halo;
	static public bool Fading = false;
	Color alphaColor;
	EdgeCollider2D eCollider;

	// Use this for initialization
	void Start () {
		halo = (Behaviour)GetComponent("Halo");
		halo.enabled = false;
		Fading = false;

		alphaColor = GetComponent<Renderer>().material.color;
		alphaColor.a = 0;

		eCollider = GetComponent<EdgeCollider2D>();
		eCollider.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Fading)
        {
			halo.enabled = true;
			GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color,
alphaColor, 5f * Time.deltaTime);
		}
        else
        {
			halo.enabled = false;
        }
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collidedWith = coll.gameObject;
		if(collidedWith.tag == "Hero")
        {
			Fading = true;
			eCollider.enabled = false;
		}
    }
}
