using UnityEditor;

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

            switch (towerData.damageType)
            {
                case DamageType.Burst:
                    BurstSettings(towerData);
                    break;
            }
            EditorGUILayout.Space(30);
            
            DrawPropertiesExcluding(
                serializedObject,
                "burstDelay",
                "burstCount",
                "burstMaxBullets"
                );

            // Fin du bloc modifiable
            serializedObject.ApplyModifiedProperties();
        }

        #region Settings
        private void BurstSettings(TowerData towerData)
        {
            EditorGUILayout.LabelField("Burst Settings", EditorStyles.boldLabel);
            //towerData.burstCount = EditorGUILayout.IntField("Burst Count", towerData.burstCount);
            towerData.burstMaxBullets = EditorGUILayout.IntField("Burst Max Bullets", towerData.burstMaxBullets);
            towerData.burstDelay = EditorGUILayout.FloatField("Burst Delay", towerData.burstDelay);
        }
        
        
        #endregion
    }
}
