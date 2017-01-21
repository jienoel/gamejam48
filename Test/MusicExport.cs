using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts;
using UnityEngine;

public class MusicExport : MonoBehaviour {
    private FileStream fs;
    private StreamWriter sw;
    private float time = 0;

    void Start() {
        fs = new FileStream("export.txt", FileMode.Create);
        sw = new StreamWriter(fs);
    }

	// Update is called once per frame
	void FixedUpdate () {
		sw.WriteLine(String.Format("{0}:{1}", time, AudioUtility.GetNoteFromFreq(AudioUtility.AnalyzeSound(GetComponent<AudioSource>()))));
        sw.Flush();
	    time += Time.fixedDeltaTime;
	}

    void OnDestroy() {
        sw.Close();
        fs.Close();
    }
}
