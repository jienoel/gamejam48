using System.Collections;
using System.Collections.Generic;
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
	public float tolerant = 0.5f;
	public float height = 1044f;

	string formater = "{0}:{1}:{2}";
	// Use this for initialization
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



	public void SetPitchStepData (float value, float time)
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
				start = time;
				end = -1;

			}
			currIndex = index;
			Debugger.Log (index + "  " + value + " " + max + "  " + min + " " + addon + "  " + step);
		}
	}

	void PlacePitch (float time, DoubleFloat data)
	{
		if (MusicExport.Instance != null)
			MusicExport.Instance.Export1 (string.Format (formater, time, data.time, data.pitch));
		PitchStep step = GameManager.Instance.gameCache.GetPitchStep ();
		float y = MathUtility.GetScreenPositionByAudioPitch (data.pitch, GameManager.Instance.pitchManager.max, GameManager.Instance.pitchManager.min, GameManager.Instance.pitchManager.height,
			          GameManager.Instance.pitchManager.addon, GameManager.Instance.pitchManager.step);
		Vector3 pos = step.rect.localPosition;
		pos.y = y;
		pos.x = 50 + time * speed;
		step.rect.localPosition = pos;
		Rect rect = step.rect.rect;
		rect.width = MathUtility.PitchStepWidth (speed, data.time);
		step.rect.sizeDelta = new Vector2 (rect.width, rect.height);

	}
}
