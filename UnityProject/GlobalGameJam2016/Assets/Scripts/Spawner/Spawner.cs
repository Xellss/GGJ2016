using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour 
{
    [HideInInspector]
    public bool Spawning = true;
    [Tooltip("If Checked it Chooses Random Transforms to Spawn at, if not it chooses them by order")]
    public bool SpawnRandomOrder = true;
    [Tooltip("If checked it Chooses Random Enemy from The Enemys list, if not it chooses the first")]
    public bool SpawnRandomEnemy = false;
    [Tooltip("Fire this to start a Wave")]
    public bool StartSpawning = false;

    [HideInInspector]
    public Wave CurrentWave;

    public List<Transform> SpawnPositions = new List<Transform>();
    public List<GameObject> Enemys = new List<GameObject>();
    public List<Wave> Waves = new List<Wave>();

    private int waveIndex = 0;
    private int orderIndex;

    [HideInInspector]
    public List<GameObject> AliveEnemys = new List<GameObject>();

	void Start() 
    {
        if (StartSpawning)
            Spawning = true;

        CurrentWave = Waves[0];
	}
	
	void Update() 
    {
        CheckForSpawning();
	}

    public void NextWave()
    {
        waveIndex++;
        if (waveIndex > Waves.Count)
            print("Not Enough Waves");

        CurrentWave = Waves[waveIndex];

        StartCoroutine(SpawnTimer());
        Spawning = true;
    }

    private void CheckForSpawning()
    {
        if (StartSpawning)
        {
            StartSpawning = false;
            StartCoroutine(SpawnTimer());
        }
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(CurrentWave.SpawnDelay);
            StartCoroutine(SpawnTimer());
            Spawn();
    }

    private void Spawn()
    {
        if (SpawnRandomOrder)
        {
            if (SpawnRandomEnemy)
            {
                GameObject enemy = ChooseRandomEnemy();
                Instantiate(enemy, ChooseRandomPosition().position, Quaternion.identity);
                AliveEnemys.Add(enemy);
            }
            else
            {
                GameObject enemy = ChooseEnemy();
                Instantiate(enemy, ChooseRandomPosition().position, Quaternion.identity);
                AliveEnemys.Add(enemy);
            }
        }
        else
        {
            if (SpawnRandomEnemy)
            {
                GameObject enemy = ChooseRandomEnemy();
                Instantiate(enemy, ChooseNextPosition().position, Quaternion.identity);
                AliveEnemys.Add(enemy);
            }
            else
            {
                GameObject enemy = ChooseEnemy();
                Instantiate(enemy, ChooseNextPosition().position, Quaternion.identity);
                AliveEnemys.Add(enemy);
            }
        }
    }

    private GameObject ChooseRandomEnemy()
    {
        int index = Random.Range(0, Enemys.Count + 1);

        return Enemys[index];
    }

    private GameObject ChooseEnemy()
    {
        return Enemys[0];
    }

    private Transform ChooseRandomPosition()
    {
        int index = Random.Range(0, SpawnPositions.Count + 1);

        return SpawnPositions[index];
    }

    private Transform ChooseNextPosition()
    {
        orderIndex++;

        if (orderIndex > SpawnPositions.Count)
            orderIndex = 0;

        return SpawnPositions[orderIndex];
    }
}

