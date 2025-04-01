using UnityEngine;
[System.Serializable]
public abstract class CardEffect : MonoBehaviour
{

    public abstract void Use();
    public abstract void Use(ICardPlayer owner,ICardPlayer target);
}
