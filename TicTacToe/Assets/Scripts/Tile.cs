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
        if (available)
        {
            spriteRenderer.sprite = gameManager.GetState() == GameManager.State.XTurn ? cross : zero;
            gameManager.SetTile(id);
            available = false;
        } 
    }
}
