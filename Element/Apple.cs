using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
	Heal,
	Damage
}

public class Apple : MonoBehaviour
{
	public int hp;
	public DamageType damageType = DamageType.Heal;

	void OnCollisionEnter (Collision collision)
	{
		Chick chick = collision.gameObject.GetComponent<Chick> ();
		if (chick != null) {
			chick.OnHitApple (this);
			GameManager.Instance.gameCache.RecycleApple (this);
		}
			
		
	}
}
