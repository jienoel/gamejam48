﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;


	public GameCache gameCache;
	public Chick chick;
	public UIManager uiManger;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
