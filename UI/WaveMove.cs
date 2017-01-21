using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMove : MonoBehaviour {
    public float moveVelosity;
    private RectTransform rt;
    private Vector3 anchor;

	// Use this for initialization
	void Start () {
	    rt = GetComponent<RectTransform>();
	    anchor = rt.transform.localPosition;
        Debug.Log(anchor);
	}
	
	// Update is called once per frame
	void Update () {
	    anchor.x -= moveVelosity*Time.deltaTime;
	    rt.transform.localPosition = anchor;
	    if (anchor.x < -1215 - 375) {
	        anchor.x += 1215 * 2;
	    }
	}
}
