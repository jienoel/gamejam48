using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    internal static class AudioUtility {
        internal class Peak {
            public float amplitude;
            public int index;

            public Peak() {
                amplitude = 0f;
                index = -1;
            }

            public Peak(float _frequency, int _index) {
                amplitude = _frequency;
                index = _index;
            }
        }

        internal class AmpComparer : IComparer<Peak> {
            public int Compare(Peak a, Peak b) {
                return 0 - a.amplitude.CompareTo(b.amplitude);
            }
        }

        public static float[] spectrum = new float[1024];
        private static readonly List<Peak> peaks = new List<Peak>();

        public static float AnalyzeSound(AudioSource audioSource) {
            // get sound spectrum
            audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
            var maxV = 0f;
            for (var i = 0; i < 1024; i++) {
                // find max
                if (spectrum[i] > maxV && spectrum[i] > 0.001) {
                    peaks.Add(new Peak(spectrum[i], i));
                    if (peaks.Count > 5) {
                        // get the 5 peaks in the sample with the highest amplitudes
                        peaks.Sort(new AmpComparer()); // sort peak amplitudes from highest to lowest
                        //peaks.Remove (peaks [5]); // remove peak with the lowest amplitude
                    }
                }
            }
            var freqN = 0f;
            if (peaks.Count > 0) {
                //peaks.Sort (new IndexComparer ()); // sort indices in ascending order
                maxV = peaks[0].amplitude;
                var maxN = peaks[0].index;
                freqN = maxN; // pass the index to a float variable
                if (maxN > 0 && maxN < 1024 - 1) {
                    // interpolate index using neighbours
                    var dL = spectrum[maxN - 1]/spectrum[maxN];
                    var dR = spectrum[maxN + 1]/spectrum[maxN];
                    freqN += 0.5f*(dR*dR - dL*dL);
                }
            }
            peaks.Clear();
            return freqN*(AudioSettings.outputSampleRate/2f)/1024f; // convert index to frequency
        }

        static public int GetNoteFromFreq(float freq) {
            int noteIndex = 0, octave = 0;
            if (freq > 0.0f) {
                float noteval = 57.0f + 12.0f * Mathf.Log10(freq / 440.0f) / Mathf.Log10(2.0f);
                float f = Mathf.Floor(noteval + 0.5f);
                noteIndex = (int)f % 12;
                octave = (int)Mathf.Floor((noteval + 0.5f) / 12.0f);
            }
            return octave*12 + noteIndex;
        }
    }
}
