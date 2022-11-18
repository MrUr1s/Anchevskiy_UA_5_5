using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : AData
{
    [SerializeField] [Range(1, 10)]  float _spawnDelay;
    [SerializeField] [Range(0, 100)] float _detectionDistance;
    [SerializeField] [Range(0, 100)] float _attackRate;
    [SerializeField] [Range(0, 100)]  float _stoppingDistance;

    public float AttackRate => _attackRate / 60;
    public abstract EnemyTypes EnemyType { get; }
    public AData Target { get; private set; }
    public float SpawnDelay { get => _spawnDelay; set => _spawnDelay = value; }
    public float DetectionDistance { get => _detectionDistance; set => _detectionDistance = value; }
    public float StoppingDistance { get => _stoppingDistance; set => _stoppingDistance = value; }

    public void SetTarget(AData target) => Target = target;
    public override void Death()
    {
        this.gameObject.SetActive(false);
    }

  
}