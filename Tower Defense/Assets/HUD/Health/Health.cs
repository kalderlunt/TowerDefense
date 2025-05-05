using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float hpMax = 100;
    [SerializeField] private string nextScene = "Menu";
    [HideInInspector] public float lerpTimer;
    
    public float healthMax { get; private set; }
    public float healthPoint { get; private set; }

    private void Start()
    {
        healthMax = hpMax;
        healthPoint = healthMax;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncreaseHealth(4);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            DecreaseHealth(4);
        }
    }

    public void IncreaseHealth(float ammount)
    {
        lerpTimer = 0;
        
        healthPoint += ammount;
        
        if (healthPoint > healthMax)
        {
            healthPoint = healthMax;
        }
    }

    public void DecreaseHealth(float ammount)
    {
        lerpTimer = 0;
        
        healthPoint -= ammount;

        if (healthPoint <= 0)
        {
            MenuManager.instance.ChangeSceneBy(nextScene);
        }
    }
}