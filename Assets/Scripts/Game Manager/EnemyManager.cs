using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static List<EnemyManager> currentInstances;

    [SerializeField] Enemigo enemy;
    [SerializeField] int maxQuantity;
    [SerializeField] float spawnCooldown;
    [SerializeField] float timeNextBatch;

    List<Enemigo> currentEnemies = new List<Enemigo>();

    private void Awake()
    {
        if (!enemy) { Destroy(gameObject); }
        currentInstances.Add(this);
    }

    private void Start()
    {
        StartCoroutine(SpawnBatch());
    }

    IEnumerator SpawnBatch()
    {
        yield return new WaitForSeconds(spawnCooldown);
        for(int i = 0; i < maxQuantity; ++i)
        {
            Enemigo _enemigo = Instantiate(enemy);
            _enemigo.OnDead += EnemyDied;
            currentEnemies.Add(enemy);
        }
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(timeNextBatch);
        StartCoroutine(SpawnBatch());
    }

    private void EnemyDied()
    {
        for(int i = 0;i < currentEnemies.Count; ++i)
        {
            if (currentEnemies[i] == null) { currentEnemies.RemoveAt(i); }
        }
        if(currentEnemies.Count == 0) { StartCoroutine(TimeOut()); }
    }

    private void KillAllEnemies()
    {
        Enemigo[] enemigos = currentEnemies.ToArray();
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
