using System.IO.Ports;
using UnityEngine;

namespace Arduino.LCD
{
    public class LCD : MonoBehaviour
    {

        [Header("Connexion série")]
        
        [Tooltip("Port COM à utiliser (laisser vide ou cocher 'Détection automatique' pour le choisir automatiquement)")]
        public string comPort = "";
        
        [Tooltip("Vitesse de communication (baud)")]
        public int baudRate = 9600;
        
        [Tooltip("Prend automatiquement le premier port COM détecté")]
        public bool autoDetect = true;
        
        private SerialPort port;

        void Start()
        {
            if (autoDetect || string.IsNullOrEmpty(comPort))
            {
                var ports = SerialPort.GetPortNames();
                if (ports.Length > 0)
                {
                    comPort = ports[0];
                    Debug.Log("[LCD] Port série détecté : " + comPort);
                }
                else
                {
                    Debug.LogError("[LCD] Aucun port COM détecté !");
                    return;
                }
            }

            port = new SerialPort(comPort, baudRate);

            try
            {
                port.Open();
                Debug.Log($"[LCD] Port ouvert : {comPort} à {baudRate} bauds");
            }
            catch (System.Exception e)
            {
                Debug.LogError("[LCD] Erreur ouverture port : " + e.Message);
            }
        }

        
        public void Send(string message)
        {
            if (port != null && port.IsOpen)
            {
                port.WriteLine(message);
            }
        }

        
        void OnApplicationQuit()
        {
            if (port != null && port.IsOpen)
            {
                port.Close();
                Debug.Log("[LCD] Port fermé.");
            }
        }
    }
}