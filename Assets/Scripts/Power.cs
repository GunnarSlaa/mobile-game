using UnityEngine;

public abstract class Power : MonoBehaviour
{
    public PowerController powerController;
    public Sprite powerSprite;
    public abstract void Execute(Vector2Int gridPosition, bool temp);
}
