using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class AudioTest : MonoBehaviour
{
	public AudioSource source;

	public float max;
	public float min;

	void Update ()
	{
		max = Mathf.Max (source.pitch, max);
		min = Mathf.Min (source.pitch, min);
		Debugger.Log (source.pitch);
	}
}
