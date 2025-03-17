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


    public Card()
    {

    }

    public Card(int Id, string CardName, int Cost, int Power, string CardDescription)
    {
        this.id = Id;
        this.cardName = CardName;
        this.cost = Cost;
        this.power = Power;
        this.cardDescription = CardDescription;
    }
}
