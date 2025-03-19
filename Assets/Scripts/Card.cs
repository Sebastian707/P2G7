using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{  
    
    private CardPlayerScript cardPlayer;
    public string cardName;
    public int cost;
    public CardEffect[] Effects;
    
    private void Start()
    {
        cardPlayer = GetComponentInParent<CardPlayerScript>();
        GetComponentInChildren<TextMeshProUGUI>().text = cardName;
    }
    public void ButtonPress()
    {
        cardPlayer.SpendCard(this);
    }
    public void UseCard()
    {
        foreach( CardEffect effect in Effects)
        {
            effect.Use(cardPlayer);
        }
        cardPlayer.DrawCard();
        Destroy(gameObject);
    }
}
