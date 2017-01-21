using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState {
    WELCOME,
    RUN,
    END
}

public interface IGameState
{
	void Enter ();

	int Run ();

	void Exit ();


}
