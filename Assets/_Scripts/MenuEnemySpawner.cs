using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public float spawnTime = 1.5f;
    public Transform[] spawnPoints;
    public Transform[] checkpointPoints;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        var instantiate = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        instantiate.SendMessage("TheStart", checkpointPoints);
    }
}
