using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooting : Shooter
{
    public override event GettingProjectile OnShoot;

    EnemyData _enemyData;
    WaitForSeconds _waitAttackRate;
    private void Start()
    {
        _enemyData = _data as EnemyData;
        _waitAttackRate = new WaitForSeconds(_enemyData.AttackRate);
    }
    public void StartShooting() => StartCoroutine(Shoot());

    public void StopShooting() => StopAllCoroutines();
    private IEnumerator Shoot()
    {
        while (true)
        {
            OnShoot?.Invoke(_data.ProjectileType,gameObject.layer, _shootingPoint.transform.position, _shootingPoint.transform.rotation);
            yield return _waitAttackRate;
        }
    }
}
