using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text wins, draws, loses;

    // Use this for initialization
	void Start () {
        UpdateScores();
    }

    //result textbox update
    public void UpdateScores()
    {
        wins.text = PlayerPrefsManager.GetWins().ToString();
        draws.text = PlayerPrefsManager.GetDraws().ToString();
        loses.text = PlayerPrefsManager.GetLoses().ToString();
    }

    //add 1 to win/draw/lose prefs value
    public void AddResult(GameManager.State state)
    {
        switch (state)
        {
            case GameManager.State.PlayerWin:

                PlayerPrefsManager.SetWins(PlayerPrefsManager.GetWins() + 1);
                break;

            case GameManager.State.AIWin:

                PlayerPrefsManager.SetLoses(PlayerPrefsManager.GetLoses() + 1);
                break;

            case GameManager.State.Draw:

                PlayerPrefsManager.SetDraws(PlayerPrefsManager.GetDraws() + 1);
                break;

            default:
                break;
        }
    }
}
