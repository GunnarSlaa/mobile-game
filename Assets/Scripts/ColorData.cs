using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ColorData
{
    public static Color myColor = Color.blue;
    public static Color opponentColor = Color.red;
    public static Color clearColor = Color.white;

    private static readonly Dictionary<string, Color> colorDict = new()
    {
        { "blue", Color.blue },
        { "red", Color.red },
        { "white", Color.white }
    };
    private static readonly Dictionary<Color, string> colorDictInverse = new()
    {
        { Color.blue, "blue" },
        { Color.red, "red" },
        { Color.white, "white" }
    };

    public static Color ColorFromString (string str)
    {
        return colorDict[str];
    }

    public static string ColorName(Color color)
    {
        return colorDictInverse[color];
    }

    public static Color OppositeColor(Color color)
    {
        if (color == myColor) return opponentColor;
        else if (color == opponentColor) return myColor;
        else throw new System.Exception("Wrong input for OppositeColor");
    }
}
