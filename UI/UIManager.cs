using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class UIManager : MonoBehaviour
{
	private GameObject currentUI;

	[Header ("Run State")]
	public Image chickHpImg;
	public Image musicProImg;
	public Chick chick;
	public Text lyrics;
	public Text lyrics1;

	public List<GameObject> UIPrefabs;

	public void GoToState (EGameState state)
	{
		currentUI = Instantiate (UIPrefabs [(int)state]);
		if (state == EGameState.RUN)
			currentUI.GetComponent<Run> ().Init ();
		currentUI.transform.SetParent (transform, false);
	}

	public void SetChickHp (float ratio)
	{
		if (chickHpImg != null) {
			chickHpImg.fillAmount = ratio;
		}
	}

	public void SetMusicProgress (float ratio)
	{
		if (musicProImg != null) {
			musicProImg.fillAmount = ratio;
		}
	}

	public void MoveChick (float value)
	{
		if (chick != null) {
			float y = MathUtility.GetScreenPositionByAudioPitch (
				          MathUtility.GetStepIndexByPitch (value, GameModel.Instance.PitchManager.max, GameModel.Instance.PitchManager.min,
					          GameModel.Instance.PitchManager.addon, GameModel.Instance.PitchManager.step),
				          GameModel.Instance.PitchManager.max, GameModel.Instance.PitchManager.min,
				          GameModel.Instance.PitchManager.height, GameModel.Instance.PitchManager.addon, GameModel.Instance.PitchManager.step);
			chick.MoveTo (y);
		}
	}

	public void Exit ()
	{
		if (currentUI != null) {
			Destroy (currentUI);
		}
	}
}
