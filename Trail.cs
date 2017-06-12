using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{

	public SpriteRenderer mLeadingSprite;

	public int mTrailSegments;
	public float mTrailTime;
	public GameObject mTrailObject;

	private float mSpawnInterval;
	private float mSpawnTimer;
	private bool mbEnabled;

	private List<GameObject> mTrailObjectsInUse;
	private Queue<GameObject> mTrailObjectsNotInUse;


	void Start () 
	{
		mSpawnInterval = mTrailTime / mTrailSegments;
		mTrailObjectsInUse = new List<GameObject> ();
		mTrailObjectsNotInUse = new Queue<GameObject> ();

		for(int i = 0; i < mTrailSegments; i++)
		{
			GameObject trail = GameObject.Instantiate (mTrailObject);
			trail.transform.SetParent (transform);
			mTrailObjectsNotInUse.Enqueue (trail);
		}

		mbEnabled = false;
	}
	
	void Update () 
	{
		if(mbEnabled)
		{
			mSpawnTimer += Time.deltaTime;

			if(mSpawnTimer >= mSpawnInterval)
			{
				//Doesn't have an if
				if (mTrailObjectsNotInUse.Count > 0)
				{
					GameObject trail = mTrailObjectsNotInUse.Dequeue ();	
					if(trail != null)
					{
						TrailObject trailObject = trail.GetComponent<TrailObject> ();
						trailObject.Initiate (mTrailTime, mLeadingSprite.sprite, transform.position, this);
						mTrailObjectsInUse.Add (trail);
						mSpawnTimer = 0;
					}
				}
			}
		}
	}


	public void RemoveTrailObject(GameObject obj)
	{
		mTrailObjectsInUse.Remove (obj);
		mTrailObjectsNotInUse.Enqueue (obj);
	}

	public void SetEnabled (bool enabled)
	{
		mbEnabled = enabled;

		if(enabled)
		{
			mSpawnTimer = mSpawnInterval;
		}
	}

}
