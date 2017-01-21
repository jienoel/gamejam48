using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
	void Enter ();

	int Run ();

	void Exit ();


}
