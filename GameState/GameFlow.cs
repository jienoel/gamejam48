using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameFlow : MonoBehaviour
{
	public const int GAMESTART = 1;
	public const int GAMEEND = 2;

	public Dictionary<int,IGameState> states = new Dictionary<int, IGameState> ();

	

}
