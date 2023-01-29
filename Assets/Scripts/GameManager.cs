using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;

    public GameObject enemySpawner;

    [Header("UI")]
    public GameObject mainMenuScreen;
    public GameObject gameOverScreen;
    public GameObject scoreTxt;
    public GameObject timeTxt;
    public GameObject lifeTxt;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState()
    {
        switch(GMState)
        {
            case GameManagerState.Opening:

                gameOverScreen.SetActive(false);

                mainMenuScreen.SetActive(true);

                AsteroidManager.instance.ClearAsteroid();
                PowerUpManager.instance.ClearPowerUps();

                break;
            case GameManagerState.Gameplay:

                GameObject go = Instantiate(ShipManager.instance.ships[ShipManager.instance.activeShip]);

                player = go;

                scoreTxt.GetComponent<GameScore>().Score = 0;

                mainMenuScreen.SetActive(false);

                player.GetComponent<PlayerControl>().Init();

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                AsteroidManager.instance.ScheduleNextEnemySpawn();

                PowerUpManager.instance.StartSpawnPowerUp();

                timeTxt.GetComponent<TimeCounter>().StartTimeCounter();

                break;

            case GameManagerState.GameOver:

                timeTxt.GetComponent<TimeCounter>().StopTimeCounter();

                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                AsteroidManager.instance.UnscheduleEnemySpawner();

                gameOverScreen.SetActive(true);

                PowerUpManager.instance.isSpawn = false;

                Invoke("ChangeToOpeningState", 8f);

                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
