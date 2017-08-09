using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    //set of gametiles in scene
    public Tile[] tiles;

    private GameManager gm;
    //playerprefs difficulty
    private int difficulty;
    //what pieces AI have to play
    private GameManager.Pieces piece;

    //strategy to follow
    private List<int> strat = new List<int>();
	
    
    // Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
        difficulty = PlayerPrefsManager.GetDifficulty();
	}

    public void MakeTurn()
    {
        //type of turn depending on difficulty
        switch (difficulty)
        {
            case 1:
                DoAny();
                break;

            case 2:
                DoUsingStrat();
                break;

            case 3:
                BlockingMove();
                break;

            default:
                break;
        }
    }

    //random move on any available tile
    private void DoAny()
    {
        List<Tile> availableTiles = new List<Tile>();

        //loop through tiles to find available ones
        foreach (Tile tile in tiles)
        {
            if (tile.IsAvailable())
            {
                availableTiles.Add(tile);
            }
        }

        System.Random rnd = new System.Random();
        //taking random available tile
        availableTiles[rnd.Next(0, availableTiles.Count)].TakeTile();
    }

    //random winning strat, if not available choosing new strat
    private void DoUsingStrat()
    {
        if (strat.Count == 0)
        {
            PickStrat();
        }

        //picing first available position int chosen strat
        for (int i = 0; i < strat.Count; i++)
        {
            if (tiles[strat[i]].IsAvailable())
            {
                tiles[strat[i]].TakeTile();
                return;
            }
        }

        //pick a random tile if chosen strat has got no available tiles
        DoAny();
        //clear current strat to find new next turn
        strat.Clear();
    }

    //tries to block  the most dangerous players strat, not quite impossible though
    private void BlockingMove()
    {
        FindDangerStrat();

        //loop through chosen dangerous player's strat to take available tile
        foreach (int item in strat)
        {
            if (tiles[item].IsAvailable())
            {
                tiles[item].TakeTile();
                strat.Clear();
                return;
            }
        }

        //take random available tile if draw is the only possible outcome
        DoAny();
    }

    //picking random strat from GameManager.winners[]
    private void PickStrat()
    {
        System.Random rnd = new System.Random();

        int i = rnd.Next(0,gm.winners.Count);

        for (int j = 0; j < gm.winners[i].Length; j++)
        {
            strat.Add(gm.winners[i][j]);
        }
    }

    //picking strat from GameManager.winners[] using index parameter
    private void PickStrat(int index)
    {
        for (int j = 0; j < gm.winners[index].Length; j++)
        {
            strat.Add(gm.winners[index][j]);
        }
    }

    //finds strat in GameManager.winners[] that has the most occupied tiles by player
    private void FindDangerStrat()
    {
        //index of the result strat
        int dangerIndex = 0, winnerIndex = 0;
        //occupied tiles by player in result strat
        int dangerValue = 0, winnerValue = 0;

        //GameManager.winners[] loop
        for (int i = 0; i < gm.winners.Count; i++)
        {
            //occupied tiles in strat counters
            int opponentTiles = 0, friendlyTiles = 0;
            //1 for cross; 2 for zero
            int opponentValue = 0, friendValue = 0;

            if (gm.GetPieces() == GameManager.Pieces.Cross)
            {
                opponentValue = 2;
                friendValue = 1;
            }
            else
            {
                opponentValue = 1;
                friendValue = 2;
            }
            
            //sum up tiles entries both for ai and player
            for (int j = 0; j < gm.winners[i].Length; j++)
            {
                if (gm.GetTiles()[gm.winners[i][j]] == opponentValue)
                {
                    opponentTiles++;
                }
                else if (gm.GetTiles()[gm.winners[i][j]] == friendValue)
                {
                    friendlyTiles++;
                }
            }

            //if strat has got player tile and has got no ai tiles, no reason to block already blocked strat
            if (opponentTiles >= 1 && friendlyTiles == 0)
            {
                //if current strat >= dangerous than last most dangerous strat
                if (opponentTiles >= dangerValue)
                {
                    dangerValue = opponentTiles;
                    dangerIndex = i;
                }
            }

            //try to find almost completed strat to win
            //if strat has got AI tile/tiles and has got no player tiles
            if (friendlyTiles >= 1 && opponentTiles == 0)
            {
                //if current strat >= successfull than last most successfull strat
                if (friendlyTiles >= winnerValue)
                {
                    winnerValue = friendlyTiles;
                    winnerIndex = i;
                }
            }
        }

        //decide to block or to try to win
        if (winnerValue >= dangerValue)
        {
            Debug.Log("win strat, winValue: " + winnerValue.ToString() + " || dangerValue: " + dangerValue.ToString());
            PickStrat(winnerIndex);
        }
        else
        {
            Debug.Log("block strat, winValue: " + winnerValue.ToString() + " || dangerValue: " + dangerValue.ToString());
            PickStrat(dangerIndex);
        }
        
    }
}
