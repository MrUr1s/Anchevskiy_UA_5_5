using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : AData
{
    [SerializeField] [Range(0, 100)] float _sensivity;
    public float Sensivity => _sensivity;

    public void ScrollProjectile(ProjectileTypes type)
    {
        projectileType = type;
    }

    public override void Death()
    {

    }
}
