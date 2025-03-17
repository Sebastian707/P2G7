using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
[System.Serializable]

public class Card : MonoBehaviour
{
    public int id;
    public string cardName;
    public int cost;
    public int power;
    public string cardDescription;
    public ICardEffect effect;
    
    public Card(int Id, string CardName, int Cost, int Power, string CardDescription)
    {
        this.id = Id;
        this.cardName = CardName;
        this.cost = Cost;
        this.power = Power;
        this.cardDescription = CardDescription;
    }

    public void PlayCard(Character target)
{
    if (effect != null)
    {
        effect.ApplyEffect(target);
        Debug.Log(target.characterName + " affected by " + cardName);
    }
    else
        {
            Debug.Log("No effect assigned to " + cardName);
        }
}
}
