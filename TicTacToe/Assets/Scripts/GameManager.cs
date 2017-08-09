using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum State { Idle, PlayerTurn, AITurn, PlayerWin, AIWin, Draw };
    //X or O
    public enum Pieces { Cross, Zero};
    //all possible win conditions
    public List<int[]> winners = new List<int[]>()
    {
        new int[] {0, 1, 2 },
        new int[] {3, 4, 5 },
        new int[] {6, 7, 8 },
        new int[] {0, 3, 6 },
        new int[] {1, 4, 7 },
        new int[] {2, 5, 8 },
        new int[] {0, 4, 8 },
        new int[] {2, 4, 6 }
    };

    //contains the state of playboard
    private int[] tiles = new int[9];
    //amount of available tiles before player triggered any tile
    private int lastAvailable = 9;
    //who goes first
    private bool playerTurn = true;
    

    private State currentState;
    private Pieces currentPieces;

    private AIController ai;
    private ScoreManager scoreManager;
    //panel that shows result of the game
    private ResultPanel resPan;
    
    private void SetState(State newState)
    {
        currentState = newState;
    }
    

	void Start () {
        SetState(State.Idle);
        currentPieces = Pieces.Cross;
        ai = FindObjectOfType<AIController>();
        scoreManager = FindObjectOfType<ScoreManager>();
        resPan = FindObjectOfType<ResultPanel>();
	}
	
	
	void Update () {

        switch (currentState)
        {
            case State.Idle:

                //chose who goes first depending on playerTurn
                if (playerTurn)
                {
                    SetState(State.PlayerTurn);
                }
                else
                {
                    SetState(State.AITurn);
                }
                break;

            case State.PlayerTurn:

                if (CheckWinner())
                {
                    SetState(State.PlayerWin);
                }
                else if (lastAvailable - CountAvailable() == 1) //check if any tile was pressed
                {
                    lastAvailable--;

                    //no more tiles to continues, no winner
                    if (CountAvailable() == 0)
                    {
                        SetState(State.Draw);
                    }
                    else
                    {
                        //switch X to O or vice versa
                        SwitchPieces();
                        SetState(State.AITurn);
                    }
                }
                break;

            case State.AITurn:

                ai.MakeTurn();
                lastAvailable--;

                if (CheckWinner())
                {
                    SetState(State.AIWin);
                }
                else if (CountAvailable() == 0)
                {
                    SetState(State.Draw);
                }
                else
                {
                    SwitchPieces();
                    SetState(State.PlayerTurn);
                }
                break;

            case State.PlayerWin:

                resPan.ActivatePanel("Victory");
                break;

            case State.AIWin:

                resPan.ActivatePanel("Defeat");
                break;

            case State.Draw:

                resPan.ActivatePanel("Draw");
                break;

            default:
                break;
        }
    }

    //sitch currently played pieces to opposite (X to O || O to X)
    private void SwitchPieces()
    {
        if (currentPieces == Pieces.Cross)
        {
            currentPieces = Pieces.Zero;
        }
        else
        {
            currentPieces = Pieces.Cross;
        }
    }

    //insert incoming from the tile value
    public void SetTile (int index)
    {
        tiles[index] = Pieces.Cross == currentPieces ? 1 : 2;
    }

    //loop through winners[] to check if any combination is filled completely
    private bool CheckWinner()
    {
        for (int i = 0; i < winners.Count; i++)
        {
            if (tiles[winners[i][0]] == tiles[winners[i][1]] && tiles[winners[i][1]] == tiles[winners[i][2]])
            {
                if (tiles[winners[i][0]]!= 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private int CountAvailable()
    {
        int availableTiles = 0;

        foreach (int tile in tiles)
        {
            if (tile == 0)
            {
                availableTiles++;
            }
        }

        return availableTiles;
    }

    public State GetState()
    {
        return currentState;
    }

    public Pieces GetPieces()
    {
        return currentPieces;
    }

    public int[] GetTiles()
    {
        return tiles;
    }

    //back to initial values to start new round, changing turn oder depending on the previous result
    public void Restart()
    {
        //updating playerprefs values
        scoreManager.AddResult(currentState);

        
        currentPieces = Pieces.Cross;
        tiles = new int[9];
        lastAvailable = 9;

        //who goes first next round
        SetNextPlayer();
        //updating scoreboxes
        scoreManager.UpdateScores();

        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            tile.ResetTile();
        }

        

        SetState(State.Idle);
        //removing the result panel
        resPan.DeactivatePanel();
    }

    //who's gonna start next round
    private void SetNextPlayer()
    {
        switch (currentState)
        {
            case State.PlayerWin:

                playerTurn = true;
                break;

            case State.AIWin:

                playerTurn = false;
                break;

            case State.Draw:

                if (playerTurn)
                {
                    playerTurn = false;
                }
                else
                {
                    playerTurn = true;
                }
                break;

            default:
                break;
        }
    }
}
