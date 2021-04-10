using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HealthType
{
	none,
	healthExist
}

[System.Serializable]
public class hpDefinition
{
	public HealthType type = HealthType.none;
	public Image healthBarImage;
}

public class hpScript : MonoBehaviour {

	[Header("Set In Inspector")]
	public hpDefinition[] hpDefinitions;
	public Transform findHPpanel;
	public GameObject hpPrefab;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}
}
