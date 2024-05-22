using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static List<EnemyManager> currentInstances = new List<EnemyManager>();

    [SerializeField] EnemyContainer enemyPrefab;
    [SerializeField] int maxQuantity;
    [SerializeField] float spawnCooldown;
    [SerializeField] float timeNextBatch;

    List<EnemyContainer> currentEnemies = new List<EnemyContainer>();

    private void Awake()
    {
        if (enemyPrefab) {  return; }
        Destroy(gameObject);
    }

    private void Start()
    {
        currentInstances.Add(this);
        StartCoroutine(SpawnBatch());
    }

    IEnumerator SpawnBatch()
    {
        yield return new WaitForSeconds(spawnCooldown);
        for(int i = 0; i < maxQuantity; ++i)
        {
            EnemyContainer enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.OnDead += EnemyDied;
            currentEnemies.Add(enemy);
        }
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(timeNextBatch);
        StartCoroutine(SpawnBatch());
    }

    private void EnemyDied(EnemyContainer enemy)
    {
        if (!enemy) { return; }
        currentEnemies.Remove(enemy);
        if(currentEnemies.Count == 0) { StartCoroutine(TimeOut()); }
    }

    private void KillAllEnemies()
    {
        EnemyContainer[] enemigos = currentEnemies.ToArray();
        for (int i = 0; i < enemigos.Length; i++)
        {
            Destroy(enemigos[i].gameObject);
        }
    }

    public static void KillAllInstances()
    {
        foreach(EnemyManager manager in currentInstances)
        {
            manager.StopAllCoroutines();
            manager.KillAllEnemies();
        }
        EnemyManager[] managers = currentInstances.ToArray();
        for(int i = 0; i < managers.Length; i++)
        {
            Destroy(managers[i]);
        }
    }

}
