using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManipulator : MonoBehaviour {
    public GameObject bird;
    private Vector3 position = new Vector3(-2.5f, 0, 0);

	// Use this for initialization
	void Start () {
	    GetComponent<MicrophoneRecorder>().onMicrophone += MoveBird;
	}

    private void MoveBird(float value) {
        float yVelocity = 0;
        float newPosition = Mathf.SmoothDamp(transform.position.y, value / 100 - 5, ref yVelocity, 0.3f);
        position.y = Mathf.Lerp(position.y, value / 100 - 5, 0.3f);
//        position.y = value /100 - 5;
        bird.transform.position = position;
    }
}
