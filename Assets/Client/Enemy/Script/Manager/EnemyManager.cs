using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = null;

    private SpawnPoint[] _spawnPoints = null;

    private List<Enemy> enemyList = null;
    private Action<int> onEnemyDie = null;
    
    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        enemyList = new List<Enemy>();
    }

    public void Init(Player player, Action<int> onEnemyDie)
    {
        StartCoroutine(CoSpawnEnemies(player));
        this.onEnemyDie = onEnemyDie;
    }

    private IEnumerator CoSpawnEnemies(Player player)
    {
        while (true)
        {
            if (enemyList.Count < 5)
            {
                SpawnEnemy(player);
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }

    private void SpawnEnemy(Player player)
    {
        // Enemy를 소환하려면
        // Prefab을 등록하고 GameObject를 소환한 다음에
        // Enemy 컴포넌트를 가져오고
        // Init 함수 호출해야 함
        
        int randomIndex = Random.Range(0, _spawnPoints.Length);
        Vector3 spawnPosition = _spawnPoints[randomIndex].transform.position;
        Quaternion spawnRotation = _spawnPoints[randomIndex].transform.rotation;
        
        GameObject go = Instantiate(_enemyPrefab, spawnPosition, spawnRotation);
        Enemy enemyComponent = go.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.Init(player, OnEnemyDie);
        }
        enemyList.Add(enemyComponent);
    }

    private void OnEnemyDie(Enemy enemy, int score)
    {
        RemoveEnemy(enemy);
        onEnemyDie?.Invoke(score);
    }

    private void RemoveEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }
}