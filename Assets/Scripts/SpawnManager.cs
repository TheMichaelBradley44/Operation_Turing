using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject UnitEnemy;  
    public Transform[] spawnPoints; 
    public int waveNumber = 1;     
    private int enemiesToSpawn;    

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        CheckForNextWave();
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        this.enemiesToSpawn = enemiesToSpawn;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];
            GameObject newEnemy = Instantiate(UnitEnemy, spawnPoint.position, spawnPoint.rotation);

            activeEnemies.Add(newEnemy);

            HealthSystem healthSystem = newEnemy.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.OnDead += OnEnemyKilled;
            }
        }
    }

    void OnEnemyKilled(object sender, System.EventArgs e)
    {
        GameObject deadEnemy = (sender as HealthSystem).gameObject;
        activeEnemies.Remove(deadEnemy);

        Destroy(deadEnemy);
    }

    void CheckForNextWave()
    {
        if (activeEnemies.Count == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }
}
