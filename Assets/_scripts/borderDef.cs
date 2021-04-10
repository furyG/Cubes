using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BorderType
{
	none,
	Owide,
	Quadro,
	Onarrow,
	Line,
	Cube,
	Pointer,
	QuadroBig
}

[System.Serializable]
public class BorderDefinition
{
	public BorderType type = BorderType.none;
	public Color color = Color.white;
	public GameObject borderPrefab;
	public float spawnChance;
}

public class borderDef : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		
	}
}
