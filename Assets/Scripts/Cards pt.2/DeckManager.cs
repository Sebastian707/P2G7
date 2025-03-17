using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{

    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();

    public void ShuffleDeck()
    {
        deck = deck.OrderBy(x => Random.value).ToList();
    }

    public Card DrawCard()
    {
        if (deck.Count == 0)
        {
            ResetDeck();
        }
        Card drawnCard = deck[0];
        deck.RemoveAt(0);
        return drawnCard;
    }

    public void ResetDeck()
    {
        deck = new List<Card>(discardPile);
        discardPile.Clear();
        ShuffleDeck();
    }
}

