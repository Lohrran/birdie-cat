using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerPowerUps : MonoBehaviour {

	public enum PowerUpType
	{
		Normal,
		Shield,
		Magnet,
		Portal
	}

	//public AudioMixerSnapshot music1;
	//public AudioMixerSnapshot music2;

	private PowerUpType powerUp;

	public float powerUpDuration;
	public GameObject shield;
	public GameObject magnet;
	public GameObject portalObj;
	public Animator animShield;
	public Animator animMagnet;
	public List<Animator> animPortal = new List <Animator> ();

	private Animator animSky;
	public List<Animator> animCloud = new List <Animator> ();

	private float timeCount;
	private CapsuleCollider2D playerCollider;
	private PlayerScript playerScript;
	public GameObject bgEffect;

	private PortalPowerUp portalScript;

	#region Unity
	void Start ()
	{
		powerUp = PowerUpType.Normal;
		timeCount = powerUpDuration;
		playerCollider = GetComponent<CapsuleCollider2D> ();

		playerScript = GetComponent<PlayerScript> ();


		portalScript = GetComponentInChildren<PortalPowerUp> ();
		portalScript.enabled = false;

		bgEffect.SetActive (false);

		animSky = GameObject.FindGameObjectWithTag ("Sky").GetComponent<Animator> ();
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

		case PowerUpType.Portal:
			StartCoroutine (Portal());
			break;
		}
	}

	IEnumerator Normal ()
	{
		//music1.TransitionTo (.01f);

		if (GetComponent<PlayerScript>().isDead == false)
		{
			playerCollider.enabled = true;
		}

		shield.SetActive (false);
		magnet.SetActive (false);


		portalObj.SetActive (false);


		portalScript.enabled = false;


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
			//music2.TransitionTo (animShield.GetCurrentAnimatorStateInfo (0).length);	
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
			//music1.TransitionTo (animShield.GetCurrentAnimatorStateInfo (0).length);	
			yield return new WaitForSeconds (animShield.GetCurrentAnimatorStateInfo(0).length);
			timeCount = powerUpDuration;
			powerUp = PowerUpType.Normal;
			playerCollider.enabled = true;
			bgEffect.SetActive (false);
			StopTime(false);
		}
	}


	IEnumerator Magnet ()
	{
		timeCount -= Time.deltaTime;
		magnet.SetActive(true);

		//Start Animation, and Call Idle Animation
		if (animMagnet.GetCurrentAnimatorStateInfo(0).IsName("Start"))
		{	
			//music2.TransitionTo (animMagnet.GetCurrentAnimatorStateInfo (0).length);	
			bgEffect.SetActive (true);

			StopTime(true);
			yield return new WaitForSeconds (animMagnet.GetCurrentAnimatorStateInfo(0).length);
			animMagnet.SetInteger ("State", 1);
			bgEffect.SetActive (false);
			StopTime(false);

		}

		//Call Middle Animation
		if (animMagnet.GetCurrentAnimatorStateInfo(0).IsName("Idle") && timeCount <= 3)
		{
			animMagnet.SetInteger ("State", 2);
		}

		//Call End Animation
		if (animMagnet.GetCurrentAnimatorStateInfo(0).IsName("Middle") && timeCount <= 0)
		{
			animMagnet.SetInteger ("State", 3);
			bgEffect.SetActive (true);
			StopTime (true);
		}

		//Call PoweUp to Normal
		if (animMagnet.GetCurrentAnimatorStateInfo(0).IsName("End"))
		{
			//music1.TransitionTo (animMagnet.GetCurrentAnimatorStateInfo (0).length);	
			yield return new WaitForSeconds (animMagnet.GetCurrentAnimatorStateInfo(0).length);
			timeCount = powerUpDuration;
			powerUp = PowerUpType.Normal;
			bgEffect.SetActive (false);
			StopTime(false);
		}
	}

	IEnumerator Portal ()
	{
		timeCount -= Time.deltaTime;
		portalObj.SetActive (true);
		portalScript.enabled = true;
	
		for (int i = 0; i < animPortal.Count; i++)
		{
			//Start Animation, and Call Idle Animation
			if (animPortal[0].GetCurrentAnimatorStateInfo(0).IsName("Start"))
			{
				//music2.TransitionTo (animPortal[0].GetCurrentAnimatorStateInfo (0).length);	
				bgEffect.SetActive (true);

				StopTime(true);
				yield return new WaitForSeconds (animPortal[0].GetCurrentAnimatorStateInfo(0).length);
				animPortal[i].SetInteger ("State", 1);
				bgEffect.SetActive (false);
				StopTime(false);
				animSky.SetTrigger ("Green");
				animCloud[i].SetTrigger ("Green");
			}

			//Call Middle Animation
			if (animPortal[i].GetCurrentAnimatorStateInfo(0).IsName("Idle") && timeCount <= 3)
			{
				animPortal[i].SetInteger ("State", 2);
				animSky.SetTrigger ("Green_Blue");
				animCloud[i].SetTrigger ("Green_Blue");
			}

			//Call End Animation
			if (animPortal[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Middle") && timeCount <= 0)
			{
				animPortal[i].SetInteger ("State", 3);
				animSky.SetTrigger ("Blue");
				animCloud[i].SetTrigger ("Blue");
				bgEffect.SetActive (true);
				StopTime (true);
			}

			//Call PoweUp to Normal
			if (animPortal[i].GetCurrentAnimatorStateInfo(0).IsName("End"))
			{
				//music1.TransitionTo (animPortal[0].GetCurrentAnimatorStateInfo (0).length);	
				yield return new WaitForSeconds (animPortal[i].GetCurrentAnimatorStateInfo(0).length);
				animSky.SetTrigger ("Blue");
				animCloud[i].SetTrigger ("Blue");
				timeCount = powerUpDuration;
				powerUp = PowerUpType.Normal;
				bgEffect.SetActive (false);
				StopTime(false);
			}
		}
	}

	void StopTime(bool stop)
    {
        if (stop == true)
        {
            Time.timeScale = 0.001f;
            animShield.speed = 1000;
			animMagnet.speed = 1000;
			for (int i = 0; i < animPortal.Count; i++)
			{
				animPortal[i].speed = 1000;
			}
			playerScript.enabled = false;
        }

        else if (stop == false)
        {
            Time.timeScale = 1;
            animShield.speed = 1;
			animMagnet.speed = 1;
			for (int i = 0; i < animPortal.Count; i++)
			{
				animPortal[i].speed = 1;

			}
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
			powerUp = PowerUpType.Portal;
		}
	}
	#endregion
}
