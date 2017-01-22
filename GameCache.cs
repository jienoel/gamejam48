using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCache : MonoBehaviour
{
	public Queue<Apple> applecache = new Queue<Apple> ();
	public Queue<PitchStep> pitchcache = new Queue<PitchStep> ();
	public Queue<Apple> bigAppleCache = new Queue<Apple> ();

	public Apple bigApplePrefab;
	public Apple applePrefab;
	public Transform appleParent;
	public PitchStep pitchStepPrefab;
	public Transform pitchParent;

	public PitchStep GetPitchStep ()
	{
		if (pitchcache.Count > 0) {
			return pitchcache.Dequeue ();
		}
		return GameObject.Instantiate (pitchStepPrefab, pitchParent, false);
	}

	public void RecyclePitchStep (PitchStep pitchStep)
	{
		pitchStep.gameObject.SetActive (false);
		pitchcache.Enqueue (pitchStep);
	}

	public Apple GetApple (AppleSize appleSize = AppleSize.Small)
	{
		if (appleSize == AppleSize.Small) {
			if (applecache.Count > 0) {
				return applecache.Dequeue ();
			}
			return GameObject.Instantiate (applePrefab, appleParent, false);
		} else {
			if (bigAppleCache.Count > 0) {
				return bigAppleCache.Dequeue ();
			}
			return GameObject.Instantiate (bigApplePrefab, appleParent, false);
		}
	}

	public void RecycleApple (Apple prop)
	{
		
		prop.gameObject.SetActive (false);
		if (prop.appleSize == AppleSize.Small)
			applecache.Enqueue (prop);
		else
			bigAppleCache.Enqueue (prop);
	}

	public void Clear ()
	{
		while (applecache.Count > 0) {
			Apple prop = applecache.Dequeue ();
			DestroyImmediate (prop.gameObject);
		}	

		while (bigAppleCache.Count > 0) {
			Apple prop = bigAppleCache.Dequeue ();
			DestroyImmediate (prop.gameObject);
		}	

		while (pitchcache.Count > 0) {
			PitchStep pitchStep = pitchcache.Dequeue ();
			DestroyImmediate (pitchStep.gameObject);
		}
	}
}
