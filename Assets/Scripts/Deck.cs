using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private PowerController powerController;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Vector2[] buttonLocations = new Vector2[4];

    private Power[] activePowers = new Power[4];
    private PowerButton[] powerButtons = new PowerButton[4];

    List<Power> drawPile = new();
    List<Power> discardPile = new();

    void Start()
    {
        drawPile = Resources.LoadAll<Power>("Powers").ToList();
        for (int i = 0; i < buttonLocations.Length; i++)
        {
            activePowers[i] = DrawFromDeck();
            powerButtons[i] = SpawnButton(buttonLocations[i]);
            powerButtons[i].Init(this, i, activePowers[i].powerSprite);
        }
    }

    public void ReplacePower(int index)
    {
        Power newPower = DrawFromDeck();
        DiscardCard(activePowers[index]);
        activePowers[index] = newPower;
        powerButtons[index].NewPower(newPower.powerSprite);
    }

    PowerButton SpawnButton(Vector2 position)
    {
        GameObject button = Instantiate(buttonPrefab, transform);
        button.transform.localPosition = position;
        return button.GetComponent<PowerButton>();
    }

    public void ButtonClicked(int buttonNumber)
    {
        powerController.SelectPower(activePowers[buttonNumber], buttonNumber);
    }

    Power DrawFromDeck()
    {
        int index = Random.Range(0, drawPile.Count);
        Power power = drawPile[index];
        drawPile.RemoveAt(index);
        if (drawPile.Count == 0) ShuffleDiscardPile();
        return power;
    }

    void DiscardCard(Power power)
    {
        discardPile.Add(power);
    }

    void ShuffleDiscardPile()
    {
        drawPile = discardPile;
        discardPile = new List<Power>();
    }
}
