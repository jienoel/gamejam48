using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugText : MonoBehaviour
{
	public bool test;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (test)
			Debug ();
	}

	void Debug ()
	{
		Debugger.Log ("1");
	}
}
