using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{

    
    public string cardName;
    public string description;
    public int cost;
    public CardEffect[] Effects;
    // references
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private TextMeshProUGUI costText;

    private void Start()
    {

        nameText.text = cardName;
        DescriptionText.text = description;
        costText.text = $"{cost} energy";
        
    }
    public void ButtonPress()
    {
        CardPlayerScript.instance.SpendCard(this);
    }
    public void UseCard()
    {
        foreach (CardEffect effect in Effects)
        {
            effect.Use();
        }
        Destroy(gameObject);
    }
}
