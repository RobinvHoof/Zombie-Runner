using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] Camera FPCamera;
    [SerializeField] Canvas aimReticle;

    [SerializeField, Range(0, 2)] float recoilModifier = 0.2f;
    [SerializeField, Range(0, 1)] float sensitivityModifier = 0.5f;
    [SerializeField] float FOV = 60;


    private Vector3 defaultTransform;
    private Quaternion defaultRotation;
    private float defaultRecoil;
    private float defaultSensitivity;

    private Weapon weapon;
    

    void Start() 
    {
        weapon = GetComponent<Weapon>();
        
        defaultRotation = transform.localRotation;
        defaultRecoil = weapon.weaponSettings.recoil;
        defaultSensitivity = fpsController.mouseLook.XSensitivity;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            GetComponent<Animator>().SetTrigger("ScopeIn");
            GetComponent<Animator>().StopPlayback();

            aimReticle.enabled = false;
            weapon.weaponSettings.recoil = defaultRecoil * recoilModifier;

            fpsController.mouseLook.XSensitivity = defaultSensitivity * sensitivityModifier;
            fpsController.mouseLook.YSensitivity = defaultSensitivity * sensitivityModifier;
        } 

        if (Input.GetButtonUp("Fire2"))
        {

            GetComponent<Animator>().SetTrigger("ScopeOut");

            aimReticle.enabled = true;
            weapon.weaponSettings.recoil = defaultRecoil;

            fpsController.mouseLook.XSensitivity = defaultSensitivity;
            fpsController.mouseLook.YSensitivity = defaultSensitivity;
        }

        FPCamera.fieldOfView = FOV;
    }
}
