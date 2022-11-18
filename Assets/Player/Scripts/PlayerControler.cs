using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : Receiver<PlayerData>
{
    PlayerInput _playerInput;
    CharacterController _characterController;
    Coroutine _moveCoroutine;
    Camera _camera;
    float _angleX;

    protected override void OnReceive()
    {
        base.OnReceive();
        _characterController = GetComponent<CharacterController>();
        _playerInput = new();
        _camera = Camera.main;
    }
    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Motion.started += callbackContext => _moveCoroutine = StartCoroutine(Move(callbackContext));
        _playerInput.Player.Motion.canceled += callbackContext =>  StopCoroutine(_moveCoroutine);
        _playerInput.Player.Look.performed += callbackContext => Look(callbackContext);
    }

    private void Look(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
    {
        var input= callbackContext.ReadValue<Vector2>();
        Vector3 look = new Vector3(-input.y, input.x);

        _angleX += look.x * _data.Sensivity * Time.deltaTime;
        _angleX = Mathf.Clamp(_angleX, -89, 89);

        _camera.transform.localRotation = Quaternion.Euler(_angleX * Vector3.right);
        transform.Rotate(_data.Sensivity * Time.deltaTime * look.y * Vector3.up);
    }

    private IEnumerator Move(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
    {
       while (true)
        {
            var moveInput=callbackContext.ReadValue<Vector2>();
            _characterController.Move(transform.TransformDirection(_data.speed*Time.deltaTime*new Vector3(moveInput.x,0,moveInput.y)));
            yield return null;
        }
    }
}
