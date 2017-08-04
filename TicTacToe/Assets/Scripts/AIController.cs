using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public Tile[] tiles;

    private GameManager gm;
    private int difficulty;
    private GameManager.Pieces piece;

    private List<int> strat = new List<int>();
	
    
    // Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
        difficulty = PlayerPrefsManager.GetDifficulty();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MakeTurn()
    {
        switch (difficulty)
        {
            case 1:
                DoAny();
                break;

            case 2:
                DoUsingStrat();
                break;

            case 3:
                Test();
                break;

            default:
                break;
        }
    }

    private void DoAny()
    {
        List<Tile> availableTiles = new List<Tile>();

        foreach (Tile tile in tiles)
        {
            if (tile.IsAvailable())
            {
                availableTiles.Add(tile);
            }
        }

        if (availableTiles.Count != 0)
        {
            System.Random rnd = new System.Random();
            availableTiles[rnd.Next(0, availableTiles.Count)].TakeTile();
        }
    }

    private void DoUsingStrat()
    {
        if (strat.Count == 0)
        {
            PickStrat();
        }

        for (int i = 0; i < strat.Count; i++)
        {
            if (tiles[strat[i]].IsAvailable())
            {
                tiles[strat[i]].TakeTile();
                return;
            }
        }

        DoAny();
        strat.Clear();
    }

    private void BlockingMove()
    {
        for (int i = 0; i < 8; i++)
        {

        }
    }

    private void Test()
    {
        tiles[0].TakeTile();
        tiles[3].TakeTile();
        tiles[6].TakeTile();

        foreach (Tile tile in tiles)
        {
            Debug.Log(tile.id);
        }
    }

    private void PickStrat()
    {
        System.Random rnd = new System.Random();

        int i = rnd.Next(0,9);

        strat.Add(gm.winners[i, 0]);
        strat.Add(gm.winners[i, 1]);
        strat.Add(gm.winners[i, 2]);
    }
}
