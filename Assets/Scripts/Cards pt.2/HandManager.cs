using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
     public List<Card> hand = new List<Card>();
    public int maxHandSize = 5;
    public Transform handArea;

    public void DrawHand(int numCards, DeckManager deck)
    {
        for (int i = 0; i < numCards && hand.Count < maxHandSize; i++)
        {
            Card newCard = deck.DrawCard();
            hand.Add(newCard);
            InstantiateCardUI(newCard);
        }
    }

    void InstantiateCardUI(Card card)
    {
        // Instantiate card UI prefab and set details
    }
}
