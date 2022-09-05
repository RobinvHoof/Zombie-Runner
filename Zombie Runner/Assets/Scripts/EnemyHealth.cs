using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IAttackable
{
    [Min(0)]
    [SerializeField] public float startHealt = 100;
    
    public float health {get; private set;}

    void Start()
    {
        health = startHealt;
    }

    void Update() {
        if (health <= 0)
            Destroy(gameObject);
    }

    public void Hit(Attack attack, GameObject source)
    {
        health -= attack.attackSettings.damage;
    }
}
