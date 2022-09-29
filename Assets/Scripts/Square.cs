using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private SpriteRenderer lockRenderer;
    [SerializeField] private SpriteRenderer tempSquare;

    private Vector2Int gridPosition;
    private SquareGrid squareGrid;
    private Color color = ColorData.clearColor;
    private Color tempColor;
    private bool locked = false;

    public void Init(Vector2Int gridPosition, SquareGrid squareGrid, float tempSquareScale)
    {
        this.gridPosition = gridPosition;
        this.squareGrid = squareGrid;
        tempSquare.transform.localScale = new Vector3(tempSquareScale, tempSquareScale, 0);
    }    

    public void SetColor (Color color, bool temp)
    {
        if (locked) return;
        if (temp)
        {
            this.tempColor = color;
            tempSquare.color = color;
            tempSquare.enabled = true;
        }
        else
        {
            this.color = color;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    public Color GetColor()
    {
        return this.color;
    }

    private void OnMouseDown() { squareGrid.SquareClicked(gridPosition); }

    private void OnMouseEnter() { squareGrid.SquareHovered(gridPosition); }

    private void OnMouseExit() { squareGrid.SquareExit(); }

    internal void Lock(bool temp)
    {
        if(!temp) locked = true;
        lockRenderer.enabled = true;
        if (temp) lockRenderer.color = new Color(lockRenderer.color.r, lockRenderer.color.g, lockRenderer.color.b, 0.3f);
        else lockRenderer.color = new Color(lockRenderer.color.r, lockRenderer.color.g, lockRenderer.color.b, 1f);
    }

    public void ClearTemp()
    {
        tempSquare.enabled = false;
        if(!locked) lockRenderer.enabled = false;
    }

    public void Reset()
    {
        locked = false;
        SetColor(ColorData.clearColor, false);
        lockRenderer.enabled = false;
        tempSquare.enabled = false;
    }
}
