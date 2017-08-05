using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour {

    private CanvasGroup canvas;

    private void Start()
    {
        canvas = FindObjectOfType<CanvasGroup>();
    }

    //difficulty panel pop-up
    public void Activate()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public void SetDifficulty(int diff)
    {
        PlayerPrefsManager.SetDifficulty(diff);
    }
}
