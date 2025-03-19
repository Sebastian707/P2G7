using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Text cardNameText;
    private Card2 cardData;

    public void SetCardData(Card2 card)
    {
        cardData = card;
        if (cardNameText != null)
        {
            cardNameText.text = card.cardName;
        }
    }

    public void OnClick()
    {
        if (cardData != null)
        {
            HandManager.Instance.PlayCard(cardData); // Calls HandManager to play the card
            Destroy(gameObject); // Removes card from UI
        }
    }
}