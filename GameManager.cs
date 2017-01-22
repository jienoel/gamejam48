using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public GameCache gameCache;
	public Chick chick;
	public UIManager uiManger;
	public PitchManager pitchManager;
	public LRC lyric;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}
	// Use this for initialization
	void Start ()
	{
		MusicExport.Instance.Init ();
		pitchManager.Init ();
		lyric.Init ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnRecordEvent (float value)
	{
		chick.MoveTo (value * 10);
	}

	public void OnMusicEvent (float value)
	{
//		Debugger.Log (value);
		pitchManager.OnMusicEvent (value);
	}
}
