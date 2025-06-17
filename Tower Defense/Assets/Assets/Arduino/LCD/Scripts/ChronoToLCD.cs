using UnityEngine;
using Arduino.LCD;

public class ChronoToLCD : MonoBehaviour
{
    [SerializeField] LCD lcd;
    float time;

    void Start()
    {
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        string chrono = $"Temps: {time:F1} s";
        
        lcd.Send(chrono);

        // Pour éviter de spammer l’Arduino, n’envoie que 3 fois par seconde :
        // (Ou utilise une coroutine si tu veux)
    }
}