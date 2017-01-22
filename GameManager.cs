using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public GameCache gameCache;
	public UIManager uiManager;

	public List<IGameState> states;

	public IGameState currentState { get; private set; }

	private EGameState state;

	public EGameState CurrentState {
		set {
			state = value;
			currentState = states [(int)value];
			currentState.Enter ();
			currentState.Run ();
		}
	}

	public void ExitState ()
	{
		currentState.Exit ();
	}

	public void ExitState (EGameState toState)
	{
		currentState.Exit (toState);
	}

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}
	// Use this for initialization

	void Start ()
	{
		states = new List<IGameState> () { new GameStart (), new GameRun (), new GameEnd () };
		CurrentState = EGameState.WELCOME;
		if (MusicExport.Instance != null)
			MusicExport.Instance.Init ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnRecordEvent (float value)
	{
		uiManager.MoveChick (value * 10);
	}

	public void OnMusicEvent (float value)
	{
//		Debugger.Log (value);
		if (GameModel.Instance.PitchManager != null) {
			GameModel.Instance.PitchManager.OnMusicEvent (value);
		}
	}
}
