using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class GameRun : IGameState
{
	private EGameState currentState = EGameState.RUN;
	private EGameState next = EGameState.END;

	private GameObject soundManager;

	public void Enter ()
	{
		GameManager.Instance.uiManager.GoToState (currentState);
		soundManager = GameObject.Instantiate (GameModel.Instance.SoundGameObject);
		soundManager.GetComponent<SoundManager> ().recordEvent.AddListener (GameManager.Instance.OnRecordEvent);
		soundManager.GetComponent<SoundManager> ().Init ();
	}

	public void Run ()
	{
		return;
	}

	public void Exit ()
	{
		GameObject.Destroy (soundManager);
        GameManager.Instance.gameCache.Clear();
		GameManager.Instance.uiManager.Exit ();
		GameManager.Instance.CurrentState = next;
	}

	public void Exit (EGameState toState)
	{
        GameObject.Destroy(soundManager);
        GameManager.Instance.uiManager.Exit();
        GameManager.Instance.CurrentState = toState;
    }
}
