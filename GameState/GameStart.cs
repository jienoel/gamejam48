using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : IGameState
{
    private EGameState current = EGameState.WELCOME;
    private EGameState next = EGameState.RUN;

	public void Enter ()
	{
		GameManager.Instance.uiManager.GoToState(current);
	}

	public void Run ()
	{
		return ;
	}

	public void Exit () {
	    GameManager.Instance.uiManager.Exit();

        GameManager.Instance.CurrentState = next;
	}

    public void Exit(EGameState toState) {
        GameManager.Instance.uiManager.Exit();

        GameManager.Instance.CurrentState = toState;
    }
}
