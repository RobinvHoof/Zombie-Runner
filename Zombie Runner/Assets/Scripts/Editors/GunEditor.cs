using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Gun))]
public class WeaponEditor : Editor 
{
    private void OnSceneGUI() {
        Gun weapon = (Gun)target;
        Handles.color = Color.cyan;
        
        Handles.DrawWireArc(weapon.FPCamera.transform.position, Vector3.up, Vector3.forward, 360, weapon.gunSettings.range);

        Vector3 recoilVector = Quaternion.AngleAxis(weapon.gunSettings.recoil, Vector3.Cross((weapon.FPCamera.transform.forward).normalized, Vector3.up)) * (weapon.FPCamera.transform.forward).normalized;
        Handles.DrawLine(weapon.FPCamera.transform.position, weapon.FPCamera.transform.position + recoilVector);
    }
}