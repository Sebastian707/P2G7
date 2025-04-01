using UnityEngine;
public interface ICardPlayer
{
    public int Health { get; set; }
    public void DrawCard(int amount = 1, GameObject cardPrefab = null);

}
