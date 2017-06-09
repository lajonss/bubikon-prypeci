using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float spawnTime = 1.5f;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] checkpointPoints;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        var instantiate = Instantiate(enemy, spawnPoints[spawnPointIndex].position,
            spawnPoints[spawnPointIndex].rotation);
        instantiate.SendMessage("TheStart", checkpointPoints);
    }
}