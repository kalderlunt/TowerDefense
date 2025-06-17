using UnityEngine;
using UnityEditor;
using System.IO.Ports;

namespace Arduino.LCD
{
    [CustomEditor(typeof(Arduino.LCD.LCD))]
    
    public class LCD_Editor : Editor
    {
        string[] ports;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty comPortProp = serializedObject.FindProperty("comPort");
            SerializedProperty baudRateProp = serializedObject.FindProperty("baudRate");
            SerializedProperty autoDetectProp = serializedObject.FindProperty("autoDetect");

            ports = SerialPort.GetPortNames();

            EditorGUILayout.PropertyField(autoDetectProp, new GUIContent("Détection automatique"));
            EditorGUILayout.PropertyField(baudRateProp, new GUIContent("BaudRate"));

            if (autoDetectProp.boolValue)
            {
                EditorGUILayout.HelpBox("Le port COM sera choisi automatiquement.", MessageType.Info);
            }
            else
            {
                if (ports.Length == 0)
                {
                    EditorGUILayout.HelpBox("Aucun port COM détecté.", MessageType.Warning);
                    comPortProp.stringValue = "";
                }
                else
                {
                    int selected = 0;
                    
                    // Essaie de pré-sélectionner le port actuel 
                    if (!string.IsNullOrEmpty(comPortProp.stringValue))
                    {
                        int idx = System.Array.IndexOf(ports, comPortProp.stringValue);
                        if (idx >= 0)
                            selected = idx;
                    }
                    
                    selected = EditorGUILayout.Popup("Port COM", selected, ports);
                    comPortProp.stringValue = ports[selected];
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}