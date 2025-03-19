using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    private void Awake()
    {
        LoadCards();
    }

    private void LoadCards()
    {
        // Load all cards from the "Resources/Cards" folder
        Card[] loadedCards = Resources.LoadAll<Card>("Cards");

        // Add them to the list
        foreach (Card card in loadedCards)
        {
            cards.Add(card);
        }

        Debug.Log("Loaded " + cards.Count + " cards into the database.");
    }
}
