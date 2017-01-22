using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideTest : MonoBehaviour
{
	public Chick chick;

	void OnTriggerEnter2D (Collider2D other)
	{
		Apple apple = other.GetComponentInParent<Apple> ();
		if (apple != null) {
			chick.OnHitApple (apple);
			GameManager.Instance.gameCache.RecycleApple (apple);
		}

		PitchStep pitch = other.GetComponentInParent<PitchStep> ();
		if (pitch != null) {
			chick.OnHitPitch (pitch, true);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		PitchStep pitch = other.GetComponentInParent<PitchStep> ();
		if (pitch != null) {
			chick.OnHitPitch (pitch, false);
		}
	}
}
