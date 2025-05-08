using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public static CurrencyController instance;

    public float currentMoney;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Initializes the UI money display at game start
    private void Start()
    {
        UIController.instance.UpdateMoneyText(currentMoney);
    }

    // Deducts money and updates the UI
    public void SpendMoney(float amountToSpend)
    {
        currentMoney -= amountToSpend;
        UIController.instance.UpdateMoneyText(currentMoney);
    }

    // Adds money and updates the UI
    public void AddMoney(float amountToAdd)
    {
        currentMoney += amountToAdd;
        UIController.instance.UpdateMoneyText(currentMoney);
    }

    // Returns true if the player has enough money for a transaction
    public bool CheckMoney(float amount)
    {
        return currentMoney >= amount;
    }
}
