using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCat : MonoBehaviour {

	private Image image;
	private Button button;
	private SpriteState spriteState = new SpriteState ();
	private Text myText;
	private Color myColor;

	public int price;
	public int id;
	public Sprite[] newSprite;

	void Start () 
	{
		image = GetComponent<Image> ();
		button = GetComponent<Button> ();
		spriteState = button.spriteState;

		myColor = GetComponentInChildren<Text> ().color; 
		myText = GetComponentInChildren<Text> ();

		myText.text = price.ToString ();

		myColor.a = 0.0f;
	}

	void Update()
	{
		if ((StorageData.Instance.availabilityC & 1 << this.id) == 1 << this.id)
		{
			if (this.id == StorageData.Instance.catID) 
			{
				image.sprite = newSprite [2];
				spriteState.highlightedSprite = newSprite [2];
				spriteState.pressedSprite = newSprite [2];
				spriteState.disabledSprite = newSprite [2];	

				myText.color = myColor;

				button.spriteState = spriteState;
				button.onClick.AddListener (() => Select ());
			}

			else
			{
				image.sprite = newSprite [0];
				spriteState.highlightedSprite = newSprite [0];
				spriteState.pressedSprite = newSprite [1];
				spriteState.disabledSprite = newSprite [2];

				myText.color = myColor;

				button.spriteState = spriteState;
				button.onClick.AddListener (() => Select ());
			}
		}
	}

	public void Buy()
	{
		if ((StorageData.Instance.availabilityC & 1 << this.id) == 1 << this.id)
		{
			//Debug.Log (1 << this.id);
			button.onClick.AddListener (() => Select ());
		}

		else
		{
			//Debug.Log ("WHy?: " + this.id);

			if (StorageData.Instance.currency >= price)
			{
				image.sprite = newSprite [0];
				spriteState.highlightedSprite = newSprite [0];
				spriteState.pressedSprite = newSprite [1];
				spriteState.disabledSprite = newSprite [2];

				myText.color = myColor;

				button.spriteState = spriteState;

				StorageData.Instance.currency = StorageData.Instance.currency - price;
				StorageData.Instance.availabilityC += 1 << this.id;
				StorageData.Instance.Save ();

				button.onClick.AddListener (() => Select());

			}
		}

	}

	public void Select()
	{	
		if ((StorageData.Instance.availabilityC & 1 << this.id) == 1 << this.id)
		{
			FindObjectOfType<AudioManager>().Play("Cat Select");
			StorageData.Instance.catID = this.id;
			StorageData.Instance.Save ();
		}
	}
}
