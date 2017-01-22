using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TipFade : MonoBehaviour {
    private Image image;
    private Color color;

    void Start() {
        image = GetComponent<Image>();
        color = image.color;
    }

	// Update is called once per frame
	void Update () {
	    color.a = Mathf.PingPong(Time.time, 1);
	    image.color = color;
	}
}
