using UnityEngine;


public class CardEnemy : MonoBehaviour, ICardPlayer
{
    [SerializeField] private GameObject[] deck;
    public Card[] hand;
    public int maxHealth = 10;
    private int health = 10;
    private void Start()
    {
        CardGameMaster.instance.enemy = this;
    }
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    public void DrawCard(int amount = 1, GameObject cardPrefab = null)
    {
        GameObject drawnCard = cardPrefab;
        if (drawnCard == null) drawnCard = deck[Random.Range(0, deck.Length)];
        GameObject newCard = Instantiate(drawnCard, transform);
        if (amount > 1) DrawCard(amount - 1, cardPrefab);

    }

    public void TakeTurn()
    {
        hand = GetComponentsInChildren<Card>();
        hand[0].UseCard(this,CardPlayerScript.instance);

    }
}
