using System;
using System.IO;
using System.Text;
using UnityEngine;

public class TextToFile
{
	public static  string FILE_PATH = @"..\Game\";
	public static  string FILE_PATH_Discrete = @"..\Game\EffectLog\EffectsLog";

	static string path = "";
	public static StreamWriter sr = null;
	static string timestamp = "";

	public static string fileNameFormat = "Log_{0}_{1}.txt";
	public const string editorFolderName = "/../Log/";
	public const string mobileFolderName = "/Log/";
	public static string editorPath = Application.dataPath;
	public static string mobilePath = Application.persistentDataPath;

	public static void Reset (TextType debugLog = TextType.None, string fileName = "")
	{
		if (sr != null)
			return;
		if (debugLog == TextType.Discrete) {
			timestamp = System.DateTime.Now.Day.ToString () + "_" + System.DateTime.Now.Hour.ToString ()
			+ "_" + System.DateTime.Now.Minute.ToString () + "_" + System.DateTime.Now.Second.ToString ();
			string folderPath;
			#if UNITY_EDITOR
			folderPath = editorPath + editorFolderName;
			#else
			folderPath = mobilePath + mobileFolderName;
			#endif
			path = folderPath + string.Format (fileNameFormat, fileName, timestamp);
			if (!Directory.Exists (folderPath)) {
				Directory.CreateDirectory (folderPath);
			}
			if (!File.Exists (path)) {
				sr = File.CreateText (path); 
			} else {
				sr = File.AppendText (path);
			}
		}	

	}

	public static void CloseDebug ()
	{
		if (sr != null)
			sr.Close ();
		sr = null;
	}


	public static void WriteToFile (string text, string path)
	{
		if (!File.Exists (path)) {
			Debugger.Log ("create " + path, LogColor.blueLog);
			StreamWriter sw = File.CreateText (path);
			sw.WriteLine (text);
			sw.Close (); 
		} else {
			using (StreamWriter sw = File.AppendText (path)) {
				sw.WriteLine (text);
			}
		}   
	}


	public static void WriteToFile (string text, TextType debugLog = TextType.None)
	{
		if (sr == null) {
			Reset (debugLog);
		}
		if (debugLog == TextType.Discrete) {
			sr.WriteLine (text);
		} 
	}
}