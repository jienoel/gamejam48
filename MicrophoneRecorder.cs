using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (AudioSource))]
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

internal class IndexComparer : IComparer<Peak> {
    public int Compare(Peak a, Peak b) {
        return a.index.CompareTo(b.index);
    }
}

public class MicrophoneRecorder : MonoBehaviour {
    public event Action<float> onMicrophone;
    public float rmsValue;
    public float dbValue;
    public float pitchValue;

    public int qSamples = 1024;

    public int binSize = 1024;
    // you can change this up, I originally used 8192 for better resolution, but I stuck with 1024 because it was slow-performing on the phone

    public float refValue = 0.1f;
    public float threshold = 0.01f;

    public bool isMicrophone = false;

    private readonly List<Peak> peaks = new List<Peak>();
    private float[] samples;
    private float[] spectrum;
    private int samplerate;

    public Text display; // drag a Text object here to display values
    public bool mute = true;


    private void Start() {
        samples = new float[qSamples];
        spectrum = new float[binSize];
        samplerate = AudioSettings.outputSampleRate;

        // starts the Microphone and attaches it to the AudioSource
        if (isMicrophone) {
            GetComponent<AudioSource>().clip = Microphone.Start(null, true, 10, samplerate);
            GetComponent<AudioSource>().loop = true; // Set the AudioClip to loop
            while (!(Microphone.GetPosition(null) > 0)) { } // Wait until the recording has started
        }
        GetComponent<AudioSource>().Play();

        // Mutes the mixer. You have to expose the Volume element of your mixer for this to work. I named mine "masterVolume".
    }

    private void Update() {
        AnalyzeSound();
        onMicrophone.Invoke(pitchValue);
        if (display != null) {
//            display.text = "RMS: " + rmsValue.ToString("F2") +
//                           " (" + dbValue.ToString("F1") + " dB)\n" +
//                           "Pitch: " + pitchValue.ToString("F0") + " Hz";
        }
    }

//    private void AnalyzeSound() {
//        GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
//        var low = 500/(samplerate/(float)binSize);
//        var high = 2000/(samplerate/ (float)binSize);
//        var average = 0f;
//        var count = 0;
//        for (int i = Mathf.FloorToInt(low); i <= Mathf.CeilToInt(high); i++) {
//            average += spectrum[i];
//            count++;
//        }
//        average /= count;
//        pitchValue = average;
//    }

    private void AnalyzeSound() {
        var samples = new float[qSamples];
        GetComponent<AudioSource>().GetOutputData(samples, 0); // fill array with samples
        var i = 0;
        var sum = 0f;
        for (i = 0; i < qSamples; i++) {
            sum += samples[i]*samples[i]; // sum squared samples
        }
        rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
        dbValue = 20*Mathf.Log10(rmsValue/refValue); // calculate dB
        if (dbValue < -160) {
            dbValue = -160; // clamp it to -160dB min
        }

        // get sound spectrum
        GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
        var maxV = 0f;
        for (i = 0; i < binSize; i++) {
            // find max
            if (spectrum[i] > maxV && spectrum[i] > threshold) {
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
            if (maxN > 0 && maxN < binSize - 1) {
                // interpolate index using neighbours
                var dL = spectrum[maxN - 1]/spectrum[maxN];
                var dR = spectrum[maxN + 1]/spectrum[maxN];
                freqN += 0.5f*(dR*dR - dL*dL);
            }
        }
        pitchValue = freqN*(samplerate/2f)/binSize; // convert index to frequency
        peaks.Clear();
    }
}