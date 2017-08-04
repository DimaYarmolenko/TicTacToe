using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public int id;
    public Sprite cross, zero;

    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    private bool available = true;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        if (available && gameManager.GetState() == GameManager.State.PlayerTurn)
        {
            TakeTile();
        }
    }

    public void TakeTile()
    {
            spriteRenderer.sprite = gameManager.GetPieces() == GameManager.Pieces.Cross ? cross : zero;
            gameManager.SetTile(id);
            available = false;
    }

    public bool IsAvailable()
    {
        return available;
    }
}
