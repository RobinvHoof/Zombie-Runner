using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyAI))]
public class EnemyAIEditor : Editor 
{
    private void OnSceneGUI() {
        EnemyAI enemyAI = (EnemyAI)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyAI.transform.position, Vector3.up, Vector3.forward, 360, enemyAI.trackingSettings.trackingRange);
    }
}