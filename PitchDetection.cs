using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Assets.Scripts;

namespace PitchDetection {
    public class PitchDetection : MonoBehaviour {
        float[] history = new float[1000];

        string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        [Header("Debug")]
        public bool isDebuging;
        public string frequency = "detected frequency";
        public Color color;
        public Text guiText;
        public bool isMicrophone = false;
        private int samplerate;
        private AudioClip clip;

        Vector3 Plot(float[] data, int num, float x0, float y0, float w, float h, Color col, float thr) {
            GL.Begin(GL.LINES);
            GL.Color(col);
            float xs = w / num, ys = h;
            float px = 0, py = 0;
            for (int n = 1; n < num; n++) {
                float nx = x0 + n * xs, ny = y0 + data[n] * ys;
                if (n > 1 && data[n] > thr && data[n - 1] > thr) {
                    GL.Vertex3(px, py, 0);
                    GL.Vertex3(nx, ny, 0);
                }
                px = nx;
                py = ny;
            }
            GL.End();
            return new Vector3(x0 + w, py, 0);
        }

        void OnRenderObject() {
            if (!isDebuging) return;
            GL.Begin(GL.LINES);
            GL.Color(Color.white);
            GL.Vertex3(-5, 0, 0);
            GL.Vertex3(5, 0, 0);
            GL.End();

            for (int n = 1; n < history.Length; n++)
                history[n - 1] = history[n];
            history[history.Length - 1] = AudioUtility.GetNoteFromFreq(AudioUtility.AnalyzeSound(GetComponent<AudioSource>()));
            transform.position = Plot(history, history.Length, -45.0f, -1.0f, 50.0f, 0.01f, color, 0.1f);
            float freq = AudioUtility.AnalyzeSound(GetComponent<AudioSource>()), deviation = 0.0f;

            frequency = freq.ToString() + " Hz";

            if (guiText != null)
                guiText.text = "Detected frequency: " + frequency + "\nDetected note: " + AudioUtility.GetNoteFromFreq(freq);

        }
    }
}
