using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private GameObject currentUI;

    [Header("Run State")]
    public Image chickHpImg;
	public Image musicProImg;
    public Chick chick;
    public Text lyrics;
    public Text lyrics1;

    public List<GameObject> UIPrefabs;

    public void GoToState(EGameState state) {
        currentUI = Instantiate(UIPrefabs[(int)state]);
        currentUI.transform.SetParent(transform, false);
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

    public void MoveChick(float value) {
        if (chick != null) {
            chick.MoveTo(value);
        }
    }

    public void Exit() {
        if (currentUI != null) {
            Destroy(currentUI);
        }
    }
}
