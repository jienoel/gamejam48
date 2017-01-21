using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : IGameState
{
    EGameState current = EGameState.WELCOME;
	int next;

	public void Enter ()
	{
		GameManager.Instance.uiManger.GoToState(current);
	}

	public int Run ()
	{
		return next;
	}

	public void Exit ()
	{
		
	}
}
