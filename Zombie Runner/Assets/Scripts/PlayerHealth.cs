using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IAttackable
{
    [SerializeField] public float startingHealth = 100f;

    public float health {get; private set;}
    void Start()
    {
        health = startingHealth;
    }

    public void Hit(Attack attack, GameObject source)
    {
        health -= attack.attackSettings.damage;
    }
}