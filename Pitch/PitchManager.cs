using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	// Use this for initialization
	void Start ()
	{
//		lists = new List<DoubleFloat> ();
//		showTime = new List<float> ();
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
//		if (index > step)

//		if (currIndex == -1) {
//			currIndex = index;
//			if (start == -1)
//				start = time;
//		} 
		if (currIndex != index) {
			if (start == -1) {
				start = time;
			} else {
				end = time;
				showTime.Add (MathUtility.PitchShowTime (windowSize, speed, time));
				lists.Add (new DoubleFloat (end - start, currIndex));
				start = time;
				end = -1;
//				Debug.LogError ("Test");
			}
			currIndex = index;
			Debugger.Log (index + "  " + value + " " + max + "  " + min + " " + addon + "  " + step);
		}
	}
}
