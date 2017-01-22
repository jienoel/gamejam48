using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

[System.Serializable]
public struct DoubleFloat
{
	public float time;
	public float pitch;

	public DoubleFloat (float time, float pitch)
	{
		this.pitch = pitch;
		this.time = time;
	}
}

public static class AudioExportFileLoader
{
	public static string regexEmpty = @"^(\d+.\d+):(\d+)$";

	public static List<DoubleFloat> LoadAudioExportFile (string audioName, out float min, out float max)
	{
		min = 10000000000;
		max = 0;
		Debug.LogError ("path");
		if (string.IsNullOrEmpty (audioName))
			return null;
		List<DoubleFloat> list = new List<DoubleFloat> ();

		string path = Application.streamingAssetsPath + "/Exp/" + audioName + ".txt";

		FileInfo sr = new FileInfo (path);
		var reader = sr.OpenText ();
		string str;

		while ((str = reader.ReadLine ()) != null) {
			if (string.IsNullOrEmpty (str))
				continue;
			Match empty = Regex.Match (str, regexEmpty);
//			Debugger.Log (str);
			if (empty.Success) {
				float value = float.Parse (empty.Groups [2].Value);
				min = Mathf.Min (min, value);
				max = Mathf.Max (max, value);
				
				list.Add (new DoubleFloat (float.Parse (empty.Groups [1].Value), value));
			}
		}
		Debugger.Log (min + "  :" + max);
		return list;
	}
}
