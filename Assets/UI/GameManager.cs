using Assets.Vehicles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static bool gameStarted = false;

    public static float score = 0;

    public static int level = 1;

    public PlayerShip player;

    public GameObject basicEnemyPrefab;
    public GameObject laserEnemyPrefab;
    public GameObject wallEnemyPrefab;

    public static int enemiesActive = 0;

    public static bool waitingForLevelTimeout = false;
    public static float betweenLevelTimer = 0;
    public const float betweenLevelTimeout = 5.0f;

    public static float enemyHitMultiplier = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        enemyHitMultiplier = Mathf.Clamp01(enemyHitMultiplier - Time.deltaTime);

        if (gameStarted && enemiesActive <= 0 && !waitingForLevelTimeout)
        {
            level++;
            waitingForLevelTimeout = true;
            betweenLevelTimer = 0.0f;
        }

        if (waitingForLevelTimeout)
        {
            betweenLevelTimer += Time.deltaTime;
            if (betweenLevelTimer >= betweenLevelTimeout)
            {
                waitingForLevelTimeout = false;
                SpawnEnemiesForLevel(level);
            }
        }

        if (player.health <= 0)
        {
            EndGame();
        }
    }

    public static void StartGame()
    {
        level = 0;
        score = 0;
        gameStarted = true;
    }

    public static void EndGame()
    {
        gameStarted = false;
    }

    public static void EnemyDied(float escore)
    {
        score += escore;
        enemiesActive--;
        enemyHitMultiplier = 1.0f;
    }

    Vector3 RandomSpawnPosition()
    {
        float radialPosition = Random.Range(0, Mathf.PI * 2);
        float height = Random.Range(0.5f, 2.0f);

        return new Vector3(2.5f * Mathf.Cos(radialPosition), height, 2.5f * Mathf.Sin(radialPosition));
    }

    void SpawnEnemiesForLevel(int level)
    {
        int basicEnemies = level + 5;
        int laserEnemies = level - 3;

        if (basicEnemies < 0) basicEnemies = 0;
        if (laserEnemies < 0) laserEnemies = 0;

        enemiesActive = basicEnemies + laserEnemies;

        for (int i = 0; i < basicEnemies; i++)
        {
            GameObject gameObject = Instantiate(basicEnemyPrefab, RandomSpawnPosition(), Quaternion.identity);

            gameObject.transform.LookAt(player.transform);
        }

        for (int i = 0; i < laserEnemies; i++)
        {
            GameObject gameObject = Instantiate(laserEnemyPrefab, RandomSpawnPosition(), Quaternion.identity);

            gameObject.transform.LookAt(player.transform);
        }
    }
}
