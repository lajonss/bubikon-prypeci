using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.VR.WSA.Input;
using Random = UnityEngine.Random;

public class GameEnemySpawner : MonoBehaviour
{
    #region serialize fields variables

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject fpsController;
    [SerializeField] private float spawnTime = 1.5f;
    [SerializeField] private int incrementEnemiesPerWave = 4;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] checkpointPoints;

    #endregion

    #region private variables

    private int enemiesNumber;
    private int waveNumber;
    private int enemiesToSpawn;
    private int spawnedEnemies = 0;
    private float testScore = 0.0f;
    private Boolean nextRound = false;
    private Boolean incrementWave = true;
    private Boolean isGameRunning = false;

    #endregion

    void Start()
    {
        enemiesNumber = 0;
        waveNumber = 0;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Update()
    {
        enemiesNumber = GameObject.FindObjectsOfType(typeof(EnemyAIGame_03)).Length;
        if (enemiesNumber == 0 && isGameRunning)
        {
            if (incrementWave == true)
            {
                incrementWave = false;
                waveNumber++;
            }
            nextRound = true;
            spawnedEnemies = 0;
            enemiesToSpawn = incrementEnemiesPerWave * waveNumber;
        }
        testScore = fpsController.GetComponent<getDmg>().hp;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(200, 100, 200, 50), "Bubikons enemies: " + enemiesNumber.ToString());
        GUI.Label(new Rect(200, 150, 200, 50), "Question wave: " + waveNumber.ToString());
        GUI.Label(new Rect(200, 200, 200, 50), "Test score: " + testScore);
    }

    void Spawn()
    {        
        if (nextRound)
        {            
            if (spawnedEnemies < enemiesToSpawn)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                var instantiate = Instantiate(enemy, spawnPoints[spawnPointIndex].position,
                    spawnPoints[spawnPointIndex].rotation);
                instantiate.SendMessage("TheStart", checkpointPoints);
                spawnedEnemies++;
            }
            else
            {
                nextRound = false;
                incrementWave = true;
            }
        }

    }

    public void startSpawning()
    {
        isGameRunning = true;
    }
}