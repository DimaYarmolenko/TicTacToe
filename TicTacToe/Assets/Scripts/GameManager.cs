using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum State { Idle, XTurn, OTurn, XWin, OWin, Draw };

    private int[] tiles = new int[9];
    private int lastAvailable = 9;
    private bool playerTurn = true;
    private int[,] winners = {
        {0, 1, 2 },
        {3, 4, 5 },
        {6, 7, 8 },
        {0, 3, 6 },
        {1, 4, 7 },
        {2, 5, 8 },
        {0, 4, 8 },
        {2, 4, 6 }
    };

    private State currentState;
    
    private void SetState(State newState)
    {
        currentState = newState;
    }
    // Use this for initialization
	void Start () {
        SetState(State.Idle);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(CheckWinner());

        switch (currentState)
        {
            case State.Idle:

                Debug.Log("Idle State");
                SetState(State.XTurn);
                break;

            case State.XTurn:
                Debug.Log("XTurn State");

                if (CheckWinner())
                {
                    SetState(State.XWin);
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
                        SetState(State.OTurn);
                    }
                }
                

                break;
            case State.OTurn:
                Debug.Log("OTurn State");

                if (CheckWinner())
                {
                    SetState(State.OWin);
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
                        SetState(State.XTurn);
                    }
                }
                break;
            case State.XWin:

                Debug.Log("XWin state");
                break;
            case State.OWin:

                Debug.Log("OWin state");
                break;
            case State.Draw:

                Debug.Log("Draw State");
                break;
            default:
                break;
        }
    }

    public void SetTile (int index)
    {
        tiles[index] = State.XTurn == currentState ? 1 : 2;
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
}
