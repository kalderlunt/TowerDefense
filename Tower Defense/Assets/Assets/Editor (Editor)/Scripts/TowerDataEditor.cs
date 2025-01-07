using UnityEditor;
using UnityEngine;

namespace Assets.Editor__Editor_.Scripts
{
    [CustomEditor(typeof(TowerData))]
    public class TowerDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Référence vers l'objet TowerData
            TowerData towerData = (TowerData)target;

            // Début du bloc modifiable
            serializedObject.Update();
            
            // Affichage conditionnel pour Burst Settings
            if (towerData.damageType == DamageType.Burst)
            {
                EditorGUILayout.LabelField("Burst Settings", EditorStyles.boldLabel);
                //towerData.burstCount = EditorGUILayout.IntField("Burst Count", towerData.burstCount);
                towerData.burstMaxBullets = EditorGUILayout.IntField("Burst Max Bullets", towerData.burstMaxBullets);
                towerData.burstDelay = EditorGUILayout.FloatField("Burst Delay", towerData.burstDelay);
                EditorGUILayout.Space(20);
            }

            DrawPropertiesExcluding(
                serializedObject,
                "burstDelay",
                "burstCount",
                "burstMaxBullets"
                );

            // Fin du bloc modifiable
            serializedObject.ApplyModifiedProperties();
        }
    }
}