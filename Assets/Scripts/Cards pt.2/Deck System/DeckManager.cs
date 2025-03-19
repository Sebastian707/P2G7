using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance; // Singleton instance 

    public List<Card2> deck = new List<Card2>();
    public List<Card2> discardPile = new List<Card2>();

    private void Awake()
    {
        // Ensure only one instance exists 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public void ShuffleDeck()
    {
        deck = deck.OrderBy(x => Random.value).ToList();
    }

    public Card2 DrawCard()
    {
        if (deck.Count == 0)
        {
            ResetDeck();
        }

        if (deck.Count > 0)
        {
            Card2 drawnCard = deck[0];
            deck.RemoveAt(0);
            return drawnCard;
        }

        return null; // If no cards left
    }

    public void ResetDeck()
    {
        deck = new List<Card2>(discardPile);
        discardPile.Clear();
        ShuffleDeck();
    }

    public void DrawStartingHand(HandManager handManager, int numCards)
{
    if (handManager != null)
    {
        handManager.DrawHand(numCards, this);
    }
}

}
