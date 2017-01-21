using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PitchStep : MonoBehaviour
{
	public RectTransform rect;
	public bool moving;
	public float speed;
	Vector3 pos;
	// Use this for initialization
	void Start ()
	{
		if (rect == null) {
			rect = GetComponent<RectTransform> ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (moving) {
			pos = rect.localPosition;
			pos.x -= speed * Time.deltaTime;
			rect.localPosition = pos;
			if (pos.x <= -10) {
				GameManager.Instance.gameCache.RecyclePitchStep (this);
			}
		}
	}

	public void SetStepLength (DoubleFloat data)
	{
		this.gameObject.SetActive (true);
		float y = MathUtility.GetScreenPositionByAudioPitch (data.pitch, GameModel.Instance.PitchManager.max, GameModel.Instance.PitchManager.min, GameModel.Instance.PitchManager.height,
                      GameModel.Instance.PitchManager.addon, GameModel.Instance.PitchManager.step);
		pos = rect.localPosition;
		pos.y = y;
		rect.localPosition = pos;
		speed = GameModel.Instance.PitchManager.speed;
		moving = true;
	}
}
