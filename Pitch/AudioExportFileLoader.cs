using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

public struct AudioClipDic
{
	public float time;
	public float pitch;
}

public static class AudioExportFileLoader
{
	public static string regexEmpty = @"^(\d+.\d+):(\d+)$";

	public static List<AudioClipDic> LoadAudioExportFile (string audioName)
	{
		if (string.IsNullOrEmpty (audioName))
			return null;
		List<AudioClipDic> list = new List<AudioClipDic> ();

		string path = Application.streamingAssetsPath + "/Trc/" + audioName + ".txt";
		FileInfo sr = new FileInfo (path);
		var reader = sr.OpenText ();
		string str;

		while ((str = reader.ReadLine ()) != null) {
			Debugger.Log (str);
			if (string.IsNullOrEmpty (str))
				continue;
			Match empty = Regex.Match (str, regexEmpty);
			if (empty.Success) {
//				list.Add (new AudioClipDic (float.Parse (empty.Groups [1].Value), float.Parse (empty.Groups [2].Value)));
			}
		}
		return list;
	}
}
