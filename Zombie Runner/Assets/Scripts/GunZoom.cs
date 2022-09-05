using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.FirstPerson;

public class GunZoom : MonoBehaviour
{
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] Camera FPCamera;
    [SerializeField] Canvas aimReticle;

    [SerializeField, Range(0, 2)] float recoilModifier = 0.2f;
    [SerializeField, Range(0, 2)] float spreadModifier = 1f;
    [SerializeField, Range(0, 1)] float sensitivityModifier = 0.5f;
    [SerializeField] float FOV = 60;


    private Vector3 defaultTransform;
    private Quaternion defaultRotation;
    private float defaultRecoil;
    private float defaultSpread;
    private float defaultSensitivity;

    private Gun gun;
    

    void Start() 
    {
        gun = GetComponent<Gun>();
        
        defaultRotation = transform.localRotation;
        defaultRecoil = gun.gunSettings.recoil;
        defaultSpread = gun.gunSettings.palletSpread;
        defaultSensitivity = fpsController.mouseLook.XSensitivity;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            GetComponent<Animator>().SetTrigger("ScopeIn");
            GetComponent<Animator>().StopPlayback();

            aimReticle.enabled = false;
            gun.gunSettings.recoil = defaultRecoil * recoilModifier;
            gun.gunSettings.palletSpread = defaultSpread * spreadModifier;

            fpsController.mouseLook.XSensitivity = defaultSensitivity * sensitivityModifier;
            fpsController.mouseLook.YSensitivity = defaultSensitivity * sensitivityModifier;
        } 

        if (Input.GetButtonUp("Fire2"))
        {

            GetComponent<Animator>().SetTrigger("ScopeOut");

            aimReticle.enabled = true;
            gun.gunSettings.recoil = defaultRecoil;
            gun.gunSettings.palletSpread = defaultSpread;

            fpsController.mouseLook.XSensitivity = defaultSensitivity;
            fpsController.mouseLook.YSensitivity = defaultSensitivity;
        }

        FPCamera.fieldOfView = FOV;
    }
}
