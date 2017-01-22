using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Run : MonoBehaviour
{
	public Image chickHP;
	public Image progress;
	public Chick chick;
	public Text lyrics;
	public Text lyrics1;
	public Transform appleParent;
	public Transform pitchParent;
    public Button pause;

	// Use this for initialization
	void Start ()
	{
	}

	public void Init ()
	{
		GameManager.Instance.uiManager.chickHpImg = chickHP;
		GameManager.Instance.uiManager.musicProImg = progress;
		GameManager.Instance.uiManager.chick = chick;
		GameManager.Instance.uiManager.lyrics = lyrics;
		GameManager.Instance.uiManager.lyrics1 = lyrics1;
		GameManager.Instance.gameCache.appleParent = appleParent;
		GameManager.Instance.gameCache.pitchParent = pitchParent;
        pause.onClick.AddListener(() => GameManager.Instance.ExitState(EGameState.WELCOME));
    }

    // Update is called once per frame
    void Update ()
	{
		
	}
}
