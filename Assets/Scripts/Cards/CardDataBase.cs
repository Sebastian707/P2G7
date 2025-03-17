using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{

    public static System.Collections.Generic.List<Card> cardList = new System.Collections.Generic.List<Card>();
    void Awake()
    {
        CardDataBase.cardList.Add(new Card(0, "None", 0, 0, "None"));
        CardDataBase.cardList.Add(new Card(1, "Human", 0, 0, "This is a human"));
        CardDataBase.cardList.Add(new Card(2, "Elf", 0, 0, "This is a elf"));
        CardDataBase.cardList.Add(new Card(3, "Dwarf", 0, 0, "This is a dwarf"));
        CardDataBase.cardList.Add(new Card(4, "Troll", 0, 0, "This is a troll"));
    }
}
