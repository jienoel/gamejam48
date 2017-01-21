using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour
{
	public int maxHp;
	public int hp;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnHitApple (Apple prop)
	{
		int hpBefore = hp;
		if (prop.damageType == DamageType.Heal) {
			hp = Mathf.Min (maxHp, hp + prop.hp);
		} else {
			hp = Mathf.Max (0, hp - prop.hp);
		}
		if (hpBefore != hp) {
			float ratio = ((float)hp) / maxHp;
			GameManager.Instance.uiManger.SetChickHp (ratio);
		}
	}
}
