using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileData : MonoBehaviour
{
    [SerializeField]
    int _Damage;
    protected abstract ProjectileTypes _ProjectileTypes { get; }
    public int Damage 
    {
        get => _Damage; 
    }

    [SerializeField]
    float _ProjectileSpeed;

    public float ProjectileSpeed
    { 
        get => _ProjectileSpeed; 
    }

    [SerializeField]
    float _ProjectileLifeTime;

    public float ProjectileLifeTime 
    {
        get => _ProjectileLifeTime; 
    }
    float _currentLifetime;
    private void Update()
    {
        Move(); 
        Live();
    }

    void Move()
    {
        transform.Translate(_ProjectileSpeed * Time.deltaTime * Vector3.forward);
    }
    void Live()
    {
        _currentLifetime += Time.deltaTime;

        if (_currentLifetime >= _ProjectileLifeTime)
            gameObject.SetActive(false);
    }
    private void ResetLifetime() => _currentLifetime = default;
    private void OnDisable() => ResetLifetime();
}
