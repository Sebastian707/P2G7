using TMPro;
using UnityEngine;

public class CardGameMaster : MonoBehaviour
{
    public CardEnemy enemy;

    public int enemyDamage = 3;
    public static CardGameMaster instance;
    [SerializeField] private TextMeshProUGUI enemyHealthText;
    private int enemyMaxHealth = 10;
    private int enemyHealth;
    public int EnemyHealth
    {
        get
        {
            return enemyHealth;
        }
        set
        {
            enemyHealth = value;
            //enemyHealthText.text = $"Enemy health: {value}/{enemyMaxHealth}";
        }
    }
    private void Awake()
    {
        if (instance != null) Debug.LogError("an instance of cardGameMaster already exists");
        instance = this;
    }
    private void Start()
    {
        EnemyHealth = enemyMaxHealth;
    }
    private void Update()
    {
        enemyHealthText.text = $"Enemy health: {enemy.Health}/{enemyMaxHealth}";
    }

    public void Endturn()
    {
        //CardPlayerScript.instance.Health -= enemyDamage;
        enemy.DrawCard();
        enemy.TakeTurn();
        CardPlayerScript.instance.Energy = CardPlayerScript.instance.maxEnergy;
        CardPlayerScript.instance.DrawCard();
    }
}
