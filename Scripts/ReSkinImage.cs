using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ReSkinImage : MonoBehaviour {

	private string imageName;
	private Image image;

	void Awake()
	{
		image = GetComponent<Image> ();
	}
	void Start()
	{
		imageName = UnityAdService.Instance.adID;
	}

	void LateUpdate ()
	{
		//image.sprite = Resources.LoadAll<Sprite> ("Ad/" + imageName);
		image.sprite = Resources.Load<Sprite> ("Ad/" + imageName);
		/*foreach (var renderer in GetComponentsInChildren <SpriteRenderer>())
		{
			string spriteName = image.sprite.name;
			var newSprite = Array.Find (subSprites, item => item.name == spriteName);

			if(newSprite)
			{
				renderer.sprite = newSprite;
			}
		}*/
	}
}
