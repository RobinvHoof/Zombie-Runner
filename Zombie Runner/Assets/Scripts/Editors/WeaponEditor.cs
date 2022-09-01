using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor 
{
    private void OnSceneGUI() {
        Weapon weapon = (Weapon)target;
        Handles.color = Color.cyan;
        
        Handles.DrawWireArc(weapon.FPCamera.transform.position, Vector3.up, Vector3.forward, 360, weapon.weaponSettings.range);

        Vector3 recoilVector = Quaternion.AngleAxis(weapon.weaponSettings.recoil, Vector3.Cross((weapon.FPCamera.transform.forward).normalized, Vector3.up)) * (weapon.FPCamera.transform.forward).normalized;
        Handles.DrawLine(weapon.FPCamera.transform.position, weapon.FPCamera.transform.position + recoilVector);
    }
}