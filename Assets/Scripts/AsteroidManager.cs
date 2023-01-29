using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager instance;

    public GameObject[] asteroid;

    public List<GameObject> asteroidSpawned;

    private void Awake()
    {
        instance = this;
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject go = (GameObject)Instantiate(asteroid[Random.Range(0,asteroid.Length)]);
        go.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        asteroidSpawned.Add(go);

        ScheduleNextEnemySpawn();
    }

    public void ClearAsteroid() {
        if (asteroidSpawned.Count > 0) {
            for (int i = 0; i < asteroidSpawned.Count; i ++) {
                Destroy(asteroidSpawned[i]);
            }
            asteroidSpawned.Clear();
        }
    }

    public void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;

        spawnInNSeconds = Random.Range(5, 10);

        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
    }
}
