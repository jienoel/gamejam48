using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private List<GameObject> currentUI;

    public Image chickHpImg;
	public Image musicProImg;

    [Header("Game Start UI")] public GameObject welcome;


    public void GoToState(EGameState state) {
        GameObject obj = Instantiate(welcome);
        currentUI.Add(obj);
    }


	public void SetChickHp (float ratio)
	{
		chickHpImg.fillAmount = ratio;
	}

	public void SetMusicProgress (float ratio)
	{
		musicProImg.fillAmount = ratio;
	}
}
