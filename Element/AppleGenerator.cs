using UnityEngine;
using System;
using System.Collections;
using System.Net;
using Assets.Scripts;
using UnityEngine.UI;

public enum Difficulty {
    EASY = 2,
    NORMAL = 4,
    HARD = 6
};

public class AppleGenerator : MonoBehaviour {
    private System.Random heightRandom;
    private System.Random timeRandom;
    private System.Random appleRandom;

    public AudioSource music;
    public float appleMargin = 1;
    public int trapCount = 50;
    public Difficulty difficulty;
    public Action<int, bool> onAppleGenerated;
    public float badApple;

    public Text text;

    private bool isTrap = false;

    void Start() {
        Init();
    }

    public void Init(int seed = 0) {
        heightRandom = new System.Random(seed);
        timeRandom = new System.Random(seed);
        appleRandom = new System.Random(seed);
        
        Play();
    }

    public void Play() {
        StartCoroutine(GenerateTrap());
        StartCoroutine(PlaceApple());
    }

    IEnumerator GenerateTrap() {
        var timeStep = music.clip.length/trapCount;
        while (true) {
            yield return new WaitForSecondsRealtime((float)(timeRandom.NextDouble() - 0.5) + timeStep);
            isTrap = true;
        }
    }

    IEnumerator PlaceApple() {
        int retValue = 0;
        bool isBad = false;
        while (true) {
            isBad = false;
            retValue = AudioUtility.GetNoteFromFreq(AudioUtility.AnalyzeSound(music));
            if (isTrap) {
                var delta = (int) ((heightRandom.NextDouble() - 0.5)*(int) difficulty);
                if (appleRandom.NextDouble() < badApple) {
                    delta = -Mathf.Abs(delta);
                    isBad = true;
                }
                retValue += delta;
                isTrap = false;
                if (onAppleGenerated != null) {
                    onAppleGenerated.Invoke(retValue, isBad);
                }
            }
            text.text = retValue.ToString();
            yield return new WaitForSecondsRealtime(appleMargin);
        }
    }

}
