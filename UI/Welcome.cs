using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Welcome : MonoBehaviour {
    public Button button;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(GameManager.Instance.ExitState);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
