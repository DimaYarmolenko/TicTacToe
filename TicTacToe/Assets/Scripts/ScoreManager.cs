using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text wins, draws, loses;

    // Use this for initialization
	void Start () {
        wins.text = PlayerPrefsManager.GetWins().ToString();
        draws.text = PlayerPrefsManager.GetDraws().ToString();
        loses.text = PlayerPrefsManager.GetLoses().ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
