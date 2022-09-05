using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Attack
{
    [System.Serializable]
    public class GunSettings {
        [Min(0)]
        public float rpm = 60;

        [Min(0)]
        public float range = 100;

        [Min(1)]
        public int palletsPerShot = 1;

        [Range(0, 90)]
        public float palletSpread = 0;

        [Range(0, 45)]
        public float recoil = 0;        

        public LayerMask penetrateLayers;
    }

    [SerializeField] public GunSettings gunSettings;
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
                for (int i = 0; i < gunSettings.palletsPerShot; i++)
                {
                    RaycastHit hit;

                    Vector3 randomVector = 
                        Quaternion.AngleAxis(Random.Range(-gunSettings.palletSpread, gunSettings.palletSpread), Vector3.Cross((FPCamera.transform.forward).normalized, Vector3.up)) * (FPCamera.transform.forward).normalized +
                        Quaternion.AngleAxis(Random.Range(-gunSettings.palletSpread, gunSettings.palletSpread), Vector3.Cross((FPCamera.transform.forward).normalized, Vector3.right)) * (FPCamera.transform.forward).normalized;

                    if (Physics.Raycast(FPCamera.transform.position, randomVector, out hit, gunSettings.range, ~gunSettings.penetrateLayers.value, QueryTriggerInteraction.Collide))
                    {                    
                        IAttackable target = hit.collider.GetComponent<IAttackable>();
                        if (target != null) 
                        {
                            target.Hit(this, FPCamera.gameObject);
                        }

                        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(impact, 0.2f);
                    }
                }
                
                muzzleFlash.Play(); 
                FPCamera.transform.rotation *= Quaternion.Euler(-gunSettings.recoil, 0, 0);
                yield return new WaitForSeconds(60 / gunSettings.rpm);
                
            }
            yield return new WaitForEndOfFrame();
        }
    }
    
}
