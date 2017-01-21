using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public static class MathUtility
{
	public const int A = 0;
	public const int B = 1;
	public const int C = 2;
	public const int D = 3;
	public const int E = 4;
	public const int F = 5;

	public static float PitchStepWidth (float speed, float deltaPitchStep)
	{
		return speed * deltaPitchStep;
	}

	public static float PitchShowTime (float windowSize, float speed, float hitTime)
	{
		return  hitTime - windowSize / speed;
	}

	public static int GetStepIndexByPitch (float pitch, float max, float min, float addon = 0.25f, int step = 6)
	{
		return (int)(pitch / ((max + addon * (max - min)) / step));
	}

	public static int GetStepIndexByScreenPosition (float positionY, float height = 1044, int step = 6)
	{
		return (int)(positionY / (height / step));
	}

	public static float GetScreenPositionByAudioPitch (float pitchIndex, float max, float min, float height = 1044, float addon = 0.25f, int step = 6)
	{
//		int index = (int)((max + addon * (max - min)) / step);
//		if (index > step) {
//			Debug.LogError ("Error");
//		}
//		return index * height / step;
		float delta = height / step;
		return pitchIndex * delta;
	}

	public static float[] GetAudioPitchToStepIndexTable (float max, float min, int step = 6)
	{
		float[] indexes = new float[step];
		float delta = (max - min) / step;
		for (int i = 0; i < step; i++) {
			indexes [i] = delta * i;
		}
		return indexes;
	}

	public static float[] GetScreenPositionToStepIndexTable (float height = 1044, int step = 6)
	{
		float[] indexes = new float[step];
		float delta = height / step;
		for (int i = 0; i < step; i++) {
			indexes [i] = delta * i;
		}
		return indexes;
	}
}
