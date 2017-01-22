using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Player : MonoBehaviour {
    public int hp = 100;

	// Use this for initialization
	void Start () {
	    GameModel.Instance.player = this;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
