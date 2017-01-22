using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts;
using UnityEngine;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class MusicExport : MonoBehaviour
{
	private FileStream fs;
	private StreamWriter sw;
	private float time = 0;
	string regexString = @"^\[(\d{2}):(\d{2}).(\d{2})\](.+)$";
	string regexEmpty = @"^\[(\d{2}):(\d{2}).(\d{2})\]$";
	public List<LyricCell> lyrics = new List<LyricCell> ();
	public List<DoubleFloat> efficient = new List<DoubleFloat> ();
	string path;
	public DoubleFloat current;
	AudioSource mp3;
	public bool play;
	public static MusicExport Instance;

	void Awake ()
	{
		Instance = this;
	}

	void Start ()
	{
		
//		efficient.Remove (current);
	}

	public void Init ()
	{
		mp3 = GetComponent<AudioSource> ();
		fs = new FileStream (Application.streamingAssetsPath + "/Exp/" + mp3.clip.name + "1.txt", FileMode.Create);
		sw = new StreamWriter (fs);
		path = Application.streamingAssetsPath + "/Trc/" + mp3.clip.name + ".lrc"; //获取歌词路径，并同步歌词和歌曲名称
		//		ReadFile ();
		if (efficient.Count > index)
			current = efficient [index++];
	}

	//打开歌词文件
	public void ReadFile ()
	{
		lyrics.Clear ();
		FileInfo sr = new FileInfo (path);
		var reader = sr.OpenText ();
		string str;
		float start = -1;
		float end = -1;

		while ((str = reader.ReadLine ()) != null) {
			Debugger.Log (str);
			if (string.IsNullOrEmpty (str))
				continue;
			Match empty = Regex.Match (str, regexEmpty);
			int minute = 0;
			int second = 0;
			int milSecond = 0;
			if (empty.Success) {

				//				Debugger.Log ("[Success] " + empty.Groups [0] + "  " + empty.Groups [1] + "  " + empty.Groups [2] + "   " + empty.Groups [3] );
				minute = int.Parse (empty.Groups [1].Value);
				second = int.Parse (empty.Groups [2].Value);
				milSecond = int.Parse (empty.Groups [3].Value);
				lyrics.Add (new LyricCell (PackTime (minute, second, milSecond), ""));
				if (start != -1) {
					end = PackTime (minute, second, milSecond);
					efficient.Add (new DoubleFloat (start, end));
					start = -1;
					end = -1;
				}

			} else {
				Match match = Regex.Match (str, regexString);
				if (match.Success) {
					//					Debugger.Log ("[Success] " + match.Groups [0] + "  " + match.Groups [1] + "  " + match.Groups [2] + "   " + match.Groups [3] +
					//					match.Groups [4]);
					minute = int.Parse (match.Groups [1].Value);
					second = int.Parse (match.Groups [2].Value);
					milSecond = int.Parse (match.Groups [3].Value);
					lyrics.Add (new LyricCell (PackTime (minute, second, milSecond), match.Groups [4].Value));
					if (start == -1) {
						start = PackTime (minute, second, milSecond);
					} else if (end != -1) {
						Debug.LogError ("Lyric Error!");
					}
				} //else
				//					Debugger.Log ("[Fail]");
			}

		}
	
	}

	void Update ()
	{
		if (play) {
			mp3.Play ();
			play = false;
		}

	}

	public void Export1 (string line)
	{
		
		sw.WriteLine (line);
	}

	float PackTime (int minite, int second, int milSecond)
	{
		return minite * 60 + second + milSecond / 100.0f;
	}

	public int index;
	// Update is called once per frame
	void FixedUpdate ()
	{
//		sw.WriteLine (String.Format ("{0}:{1}", mp3.time, AudioUtility.GetNoteFromFreq (AudioUtility.AnalyzeSound (mp3))));
		return;
		if (current.time == 0 || current.pitch == 0)
			return;
		if (mp3.time >= current.time && mp3.time <= current.pitch) {
			sw.WriteLine (String.Format ("{0}:{1}", mp3.time, AudioUtility.GetNoteFromFreq (AudioUtility.AnalyzeSound (mp3))));
			sw.Flush ();
		} else if (mp3.time > current.pitch) {
			if (efficient.Count > index) {
				
				current = efficient [index++];
//				efficient.Remove (current);
				if (mp3.time >= current.time && mp3.time <= current.pitch) {
					sw.WriteLine (String.Format ("{0}:{1}", mp3.time, AudioUtility.GetNoteFromFreq (AudioUtility.AnalyzeSound (mp3))));
					sw.Flush ();
				}
			}
			
//		time += Time.fixedDeltaTime;
		}
	}

	void OnDestroy ()
	{
		if (sw != null) {
			sw.Close ();
		}
		if (fs != null)
			fs.Close ();
	}
}
