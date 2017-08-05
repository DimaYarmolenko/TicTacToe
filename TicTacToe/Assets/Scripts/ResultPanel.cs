using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour {

    private Text resultText;
    private CanvasGroup canvasGroup;

    void Start () {
        resultText = GetComponentInChildren<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
	}

    //result panel pop-up
    public void ActivatePanel(string message)
    {
        resultText.text = message;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    //result panel removal
    public void DeactivatePanel()
    {
        resultText.text = "";

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
