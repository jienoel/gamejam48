using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public Image chickHpImg;
	public Image musicProImg;


	public void SetChickHp (float ratio)
	{
		chickHpImg.fillAmount = ratio;
	}

	public void SetMusicProgress (float ratio)
	{
		musicProImg.fillAmount = ratio;
	}
}
