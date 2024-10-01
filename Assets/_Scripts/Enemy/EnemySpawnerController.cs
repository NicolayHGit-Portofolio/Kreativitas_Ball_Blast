using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private List<EnemyController> _enemyPrefab;
    [SerializeField] private List<Transform> _enemySpawners;

    public void SpawnEnemy(EnemyType typeUnlocked, int maxHealth)
    {
        int randTypeEnemy = Random.Range(0,(int)typeUnlocked);

        int rand = Random.Range(0, _enemySpawners.Count);
        int randHealth = Random.Range(1, maxHealth);
        int health = (randHealth > 1 && randHealth % 2 != 0) ? randHealth + 1 : randHealth;

        Vector2 randDirection = new(Random.Range(-1f, 1f) * 1.2f, -1f);

        Transform enemySpawner = _enemySpawners[rand];

        var enemy = Instantiate(_enemyPrefab[randTypeEnemy], enemySpawner.position, Quaternion.identity);     
        enemy.Spawn(health, randDirection);

        GameManager.Instance.RegisterEnemyScene(enemy);
    }
}
