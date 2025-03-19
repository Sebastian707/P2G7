using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{  
    
    private CardPlayerScript cardPlayer;
    public string cardName;
    public int cost;
    public string speech;
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
        Debug.Log(speech);
        cardPlayer.DrawCard();
        Destroy(gameObject);
    }
}
