using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCache : MonoBehaviour
{
	public Queue<Apple> applecache = new Queue<Apple> ();
	public Apple applePrefab;
	public Transform parent;

	public Apple GetApple ()
	{
		if (applecache.Count > 0) {
			return applecache.Dequeue ();
		}
		return GameObject.Instantiate (applePrefab, parent);
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
	}
}
