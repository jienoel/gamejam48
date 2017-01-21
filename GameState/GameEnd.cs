using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : IGameState
{

	int next;

	public void Enter ()
	{

	}

	public int Run ()
	{
		return next;
	}

	public void Exit ()
	{

	}
}
