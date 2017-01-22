using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	internal static class AudioUtility
	{
		public static float[] spectrum = new float[1024];

		public static float AnalyzeSound (AudioSource audioSource)
		{
			// get sound spectrum
			audioSource.GetSpectrumData (spectrum, 0, FFTWindow.Hanning);
			var maxV = 0f;
			var maxIndex = 0;
			for (var i = 0; i < 1024; i++) {
				if (spectrum [i] > maxV && spectrum [i] > 0.01) {
					maxV = spectrum [i];
					maxIndex = i;
				}
			}
			float freqN = maxIndex;
			if (maxIndex > 0 && maxIndex < 1023) {
				var dL = spectrum [maxIndex - 1] / spectrum [maxIndex];
				var dR = spectrum [maxIndex + 1] / spectrum [maxIndex];
				freqN += 0.5f * (dR * dR - dL * dL);
			}
			return freqN * (AudioSettings.outputSampleRate / 2f) / 1024f; // convert index to frequency
		}

		static public int GetNoteFromFreq (float freq)
		{
			int noteIndex = 0, octave = 0;
			if (freq > 0.0f) {
				float noteval = 57.0f + 12.0f * Mathf.Log10 (freq / 440.0f) / Mathf.Log10 (2.0f);
				float f = Mathf.Floor (noteval + 0.5f);
				noteIndex = (int)f % 12;
				octave = (int)Mathf.Floor ((noteval + 0.5f) / 12.0f);
			}
			return octave * 12 + noteIndex;
		}
	}
}
