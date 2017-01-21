using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Collections.Generic;

//using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LyricCell
{
	public float time;
	public string lyric;

	public LyricCell (float time, string lyric)
	{
		this.time = time;
		this.lyric = lyric;
	}
}

public class LRC : MonoBehaviour
{
	public float tolerance = 0.1f;
	public Text MusicLrc;
	public Text MusicLrc1;
	private AudioSource mp3;
	public string path;
	public float music;
	public int timesample;
	string regexString = @"^\[(\d{2}):(\d{2}).(\d{2})\](.+)$";
	string regexEmpty = @"^\[(\d{2}):(\d{2}).(\d{2})\]$";
	//	Dictionary<float,string> lyricsDic = new Dictionary<float, string> ();
	public List<LyricCell> lyrics = new List<LyricCell> ();
	LyricCell current;
	LyricCell next;
	LyricCell next1;
	LyricCell next2;

	private void Start ()
	{
		mp3 = gameObject.GetComponentInChildren<AudioSource> () as AudioSource;
		path = Application.streamingAssetsPath + "/Trc/" + mp3.clip.name + ".lrc"; //获取歌词路径，并同步歌词和歌曲名称
		ReadFile ();
		if (lyrics.Count > 2) {
			next = lyrics [0];
			next1 = lyrics [1];
			next2 = lyrics [2];

			lyrics.Remove (next);
			lyrics.Remove (next1);
			lyrics.Remove (next2);
		} else if (lyrics.Count == 2) {
			next = lyrics [0];
			next1 = lyrics [1];
			lyrics.Remove (next);
			lyrics.Remove (next1);
		} else if (lyrics.Count == 0) {
			next = lyrics [0];
			lyrics.Remove (next);
		}

	}

	//打开歌词文件
	public void ReadFile ()
	{
		lyrics.Clear ();
		FileInfo sr = new FileInfo (path);
		var reader = sr.OpenText ();
		string str;

		while ((str = reader.ReadLine ()) != null) {
			Debugger.Log (str);
			if (string.IsNullOrEmpty (str))
				continue;
			Match empty = Regex.Match (str, regexEmpty);
			int minute = 0;
			int second = 0;
			int milSecond = 0;
			if (empty.Success) {
				
				Debugger.Log ("[Success] " + empty.Groups [0] + "  " + empty.Groups [1] + "  " + empty.Groups [2] + "   " + empty.Groups [3] 
				);
				minute = int.Parse (empty.Groups [1].Value);
				second = int.Parse (empty.Groups [2].Value);
				milSecond = int.Parse (empty.Groups [3].Value);
				lyrics.Add (new LyricCell (PackTime (minute, second, milSecond), ""));
			} else {
				Match match = Regex.Match (str, regexString);
				if (match.Success) {
					Debugger.Log ("[Success] " + match.Groups [0] + "  " + match.Groups [1] + "  " + match.Groups [2] + "   " + match.Groups [3] +
					match.Groups [4]);
					minute = int.Parse (match.Groups [1].Value);
					second = int.Parse (match.Groups [2].Value);
					milSecond = int.Parse (match.Groups [3].Value);
					lyrics.Add (new LyricCell (PackTime (minute, second, milSecond), match.Groups [4].Value));
				} else
					Debugger.Log ("[Fail]");
			}

		}
	}

	float PackTime (int minite, int second, int milSecond)
	{
		return minite * 60 + second + milSecond / 100.0f;
	}

	void FixedUpdate ()
	{
		music = mp3.time;
		timesample = mp3.timeSamples;
		if (next == null)
			return;

		float diff = music - next.time;
		Debugger.Log (music + "    " + diff);
		if (diff >= 0 && diff <= tolerance) {
			current = next;
			MusicLrc.text = current.lyric;
			if (next1 != null) {
				Debugger.Log ("Next:" + next1.time);
				MusicLrc1.text = next1.lyric;
			} else
				return;
			next = next1;
			next1 = next2;

			if (lyrics.Count > 0) {
				next2 = lyrics [0];
				lyrics.Remove (next2);
			}
		}
	}

	void Update ()
	{
		
	}
}


