using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Run : MonoBehaviour {
    public Image chickHP;
    public Image progress;
    public Chick chick;
    public Text lyrics;
    public Text lyrics1;

	// Use this for initialization
	void Start () {
	    GameManager.Instance.uiManager.chickHpImg = chickHP;
	    GameManager.Instance.uiManager.musicProImg = progress;
	    GameManager.Instance.uiManager.chick = chick;
	    GameManager.Instance.uiManager.lyrics = lyrics;
	    GameManager.Instance.uiManager.lyrics1 = lyrics1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
