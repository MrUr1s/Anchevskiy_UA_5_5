using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate ProjectileData GettingProjectile(ProjectileTypes projectileType,int layer, Vector3 shootingPosition, Quaternion shootingRotation);

public class GameStartup : MonoBehaviour
{
    Dictionary<ProjectileTypes, ProjectilePool> _projectiles;
    Dictionary<EnemyTypes, EnemiesPool> _enemies;

    [SerializeField] private Transform _entityContainer;

    [SerializeField] private EnemyData _prefabEnemyBig;
    [SerializeField] private EnemyData _prefabEnemyMedium;
    [SerializeField] private EnemyData _prefabEnemySmall;

    [SerializeField] private ProjectileData _prefabProjectileRockets;
    [SerializeField] private ProjectileData _prefabProjectileBullets;

    [SerializeField] PlayerData _prefabPlayer;

    [SerializeField] EnemySpawnPoint[] _enemySpawnPoints;
    [SerializeField] Transform _characterSpawnPoint;

    private void Awake()
    {
        _entityContainer = this.transform;
        _enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();

    }
    private void Start()
    {
        SpawnCharacter();
        InstantiatePoolEnemyes();
        InstantiatePoolProjectile();
    }

    private void SpawnCharacter()
    {
        _prefabPlayer = Instantiate(_prefabPlayer, _characterSpawnPoint.position, _characterSpawnPoint.rotation, _entityContainer);
        _prefabPlayer.GetComponent<ControllableShooting>().OnShoot += GetProjectile;
        _prefabPlayer.GetComponent<Healther>().OnHealthChange += _prefabPlayer.GetComponentInChildren<HealthBar>().Redraw;
    }

    private ProjectileData GetProjectile(ProjectileTypes projectileType,int layer, Vector3 shootingPosition, Quaternion shootingRotation)
    {
       var projectile = _projectiles[projectileType].Spawn(shootingPosition, shootingRotation);
        projectile.GetComponent<Hitter>().SetLayer(layer);
        return projectile;
    }

    private void InstantiatePoolProjectile()
    {
        _projectiles = new()
        {
            { ProjectileTypes.Bullets, new ProjectilePool(_prefabProjectileBullets, _entityContainer, 10) },
            { ProjectileTypes.Rockets, new ProjectilePool(_prefabProjectileRockets, _entityContainer, 10) }
        };
    }

    private void InstantiatePoolEnemyes()
    {
        _enemies = new()
        {
            { EnemyTypes.Big, new EnemiesPool(_prefabEnemyBig, SpawnEnemy, GetProjectile, _entityContainer, _prefabPlayer, 5) },
            { EnemyTypes.Medium, new EnemiesPool(_prefabEnemyMedium, SpawnEnemy, GetProjectile, _entityContainer, _prefabPlayer, 5) },
            { EnemyTypes.Small, new EnemiesPool(_prefabEnemySmall, SpawnEnemy, GetProjectile, _entityContainer, _prefabPlayer, 5) }
        };
        foreach (var spawnPoint in _enemySpawnPoints)
            SpawnEnemy(spawnPoint.transform);
    }
    private void SpawnEnemy(Transform spawnPoint)
    {
        _enemies[RandomEnemyTypes()].Spawn(spawnPoint.position);
    }
    EnemyTypes RandomEnemyTypes()
    {
        Array types = typeof(EnemyTypes).GetEnumValues();
        return (EnemyTypes)types.GetValue(UnityEngine.Random.Range(0, types.Length));
    }


    private void SpawnEnemy()
    {
        Transform randomSpawnPoint = _enemySpawnPoints[UnityEngine.Random.Range(0, _enemySpawnPoints.Length)].transform;
        _enemies[RandomEnemyTypes()].Spawn(randomSpawnPoint.position);
    }

}
