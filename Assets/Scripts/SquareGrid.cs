using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class SquareGrid : MonoBehaviour
{
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private GameObject lineCheckPrefab;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private int squareSize;
    [SerializeField] private int tempSquareSize;
    [SerializeField] private int lineCheckerSize;
    [SerializeField] private int squareSpacing;
    [SerializeField] private int lineCheckOffset;

    [SerializeField] private TurnController turnController;
    [SerializeField] private PowerController powerController;
    [SerializeField] private ScoreController scoreController;

    private Dictionary<Vector2Int, Square> gridDict = new ();
    private List<LineChecker> lineCheckers = new();

    // Start is called before the first frame update
    void Start()
    {
        DrawGrid();
        DrawLineCheckers();
    }

    void DrawGrid()
    {
        Vector3 startingPoint = new Vector3(-(gridSize.x - 1) * 0.5f * (squareSpacing + squareSize), (gridSize.y - 1) * 0.5f * (squareSpacing + squareSize), 0);
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 offset = new Vector3(x * (squareSize + squareSpacing), -y * (squareSize + squareSpacing), 0);
                Vector3 spawnLocation = startingPoint + offset;
                Square square = Instantiate(squarePrefab, transform).GetComponent<Square>();
                Vector2Int gridPosition = new(x, y);
                square.Init(gridPosition, this, (float)tempSquareSize / squareSize);
                gridDict[gridPosition] = square;
                square.transform.localPosition = spawnLocation;
                square.transform.localScale = new Vector3(squareSize * 100, squareSize * 100, 0);
            }
        }
        Debug.Log($"gridDict: {gridDict.Count}");
    }

    void DrawLineCheckers()
    {
        LineChecker DrawLineChecker(Vector3 position)
        {
            LineChecker lineChecker = Instantiate(lineCheckPrefab, transform).GetComponent<LineChecker>();
            lineChecker.transform.localPosition = position;
            lineChecker.transform.localScale = new Vector3(lineCheckerSize * 100, lineCheckerSize * 100, 0);
            lineChecker.scoreController = scoreController;
            lineCheckers.Add(lineChecker);
            return lineChecker;
        }

        int cornerOffset = lineCheckOffset + squareSize / 2 + lineCheckerSize / 2;
        Vector3 startingPoint = new(-(gridSize.x - 1) * 0.5f * (squareSpacing + squareSize) - cornerOffset, (gridSize.y - 1) * 0.5f * (squareSpacing + squareSize) + cornerOffset, 0);
        LineChecker lineChecker;
        //Diagonal 1 \
        lineChecker = DrawLineChecker(startingPoint);
        lineChecker.squareList = (from i in Enumerable.Range(0, gridSize.x) select gridDict[new Vector2Int(i, i)]).ToList();
        //Diagonal 2 /
        Vector3 topRightOffset = new(2 * cornerOffset + (gridSize.x - 1) * (squareSize + squareSpacing), 0, 0);
        lineChecker = DrawLineChecker(startingPoint + topRightOffset);
        lineChecker.squareList = (from i in Enumerable.Range(0, gridSize.x) select gridDict[new Vector2Int(i, gridSize.x - i - 1)]).ToList();
        //Rows
        for (int y = 0; y < gridSize.y; y++)
        {
            Vector3 offset = new Vector3(0, -(cornerOffset + (squareSize + squareSpacing) * y), 0);
            lineChecker = DrawLineChecker(startingPoint + offset);
            lineChecker.squareList = (from x in Enumerable.Range(0, gridSize.x) select gridDict[new Vector2Int(x, y)]).ToList();
        }
        //Columns
        for (int x = 0; x < gridSize.y; x++)
        {
            Vector3 offset = new Vector3(cornerOffset + (squareSize + squareSpacing) * x, 0, 0);
            lineChecker = DrawLineChecker(startingPoint + offset);
            lineChecker.squareList = (from y in Enumerable.Range(0, gridSize.y) select gridDict[new Vector2Int(x, y)]).ToList();
        }
    }

    public void CheckLines()
    {
        foreach (LineChecker lineChecker in lineCheckers) lineChecker.CheckLine();
    }

    public void SquareClicked(Vector2Int gridPosition)
    {
        if (!turnController.myTurn) return;
        //Execute a power if one has been selected
        if (!powerController.ExecutePower(gridPosition)) return;
        //Start the next turn for the other player
        turnController.TurnFinished();
    }

    public void ColorSquare(Vector2Int gridPosition, Color color, bool temp)
    {
        gridDict[gridPosition].SetColor(color, temp);
    }

    public void FlipSquare(Vector2Int gridPosistion, Color color1, Color color2, bool temp)
    {
        Square square = gridDict[gridPosistion];
        if (square.GetColor() == color1) powerController.ColorSquare(gridPosistion, color2, temp);
        else if (square.GetColor() == color2) powerController.ColorSquare(gridPosistion, color1, temp);
    }

    public bool SquareExists(Vector2Int gridPosition)
    {
        return gridDict.ContainsKey(gridPosition);
    }

    public Vector2Int GetNeighbour(Vector2Int gridPostion, int xDelta, int yDelta)
    {
        Vector2Int neighbourPosition = new(gridPostion.x + xDelta, gridPostion.y + yDelta);
        return neighbourPosition;
    }

    internal void LockSquare(Vector2Int gridPosition, bool temp)
    {
        gridDict[gridPosition].Lock(temp);
    }

    public void SquareHovered(Vector2Int gridPosition)
    {
        if (!powerController.ExecutePower(gridPosition, true)) return;
    }

    public void SquareExit()
    {
        foreach(Square square in gridDict.Values)
        {
            square.ClearTemp();
        }
    }

    public void ResetGrid()
    {
        foreach(Square square in gridDict.Values) { square.Reset(); }
        foreach (LineChecker lineChecker in lineCheckers) { lineChecker.Reset(); }
    }
}
