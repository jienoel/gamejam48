using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : IGameState
{
    private EGameState currentState = EGameState.END;
    private EGameState next = EGameState.END;

    public void Enter ()
	{
        GameManager.Instance.uiManager.GoToState(currentState);
	}

	public void Run ()
	{
	}

	public void Exit ()
	{
        GameManager.Instance.uiManager.Exit();
    }

    public void Exit(EGameState toState) {
        GameManager.Instance.uiManager.Exit();
        GameManager.Instance.CurrentState = toState;
    }
}
