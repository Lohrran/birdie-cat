using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReSkinAnimation : MonoBehaviour {

	private string spriteSheetName;
	public string id;

	void Start ()
	{
		if (id == "cat")
		{
			spriteSheetName = StorageData.Instance.catSkin;
			//Debug.LogFormat ("Cat: " + spriteSheetName);
		}

		else if (id == "birdie")
		{
			spriteSheetName = StorageData.Instance.birdieSkin;
			//Debug.LogFormat("Birdie: " + spriteSheetName);
		}
	}

	void LateUpdate()
	{
		var subSprites = Resources.LoadAll<Sprite> ("Characters/" + spriteSheetName);

		foreach (var renderer in GetComponentsInChildren <SpriteRenderer>())
		{
			string spriteName = renderer.sprite.name;
			var newSprite = Array.Find (subSprites, item => item.name == spriteName);

			if(newSprite)
			{
				renderer.sprite = newSprite;
			}
		}
	}
}
