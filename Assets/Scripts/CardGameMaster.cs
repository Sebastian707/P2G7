using UnityEngine;
using TMPro;

public class CardGameMaster : MonoBehaviour
{
    
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
            enemyHealthText.text = $"Enemy health: {value}/{enemyMaxHealth}";
        }
    }
    private void Awake()
    {
        if (instance != null) Debug.LogError("an instance of cardplayerscript already exists");
        instance = this;
    }
    private void Start()
    {
        EnemyHealth = enemyMaxHealth;
    }

    public void Endturn()
    {
        CardPlayerScript.instance.Health -= enemyDamage;
        CardPlayerScript.instance.Energy = CardPlayerScript.instance.maxEnergy;
        CardPlayerScript.instance.DrawCard();
    }
}
