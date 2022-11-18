using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : Receiver<IShoot>
{
    public abstract event GettingProjectile OnShoot;
    protected ShootingPoint _shootingPoint;

    protected override void OnReceive()
    {
        base.OnReceive();
        _shootingPoint = GetComponentInChildren<ShootingPoint>();
    }

}
