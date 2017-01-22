using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Over : MonoBehaviour {
    public Button replay;
    public Text score;

	// Use this for initialization
	void Start () {
        replay.onClick.AddListener(()=>GameManager.Instance.ExitState(EGameState.RUN));
	    score.text = GameModel.Instance.score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
