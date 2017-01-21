using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManipulator : MonoBehaviour {
    public GameObject bird;
    public float threshold;
    private float prevPitch;
    private Vector3 position = new Vector3(-2.5f, 0, 0);

	// Use this for initialization
	void Start () {
	}

    public void MoveBird(float value) {
//        float yVelocity = 10;
//        float newPosition = Mathf.SmoothDamp(transform.position.y, value / 100f - 5, ref yVelocity, 0.1f);
        position.y = Mathf.Lerp(position.y, value / 10 - 3, 0.3f);
//        position.y = newPosition;
        bird.transform.position = position;
        prevPitch = value;
    }
}
