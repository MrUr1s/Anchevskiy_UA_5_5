using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : APool<EnemyData>
{
    Action _spawning;
    GettingProjectile _gettingProjectile;
    PlayerData _target;
    public EnemiesPool(EnemyData prefab, Action spawning, GettingProjectile gettingProjectile, Transform parent, PlayerData target, int count = 1) : base(prefab, parent)
    {
        _spawning = spawning;
        _gettingProjectile= gettingProjectile;
        _target=target;
        Init(count);
    }

    protected override EnemyData GetCreated()
    {
        EnemyData enemy = UnityEngine.Object.Instantiate(_prefab);
        enemy.SetTarget(_target);
        switch (_prefab.EnemyType)
        {
            case EnemyTypes.Small:
                break;
            case EnemyTypes.Medium:
                break;
            case EnemyTypes.Big:
                break;
        }
        Healther healther = enemy.GetComponent<Healther>();
        TargetDetection targetDetection=enemy.GetComponent<TargetDetection>();        
        AutoShooting shooter = enemy.GetComponent<AutoShooting>();
        AIController aIController = enemy.GetComponent<AIController>();
        Aimer aimer = enemy.GetComponent<Aimer>();

        healther.OnHealthChange+= enemy.GetComponentInChildren<HealthBar>().Redraw;
        healther.OnDead += enemy.Death;
        healther.OnDead += _spawning;

        targetDetection.OnDetect += aIController.StartFollowing;
        targetDetection.OnDetect += aimer.StartAiming;
        targetDetection.OnDetect += shooter.StartShooting;

        targetDetection.OnLost += aIController.StopFollowing;
        targetDetection.OnLost+= aimer.StopAiming;
        targetDetection.OnLost += shooter.StopShooting;
        shooter.OnShoot += _gettingProjectile;

        return enemy;
    }
}
