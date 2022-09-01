using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [System.Serializable]
    public class WeaponSettings {
        [Min(0)]
        public float rpm = 60;

        [Min(0)]
        public float damage = 1;

        [Min(0)]
        public float range = 100;

        [Min(0)]
        public int palletsPerShot = 1;

        [Min(0)]
        public float palletSpread = 0;

        [Range(0, 45)]
        public float recoil = 0;        

        public LayerMask penetrateLayers;
    }

    [SerializeField] public WeaponSettings weaponSettings;
    [SerializeField] public Camera FPCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    

    private void Start() {
        StartCoroutine(CheckShoot());
    }


    private IEnumerator CheckShoot() 
    {        
        while(true)
        {
            if (Input.GetButton("Fire1"))
            {
                RaycastHit hit;

                

                Vector3 randomVector = 
                    Quaternion.AngleAxis(Random.Range(-weaponSettings.palletSpread, weaponSettings.palletSpread), Vector3.Cross((FPCamera.transform.forward).normalized, Vector3.up)) * (FPCamera.transform.forward).normalized +
                    Quaternion.AngleAxis(Random.Range(-weaponSettings.palletSpread, weaponSettings.palletSpread), Vector3.Cross((FPCamera.transform.forward).normalized, Vector3.right)) * (FPCamera.transform.forward).normalized;

                if (Physics.Raycast(FPCamera.transform.position, randomVector, out hit, weaponSettings.range, ~weaponSettings.penetrateLayers.value, QueryTriggerInteraction.Collide))
                {                    
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null) 
                    {
                        enemyHealth.OnWeaponHit(this);
                    }

                    GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impact, 0.2f);
                }   
                
                muzzleFlash.Play(); 
                FPCamera.transform.rotation *= Quaternion.Euler(-weaponSettings.recoil, 0, 0);
                yield return new WaitForSeconds(60 / weaponSettings.rpm);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    
}
