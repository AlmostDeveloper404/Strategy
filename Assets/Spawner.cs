using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyPref;

    public float TimeToSpawn;

    EnemyManager enemyManager;

    float _timer;


    private void Start()
    {
        enemyManager = EnemyManager.instance;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer>TimeToSpawn)
        {
            _timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject newEnemy= Instantiate(EnemyPref, transform.position, transform.rotation);
        enemyManager.AddEnemy(newEnemy.GetComponent<Enemy>());
    }
}
