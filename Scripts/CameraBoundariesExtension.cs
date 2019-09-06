using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundariesExtension : MonoBehaviour {

	private Vector3 vMin;
	private Vector3 vMax;

	private Vector3 min;
	private Vector3 max;

	[HideInInspector] public float minX;
	[HideInInspector] public float maxX;

	[HideInInspector] public float minY;
	[HideInInspector] public float maxY;

	void Start () 
	{
		vMin.Set (0, 0, 0);
		vMax.Set (1, 1, 0);

		min = Camera.main.ViewportToWorldPoint (vMin);
		max = Camera.main.ViewportToWorldPoint (vMax);

		minX = min.x;
		maxX = max.x;

		minY = min.y;
		maxY = max.y;
	}
}
