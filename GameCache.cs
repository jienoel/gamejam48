using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCache : MonoBehaviour
{
	public Queue<Apple> applecache = new Queue<Apple> ();
	public Queue<PitchStep> pitchcache = new Queue<PitchStep> ();
	public Apple applePrefab;
	public Transform appleParent;
	public PitchStep pitchStepPrefab;
	public Transform pitchParent;

	public PitchStep GetPitchStep ()
	{
		if (pitchcache.Count > 0) {
			return pitchcache.Dequeue ();
		}
		return GameObject.Instantiate (pitchStepPrefab, pitchParent);
	}

	public void RecyclePitchStep (PitchStep pitchStep)
	{
		pitchStep.gameObject.SetActive (false);
		pitchcache.Enqueue (pitchStep);
	}

	public Apple GetApple ()
	{
		if (applecache.Count > 0) {
			return applecache.Dequeue ();
		}
		return GameObject.Instantiate (applePrefab, appleParent);
	}

	public void RecycleApple (Apple prop)
	{
		prop.gameObject.SetActive (false);
		applecache.Enqueue (prop);
	}

	public void Clear ()
	{
		while (applecache.Count > 0) {
			Apple prop = applecache.Dequeue ();
			DestroyImmediate (prop.gameObject);
		}	

		while (pitchcache.Count > 0) {
			PitchStep pitchStep = pitchcache.Dequeue ();
			DestroyImmediate (pitchStep.gameObject);
		}
	}
}
