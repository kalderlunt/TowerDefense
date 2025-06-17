using UnityEngine;
using System.IO.Ports;

public class TimerToArduino : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM3", 9600); // Remplace COM3 par ton port réel
    float timer = 0f;
    int lastSentSecond = -1;

    void Start()
    {
        if (!serial.IsOpen)
            serial.Open();
    }

    void Update()
    {
        timer += Time.deltaTime;
        int seconds = Mathf.FloorToInt(timer);

        if (seconds != lastSentSecond)
        {
            lastSentSecond = seconds;
            string message = seconds.ToString("0000"); // Ex: "0032"
            serial.WriteLine(message);
            Debug.Log("Message envoyé à Arduino : " + message);
        }
    }

    void OnApplicationQuit()
    {
        if (serial.IsOpen)
            serial.Close();
    }
}
