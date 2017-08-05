using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    //shows index to insert value in GameManager.tiles[]
    public int id;
    //cross, zero sprites to render if required
    public Sprite cross, zero;

    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    //availability to trigger the tile (only once)
    private bool available = true;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        //if tile isn't used and its player's turn
        if (available && gameManager.GetState() == GameManager.State.PlayerTurn)
        {
            TakeTile();
        }
    }

    //insert value in GameManager.Tiles[]
    public void TakeTile()
    {
        //choose what sprite to render depending on player's pieces
        spriteRenderer.sprite = gameManager.GetPieces() == GameManager.Pieces.Cross ? cross : zero;
        gameManager.SetTile(id);
        //protecting from consecutive usage
        available = false;
    }

    public bool IsAvailable()
    {
        return available;
    }

    public void ResetTile()
    {
        available = true;
        spriteRenderer.sprite = null;
    }
}
