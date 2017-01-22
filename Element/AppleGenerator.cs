using UnityEngine;
using System;
using System.Collections;
using System.Net;
using Assets.Scripts;
using UnityEngine.UI;

public enum Difficulty
{
	EASY = 5,
	NORMAL = 10,
	HARD = 15}
;

public class AppleGenerator : MonoBehaviour
{
	private System.Random heightRandom;
	private System.Random timeRandom;
	private System.Random appleRandom;

	public AudioSource music;
	public float appleMargin = 1;
	public int trapCount = 50;
	public Difficulty difficulty;
	public Action<int, bool> onAppleGenerated;
	public float badApple;

	public Text text;
	public float posi = 0.95f;
	private bool isTrap = false;

	void Start ()
	{
//		Init ();
	}

	public void Init (int seed = 0)
	{
		heightRandom = new System.Random (seed);
		timeRandom = new System.Random (seed);
		appleRandom = new System.Random (seed);
        
		Play ();
	}

	public void Play ()
	{
//		StartCoroutine (GenerateTrap ());
//        StartCoroutine(PlaceApple());

	}

	//	float sum;


	//	float timeStep = Clipping.length / trapCount.count;
	//	float deltaTime = (float)(timeRandom.NextDouble () - 0.5) + timeStep;
	//	sum += deltaTime;

	//	IEnumerator GenerateTrap ()
	//	{
	//		var timeStep = music.clip.length / trapCount;
	//		while (true) {
	//			yield return new WaitForSecondsRealtime ((float)(timeRandom.NextDouble () - 0.5) + timeStep);
	//			isTrap = true;
	//		}
	//	}

	bool IsTrap (float time)
	{
//		float possibility = Mathf.Clamp (1 / (time * 2 - Mathf.FloorToInt (time * 2) + 0.01f), 0, 1);
//		Debugger.Log (time);
//		return (timeRandom.NextDouble () - possibility) > 0;
		return (timeRandom.NextDouble () - posi) > 0;
	}

	void SetPlaceApple (float y, float x, DamageType damageType, AppleSize appleSize)
	{
		Apple apple = GameManager.Instance.gameCache.GetApple (appleSize);
		Vector3 pos = apple.rect.localPosition;
		pos.x = x;
		pos.y = y;
		apple.rect.localPosition = pos;
		apple.speed = GameModel.Instance.PitchManager.speed + GameModel.Instance.PitchManager.deltaSpeed;
		apple.moving = true;
		apple.gameObject.SetActive (true);

	}

	public void PlaceApple (int value, float posX, float time)
	{
		int retValue = 0;
		bool isBad = false;
//		while (true) {
		isBad = false;
		retValue = value;
//		Debugger.Log (retValue);
		if (IsTrap (time)) {
//			Debugger.LogError ("Apple");
			var delta = (int)((heightRandom.NextDouble () - 0.5) * (int)difficulty);
			if (appleRandom.NextDouble () < badApple) {
				delta = -Mathf.Abs (delta);
				isBad = true;
			}
			retValue += delta;
//			Debugger.LogError (retValue);
			retValue = Mathf.Clamp (retValue, 0, 6);
//			isTrap = false;
			float y = MathUtility.GetScreenPositionByAudioPitch (retValue,
					        
				          GameModel.Instance.PitchManager.max, GameModel.Instance.PitchManager.min,
				          GameModel.Instance.PitchManager.height, GameModel.Instance.PitchManager.addon, GameModel.Instance.PitchManager.step);
			SetPlaceApple (y + 60, posX, isBad ? DamageType.Damage : DamageType.Heal, AppleSize.Big);
//				if (onAppleGenerated != null) {
//					onAppleGenerated.Invoke (retValue, isBad);
//				}
		} //else {
//		float y1 = MathUtility.GetScreenPositionByAudioPitch (retValue,
//
//			            GameModel.Instance.PitchManager.max, GameModel.Instance.PitchManager.min,
//			            GameModel.Instance.PitchManager.height, GameModel.Instance.PitchManager.addon, GameModel.Instance.PitchManager.step);
//		SetPlaceApple (y1 + 60, posX, DamageType.Heal, AppleSize.Small);
		//}
//			text.text = retValue.ToString ();
//			yield return new WaitForSecondsRealtime (appleMargin);
//		}
	}

}
