using UnityEngine;

public class PlayerMoneyInGame : MonoBehaviour
{
    public static PlayerMoneyInGame instance;
    
    [SerializeField] private int moneyStart = 400;
    public int money { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("Money : " + money); 
        money = moneyStart;
        Debug.Log("Money : " + money);
    }
}