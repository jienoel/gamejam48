using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
	Heal,
	Damage
}

public enum AppleSize
{
	Big,
	Small
}

public class Apple : MonoBehaviour
{
	public int hp;
	public DamageType damageType = DamageType.Heal;
	public AppleSize appleSize = AppleSize.Big;
	public RectTransform rect;
	public bool moving;
	public float speed;
	Vector3 pos;

	void Start ()
	{
		if (rect == null)
			rect = GetComponent<RectTransform> ();
	}


	void Update ()
	{
		if (moving) {
			pos = rect.localPosition;
			pos.x -= speed * Time.fixedDeltaTime;
			rect.localPosition = pos;
			if (pos.x + rect.sizeDelta.x <= -10) {
				GameManager.Instance.gameCache.RecycleApple (this);
			}
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		Chick chick = collision.gameObject.GetComponent<Chick> ();
		if (chick != null) {
			chick.OnHitApple (this);
			GameManager.Instance.gameCache.RecycleApple (this);
		}
			
		
	}
}
