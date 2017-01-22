using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PitchStep : MonoBehaviour
{
	public RectTransform rect;
	public bool moving;
	public float speed;
	public Vector3 pos;
	// Use this for initialization
	void Start ()
	{
		if (rect == null) {
			rect = GetComponent<RectTransform> ();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (moving) {
			pos = rect.localPosition;
			pos.x -= speed * Time.fixedDeltaTime;
			rect.localPosition = pos;
			if (pos.x + rect.sizeDelta.x <= -10) {
				GameManager.Instance.gameCache.RecyclePitchStep (this);
			}
		}
	}

	public void OnEnable ()
	{
		if (moving == false)
			moving = true;
	}

	public void SetStep ()
	{
		
	}

	public void SetStepLength (DoubleFloat data)
	{
//		this.gameObject.SetActive (true);
//		float y = MathUtility.GetScreenPositionByAudioPitch (data.pitch, GameModel.Instance.PitchManager.max, GameModel.Instance.PitchManager.min, GameModel.Instance.PitchManager.height,
//			          GameModel.Instance.PitchManager.addon, GameModel.Instance.PitchManager.step);
//		pos = rect.localPosition;
//		pos.y = y;
//		pos.x = GameModel.Instance.PitchManager.windowSize;
//		rect.localPosition = pos;
//		speed = GameModel.Instance.PitchManager.speed;
//		moving = true;
	}
}
