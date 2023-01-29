using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager instance;

    public GameObject powerUpPrefab;

    public float timeToSpawn;

    [HideInInspector]
    public bool isSpawn;

    public List<GameObject> powerUpsSpawned;

    private void Awake()
    {
        instance = this;
    }

    public void StartSpawnPowerUp() {
        isSpawn = true;
        Invoke("SpawnPowerUp", timeToSpawn);
    }

    void SpawnPowerUp()
    {
        Debug.Log("SpawnPowerUp");

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject go = Instantiate(powerUpPrefab);
        go.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        powerUpsSpawned.Add(go);

        if (isSpawn) {
            Invoke("SpawnPowerUp", timeToSpawn);
        }
    }

    public void ClearPowerUps()
    {
        if (powerUpsSpawned.Count > 0)
        {
            for (int i = 0; i < powerUpsSpawned.Count; i++)
            {
                Destroy(powerUpsSpawned[i]);
            }
            powerUpsSpawned.Clear();
        }
    }
}
