using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public GameCache gameCache;
	public Chick chick;
	public UIManager uiManger;
	public PitchManager pitchManager;
	public LRC lyric;

    public List<IGameState> states;

    public IGameState currentState { get; private set; }
    private EGameState state;
    public EGameState CurrentState {
        set {
            state = value;
            currentState = states[(int)value];
            currentState.Enter();
        }
    }

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}
	// Use this for initialization
	void Start () {
	    CurrentState = EGameState.WELCOME;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnRecordEvent (float value)
	{
		chick.MoveTo (value * 10);
	}

	public void OnMusicEvent (float value)
	{
//		Debugger.Log (value);
		pitchManager.OnMusicEvent (value);
	}
}
