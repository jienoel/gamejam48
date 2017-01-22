using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using System.Text;

public class PitchManager : MonoBehaviour
{
	public List<DoubleFloat> lists = new List<DoubleFloat> ();
	public List<float> showTime = new List<float> ();
	public  int currIndex = -1;
	public float start = -1;
	public float end = -1;
	public float min = 0;
	public float max = 10;
	public float addon = 0.25f;
	public int step = 6;
	public float windowSize = 700;
	public float speed = 10;
	public float deltaSpeed = 4;
	public float tolerant = 0.5f;
	public float height = 1044f;
	public float duration = 0.5f;
	string formater = "{0}:{1}:{2}";
	// Use this for initialization

	void Awake ()
	{
		GameModel.Instance.PitchManager = this;
	}

	void Start ()
	{
		
//		lists = new List<DoubleFloat> ();
//		showTime = new List<float> ();

	}

	public void Init ()
	{
		
	}

	public void Clear ()
	{
		lists.Clear ();
		showTime.Clear ();
	}

	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnMusicEvent (float value)
	{
		return;
		int index = showTime.FindIndex (x =>
			Mathf.Abs (value - x) <= tolerant);
		if (index >= 0) {
			PitchStep step = GameManager.Instance.gameCache.GetPitchStep ();
			step.SetStepLength (lists [index]);
//			for (int i = 0; i <= index; i++) {
//				showTime.RemoveAt (i);
//				lists.RemoveAt (i);
//			}
		}
	}

	public void InitPlaceStep ()
	{
		for (int i = 0; i < showTime.Count; i++) {
			PlacePitch (showTime [i], lists [i]);
		}
	}


	public void SetPitchStepData (float value, float time)
	{

		int index = MathUtility.GetStepIndexByPitch (value, max, min, addon, step);
		if (currIndex != index) {
			if (start == -1 && time > end) {
				start = time;
				end = time + duration;
				currIndex = index;
				int lastIndex = -1;
				if (lists.Count > 0) {
					lastIndex = lists.Count - 1;
					DoubleFloat d = lists [lastIndex];
					if (d.pitch == currIndex) {
						d.time += (end - start);
						lists [lastIndex] = d;
					} else {
						showTime.Add (time);
						lists.Add (new DoubleFloat (end - start, currIndex));
					}

				} else {
					showTime.Add (time);
					lists.Add (new DoubleFloat (end - start, currIndex));
				}
//				PlacePitch (start, new DoubleFloat (end - start, currIndex));

				start = -1;
			} 
			currIndex = index;
			Debugger.Log (index + "  " + value + " " + max + "  " + min + " " + addon + "  " + step);
		}
	}



	public void SetPitchStepData1 (float value, float time)
	{

		int index = MathUtility.GetStepIndexByPitch (value, max, min, addon, step);
		if (currIndex != index) {
			if (start == -1) {
				start = time;
			} else {
				end = time;
				showTime.Add (MathUtility.PitchShowTime (windowSize, speed, time));
				lists.Add (new DoubleFloat (end - start, currIndex));
				PlacePitch (start, new DoubleFloat (end - start, currIndex));
//				PlacePitch (start, new DoubleFloat (duration, currIndex));
				start = time;
				end = -1;

			}
			currIndex = index;
			Debugger.Log (index + "  " + value + " " + max + "  " + min + " " + addon + "  " + step);
		}
	}

	void PlacePitch (float time, DoubleFloat data)
	{
		
		PitchStep step = GameManager.Instance.gameCache.GetPitchStep ();
		float y = MathUtility.GetScreenPositionByAudioPitch (data.pitch, GameModel.Instance.PitchManager.max, GameModel.Instance.PitchManager.min, GameModel.Instance.PitchManager.height,
			          GameModel.Instance.PitchManager.addon, GameModel.Instance.PitchManager.step);
		Vector3 pos = step.rect.localPosition;
		pos.y = y;
		if (y < 0)
			Debugger.LogError ("UI Error " + y);
		pos.x = 50 + time * speed;
		step.rect.localPosition = pos;
		Rect rect = step.rect.rect;
		rect.width = MathUtility.PitchStepWidth (speed, data.time);
		step.rect.sizeDelta = new Vector2 (rect.width, rect.height);
		step.speed = speed + deltaSpeed;
		step.moving = true;
	}
}
