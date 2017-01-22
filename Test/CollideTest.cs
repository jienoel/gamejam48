using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideTest : MonoBehaviour
{
	public Chick chick;

	void OnTriggerEnter2D (Collider2D other)
	{
		Apple apple = other.GetComponentInParent<Apple> ();
		chick.OnHitApple (apple);
		GameManager.Instance.gameCache.RecycleApple (apple);
	}
}
