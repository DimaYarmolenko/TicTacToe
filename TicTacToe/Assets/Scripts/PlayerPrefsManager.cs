using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager {

    private static string DIFFICULTY_KEY = "difficulty";
    private static string WINS_KEY = "wins";
    private static string DRAWS_KEY = "draws";
    private static string LOSES_KEY = "loses";


    public static void SetDifficulty(int diff)
    {
        //validation check
        if (diff >= 1 && diff <= 3)
        {
            PlayerPrefs.SetInt(DIFFICULTY_KEY, diff);
        }
        else
        {
            Debug.Log("Difficulty value is out of range: " + diff.ToString());
        }
    }

    public static void SetWins(int wins)
    {
        PlayerPrefs.SetInt(WINS_KEY, wins);
    }

    public static void SetDraws(int draws)
    {
        PlayerPrefs.SetInt(DRAWS_KEY, draws);
    }

    public static void SetLoses(int loses)
    {
        PlayerPrefs.SetInt(LOSES_KEY, loses);
    }

    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DIFFICULTY_KEY);
    }

    public static int GetWins()
    {
        return PlayerPrefs.GetInt(WINS_KEY);
    }

    public static int GetDraws()
    {
        return PlayerPrefs.GetInt(DRAWS_KEY);
    }

    public static int GetLoses()
    {
        return PlayerPrefs.GetInt(LOSES_KEY);
    }
}
