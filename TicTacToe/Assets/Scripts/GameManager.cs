using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum State { Idle, PlayerTurn, AITurn, PlayerWin, AIWin, Draw };
    public enum Pieces { Cross, Zero};
    public int[,] winners = {
        {0, 1, 2 },
        {3, 4, 5 },
        {6, 7, 8 },
        {0, 3, 6 },
        {1, 4, 7 },
        {2, 5, 8 },
        {0, 4, 8 },
        {2, 4, 6 }
    };

    private int[] tiles = new int[9];
    private int lastAvailable = 9;
    private bool playerTurn = true;
    

    private State currentState;
    private Pieces currentPieces;

    private AIController ai;
    
    private void SetState(State newState)
    {
        currentState = newState;
    }
    // Use this for initialization
	void Start () {
        SetState(State.Idle);
        currentPieces = Pieces.Cross;
        ai = FindObjectOfType<AIController>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (currentState)
        {
            case State.Idle:

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
                else if (lastAvailable - CountAvailable() == 1)
                {
                    lastAvailable--;

                    if (CountAvailable() == 0)
                    {
                        SetState(State.Draw);
                    }
                    else
                    {
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

                Debug.Log("PlayerWin state");
                break;
            case State.AIWin:

                Debug.Log("AIwin state");
                break;
            case State.Draw:

                Debug.Log("Draw State");
                break;
            default:
                break;
        }
    }

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

    public void SetTile (int index)
    {
        tiles[index] = Pieces.Cross == currentPieces ? 1 : 2;
    }

    private bool CheckWinner()
    {
        for (int i = 0; i < 8; i++)
        {
            if (tiles[winners[i,0]] == tiles[winners[i,1]] && tiles[winners[i,1]] == tiles[winners[i,2]])
            {
                if (tiles[winners[i,0]]!= 0)
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
}
