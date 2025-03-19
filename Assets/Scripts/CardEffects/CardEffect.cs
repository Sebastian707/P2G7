using UnityEngine;
[System.Serializable]
public abstract class CardEffect : MonoBehaviour 
{
    
    public abstract void Use(CardPlayerScript cardPlayer);
}
