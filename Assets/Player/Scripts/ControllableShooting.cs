using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableShooting : Shooter
{
    public override event GettingProjectile OnShoot;
    PlayerInput _playerInput;

    protected override void OnReceive()
    {
        base.OnReceive();
        _playerInput = new();
    }
    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Shoot.performed += callbackContext => 
        OnShoot?.Invoke(_data.ProjectileType,gameObject.layer, _shootingPoint.transform.position, _shootingPoint.transform.rotation);
    }

    private void OnDisable() => _playerInput.Disable();
}
