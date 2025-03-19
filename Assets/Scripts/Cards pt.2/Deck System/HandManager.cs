using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance;

    public List<Card2> hand = new List<Card2>();
    public int maxHandSize = 5;
    public Transform handArea;
    public GameObject cardPrefab; // Assign a card UI prefab in the Inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    private void Start()
    {
        DeckManager.Instance.DrawStartingHand(this, maxHandSize); // Draw starting hand at game start
    }

    public void DrawHand(int numCards, DeckManager deck)
    {
        for (int i = 0; i < numCards && hand.Count < maxHandSize; i++)
        {
            Card2 newCard = deck.DrawCard();
            if (newCard != null)
            {
                hand.Add(newCard);
                InstantiateCardUI(newCard);
            }
        }
    }

    public void DrawOneCard()
    {
        if (hand.Count < maxHandSize)
        {
            Card2 newCard = DeckManager.Instance.DrawCard(); 
            if (newCard != null)
            {
                hand.Add(newCard);
                InstantiateCardUI(newCard);
            }
            else
            {
                Debug.Log("No more cards in the deck!");
            }
        }
        else
        {
            Debug.Log("Hand is full!");
        }
    }

    public void PlayCard(Card2 card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card); // Remove card from hand
            ApplyCardEffect(card); // Apply the effect (damage, heal, etc.)
            Debug.Log(card.cardName + " was played!");
        }
    }

    void ApplyCardEffect(Card2 card)
{
    if (card.effect is CardEffect effect) // 
    {
        effect.ApplyEffect(Player.Instance); // Apply effect to player or enemy
    }
}



    void InstantiateCardUI(Card2 card)
    {
        if (cardPrefab != null && handArea != null)
        {
            GameObject cardObj = Instantiate(cardPrefab, handArea);
            CardUI cardUI = cardObj.GetComponent<CardUI>();
            if (cardUI != null)
            {
                cardUI.SetCardData(card);
            }
        }
    }
}
