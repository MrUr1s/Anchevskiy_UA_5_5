using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public abstract class AData : MonoBehaviour, IShoot, IHealth
{
    public int health;
    public float speed;

    [SerializeField] protected ProjectileTypes projectileType;
    public ProjectileTypes ProjectileType => projectileType;

    public int MaxHealth;
    public int maxHealth => MaxHealth;
     void Awake()
    {
        health = maxHealth;
    }
    protected int Health 
    { 
        get => health;
        set 
        {
            health = Mathf.Clamp(value,0,maxHealth);
            if (health <= 0)
                Death();
        }
    }

    public abstract void Death();

}
