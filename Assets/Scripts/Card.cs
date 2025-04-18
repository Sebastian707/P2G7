using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{


    public string cardName;
    public string description;
    public int cost;
    private CardEffect[] Effects;
    // references
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private TextMeshProUGUI costText;

    private void Awake()
    {
        Effects = GetComponents<CardEffect>();
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
        Debug.LogWarning("old UseCard was called", this);
        foreach (CardEffect effect in Effects)
        {
            effect.Use();
        }
        Destroy(gameObject);
    }
    public void UseCard(ICardPlayer owner, ICardPlayer target)
    {


        foreach (CardEffect effect in Effects)
        {
            effect.Use(owner,target);
        }
        Destroy(gameObject);

    }
}
