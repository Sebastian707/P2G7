using UnityEngine;

public class EnergyManager : MonoBehaviour
{
   public int maxEnergy = 3;
    public int currentEnergy;

    void Start()
    {
        ResetEnergy();
    }

    public void UseEnergy(int amount)
    {
        currentEnergy -= amount;
    }

    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
    }
}
