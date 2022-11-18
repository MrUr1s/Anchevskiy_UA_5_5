using System;
using System.Collections.Generic;
using UnityEngine;

public class SwitchProjectile : Receiver<PlayerData>
{
    int _currentIndexProjectile=0;
    PlayerInput _playerInput;

    protected override void OnReceive()
    {
        base.OnReceive();
        _playerInput = new();
    }
    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.ScrollProjectile.performed += ScrollProjectile_performed;
    }

    private void ScrollProjectile_performed(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
    {
        _currentIndexProjectile+=(int)callbackContext.ReadValue<Vector2>().y;
        _currentIndexProjectile=Mathf.Clamp(_currentIndexProjectile, 0,Enum.GetNames(typeof(ProjectileTypes)).Length-1);
        _data.ScrollProjectile( (ProjectileTypes) _currentIndexProjectile);
    }
    private void OnDisable()
    {
        _playerInput.Enable();
        _playerInput.Player.ScrollProjectile.performed -= ScrollProjectile_performed;
    }
}
