using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SoundEvent : UnityEvent<float>
{
}

public class SoundManager : MonoBehaviour
{
	public AppleGenerator appleGenerator;
	public AudioSource recordAudioSource;
	public SoundEvent recordEvent;
	public AudioSource musicAudioSource;
	public SoundEvent musicEvent;
	private int samplerate;
	public LRC lyric;
	// Use this for initialization
	void Start ()
	{
		GameModel.Instance.SoundManager = this;
		samplerate = AudioSettings.outputSampleRate;
		recordAudioSource.clip = Microphone.Start (null, false, (int)musicAudioSource.clip.length, samplerate);
		while (!(Microphone.GetPosition (null) > 0)) {
		} // Wait until the recording has started

		recordAudioSource.Play ();
		musicAudioSource.Play ();
	}

	public void Init ()
	{
		appleGenerator.Init ();
		lyric.Init ();

	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			GameManager.Instance.currentState.Exit ();
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (recordEvent != null) {
			recordEvent.Invoke (AudioUtility.GetNoteFromFreq (AudioUtility.AnalyzeSound (recordAudioSource)));
		}
		if (musicEvent != null) {
			musicEvent.Invoke (AudioUtility.GetNoteFromFreq (AudioUtility.AnalyzeSound (musicAudioSource)));
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			SavWav.Save ("record", recordAudioSource.clip);
		}
	}
}
