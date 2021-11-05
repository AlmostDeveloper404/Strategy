using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager instance;

    private void Awake()
    {
        if (instance!=null)
        {
            Debug.LogWarning("More than one instance!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion

    public List<Enemy> allEnemies = new List<Enemy>();

    private void Start()
    {
        Enemy[] enemyInStart = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemyInStart.Length; i++)
        {
            AddEnemy(enemyInStart[i]);
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        allEnemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        allEnemies.Remove(enemy);
    }
}
