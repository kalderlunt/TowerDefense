using System;
using System.IO.Ports;
using System.Globalization;
using UnityEngine;

public class TestConnectionAnalog : MonoBehaviour
{
    private SerialPort data_stream = new SerialPort("COM3", 19200);
    public string receivedString;
    public Rigidbody rb;
    public float sensitivity = 0.1f;

    private void Start()
    {
        if (!data_stream.IsOpen)
        {
            try
            {
                data_stream.Open();
                data_stream.ReadTimeout = 100;
            }
            catch (Exception e)
            {
                Debug.LogError("Erreur ouverture port série : " + e.Message);
            }
        }
    }

    private void Update()
    {
        if (!data_stream.IsOpen) return;

        try
        {
            receivedString = data_stream.ReadLine();
            string[] datas = receivedString.Trim().Split(',');

            if (datas.Length >= 3)
            {
                float x = float.Parse(datas[0].Trim(), CultureInfo.InvariantCulture);
                float y = float.Parse(datas[1].Trim(), CultureInfo.InvariantCulture);
                float rot = float.Parse(datas[2].Trim(), CultureInfo.InvariantCulture);

                rb.AddForce(x * sensitivity * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                rb.AddForce(0, 0, y * sensitivity * Time.deltaTime, ForceMode.VelocityChange);
                transform.Rotate(0, rot, 0);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Erreur parsing série : " + e.Message + " | Données reçues : " + receivedString);
        }
    }
}