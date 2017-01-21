using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class AudioTest : MonoBehaviour
{
	public AudioSource recordAudioSource;

	public float max;
	public float min;

	void Start ()
	{
        recordAudioSource.clip = Microphone.Start(null, false, 10, 44100);
        while (!(Microphone.GetPosition(null) > 0)) {
        } // Wait until the recording has started
        recordAudioSource.Play();
    }
}
