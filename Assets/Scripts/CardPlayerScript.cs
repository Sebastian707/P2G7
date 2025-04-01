using TMPro;
using UnityEngine;

public class CardPlayerScript : MonoBehaviour, ICardPlayer
{
    public int maxHealth = 10;
    private int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            healthText.text = $"{value}/{maxHealth} health";
        }
    }
    public int maxEnergy = 4;
    private int energy;
    public int Energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
            energyText.text = $"{value}/{maxEnergy} energy";
        }
    }
    public static CardPlayerScript instance;
    // REFERENCES
    [SerializeField] private GameObject[] deck;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI energyText;
    private void Awake()
    {
        if (instance != null) Debug.LogError("an instance of cardplayerscript already exists");
        instance = this;
    }
    private void Start()
    {
        Health = maxHealth;
        Energy = maxEnergy;
        energyText.text = $"{energy}/{maxEnergy} energy";
        DrawCard(1, deck[0]);
    }
    public void DrawCard(int amount = 1, GameObject cardPrefab = null)
    {
        GameObject drawnCard = cardPrefab;
        if (drawnCard == null) drawnCard = deck[Random.Range(0, deck.Length)];
        GameObject newCard = Instantiate(drawnCard, transform);
        if (amount > 1) DrawCard(amount - 1, cardPrefab);

    }
    public void SpendCard(Card card)
    {
        if (card.cost > energy) return;
        energy -= card.cost;
        energyText.text = $"{energy}/{maxEnergy} energy";
        card.UseCard(this,CardGameMaster.instance.enemy);
    }
}
