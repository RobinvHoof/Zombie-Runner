using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour 
{
    [System.Serializable]
    public class AttackSettings 
    {
        [Min(0), Tooltip("Specify how mow damage each hit deals to a target")]
        public float damage = 1;
    }
    
    [SerializeField] public AttackSettings attackSettings;
}
