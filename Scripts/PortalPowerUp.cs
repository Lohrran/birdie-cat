using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPowerUp : MonoBehaviour 
{
	private CameraBoundariesExtension cameraB;

	void Awake()
	{
		cameraB = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraBoundariesExtension> ();
	}

	void Update ()
	{
		//Bottom
		if (transform.position.y < cameraB.minY - 0.1f)
		{
			transform.position = new Vector3 (transform.position.x, transform.position.y - cameraB.minY * 2);
		}

		//Top
		if (transform.position.y > cameraB.maxY - 0.1f)
		{
			transform.position = new Vector3 (transform.position.x, transform.position.y - cameraB.maxY * 2);
		}
	}
}
