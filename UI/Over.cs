using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Over : MonoBehaviour {
    public Button replay; 

	// Use this for initialization
	void Start () {
        replay.onClick.AddListener(()=>GameManager.Instance.ExitState(EGameState.RUN));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
