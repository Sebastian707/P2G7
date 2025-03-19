using UnityEngine;
using TMPro;

public class CardPlayerScript : MonoBehaviour
{
    public int maxEnergy = 4;
    public int energy = 4;
    public GameObject[] deck;
    // UI REFERENCES
    public TextMeshProUGUI energyText;
    private void Start()
    {
        energyText.text= $"{energy}/{maxEnergy} energy";
        DrawCard(1,deck[0]);
    }
    public void DrawCard(int amount =1,GameObject drawnCard = null)
    {
        if(drawnCard==null) drawnCard = deck[Random.Range(0, deck.Length)];
        GameObject newCard = Instantiate(drawnCard,transform);
        if (amount > 1) DrawCard(amount - 1);
       
    }
    public void SpendCard(Card card)
    {
        if (card.cost > energy) return;
        energy -= card.cost;
        energyText.text = $"{energy}/{maxEnergy} energy";
        card.UseCard();

        
        
    }
}
