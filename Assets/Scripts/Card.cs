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
        //maybe change it to get the components in code instead of someone needing to place it into an array. that way there's no confusion if there's more than 1 instance of the same script (since it can't be renamed so hard to tell if you put the same instance twice or 1 of each)
        foreach (CardEffect effect in Effects)
        {
            effect.Use();
        }
        Destroy(gameObject);
    }
}
