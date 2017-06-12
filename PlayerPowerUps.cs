using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour {

	public enum PowerUpType
	{
		Normal,
		Shield,
		Magnet,
		TimeFreeze
	}
	private PowerUpType powerUp;

	public float powerUpDuration;
	public GameObject shield;
	public GameObject magnet;
	public Animator animShield;

	private float timeCount;
	private CapsuleCollider2D playerCollider;
	private PlayerScript playerScript;
	public GameObject bgEffect;

	#region Unity
	void Start ()
	{
		powerUp = PowerUpType.Normal;
		timeCount = powerUpDuration;
		playerCollider = GetComponent<CapsuleCollider2D> ();

		playerScript = GetComponent<PlayerScript> ();

		bgEffect.SetActive (false);

	}

	void Update ()
	{
		PowerUpSelect ();
	}
	#endregion

	#region PowerUpsControl
	void PowerUpSelect ()
	{
		switch (powerUp) 
		{
		case PowerUpType.Normal:
			StartCoroutine (Normal ());
			break;

		case PowerUpType.Shield:
			StartCoroutine (Shield());
			break;

		case PowerUpType.Magnet:
			StartCoroutine (Magnet());
			break;

		case PowerUpType.TimeFreeze:
			StartCoroutine (TimeFreeze());
			break;
		}
	}

	IEnumerator Normal ()
	{
		//FadeOut the PowerUp
		if (GetComponent<PlayerScript>().isDead == false)
		{
			playerCollider.enabled = true;
		}

		shield.SetActive (false);
		magnet.SetActive (false);
       // StopTime(false);
		yield return null;
	}

	IEnumerator Shield ()
	{
		timeCount -= Time.deltaTime;
        shield.SetActive(true);
        playerCollider.enabled = false;

		//Start Animation, and Call Idle Animation
		if (animShield.GetCurrentAnimatorStateInfo(0).IsName("Start"))
		{			
			bgEffect.SetActive (true);
			
			StopTime(true);
			yield return new WaitForSeconds (animShield.GetCurrentAnimatorStateInfo(0).length);
			animShield.SetInteger ("State", 1);
			bgEffect.SetActive (false);
			StopTime(false);

        }

		//Call Middle Animation
		if (animShield.GetCurrentAnimatorStateInfo(0).IsName("Idle") && timeCount <= 3)
		{
			animShield.SetInteger ("State", 2);
		}

		//Call End Animation
		if (animShield.GetCurrentAnimatorStateInfo(0).IsName("Middle") && timeCount <= 0)
		{
			animShield.SetInteger ("State", 3);
			bgEffect.SetActive (true);
			StopTime (true);
		}

		//Call PoweUp to Normal
		if (animShield.GetCurrentAnimatorStateInfo(0).IsName("End"))
		{
			yield return new WaitForSeconds (animShield.GetCurrentAnimatorStateInfo(0).length);
			timeCount = powerUpDuration;
			powerUp = PowerUpType.Normal;
			bgEffect.SetActive (false);
			StopTime(false);
		}
	}


	IEnumerator Magnet ()
	{
		magnet.SetActive (true);

		yield return new WaitForSeconds (powerUpDuration);
		powerUp = PowerUpType.Normal;
	}

	IEnumerator TimeFreeze ()
	{
		yield return new WaitForSeconds (powerUpDuration);
		powerUp = PowerUpType.Normal;
	}

	void StopTime(bool stop)
    {
        if (stop == true)
        {
            Time.timeScale = 0.001f;
            animShield.speed = 1000;
			playerScript.enabled = false;
        }

        else if (stop == false)
        {
            Time.timeScale = 1;
            animShield.speed = 1;
			playerScript.enabled = true;
        }
    }

	#endregion


	#region ReceiveCollision
	void MailBox (int ID)
	{
		if (ID == 1) 
		{
			powerUp = PowerUpType.Shield;
		} 
		else if (ID == 2)
		{
			powerUp = PowerUpType.Magnet;
		}
		else if (ID == 3) 
		{
			powerUp = PowerUpType.TimeFreeze;
		}
	}
	#endregion
}
