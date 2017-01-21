using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour
{
	public int maxHp;
	public int hp;
	public RectTransform rect;

	public float targetY;
	public bool moving;
	public float tolerance = 1f;
	public float deltaTime = 0.5f;
	// Use this for initialization
	void Start ()
	{
		if (rect == null)
			rect = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		

		if (moving) {
			Move ();
		}

		if (moving && Mathf.Abs (targetY - rect.localPosition.y) <= tolerance) {
			moving = false;
		}
	}

	public void MoveTo (float y)
	{
		if (targetY != y)
			targetY = y;
		if (!moving) {
			moving = true;
		}
	}

	void Move ()
	{
		Vector3 position = rect.localPosition;

		float y = Mathf.Lerp (position.y, targetY, deltaTime);
		if (position.y != y) {
			position.y = y;
			rect.localPosition = position;
		} else {
			moving = false;
		}
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
