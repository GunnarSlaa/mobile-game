using TMPro;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int buttonNumber;
    private Deck deck;

    public void Init(Deck deck, int buttonNumber, Sprite sprite)
    {
        this.deck = deck;
        this.buttonNumber = buttonNumber;
        spriteRenderer.sprite = sprite;
    }

    public void NewPower(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void Clicked()
    {
        deck.ButtonClicked(buttonNumber);
    }
}
