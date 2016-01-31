using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawn : MonoBehaviour
{
    public bool PlacedItems = true;
    public List<GameObject> Items;
    public List<Transform> SpawnPoints;
    public Spawner spawner;
    private List<Transform> constSpawnPoints;
    private List<Transform> alreadyInUse;
    int chosedItem;
    int chosedSpawn;


    void Awake()
    {
        constSpawnPoints = SpawnPoints;
        alreadyInUse = new List<Transform>();
    }

    void Update()
    {
        if (PlacedItems)
        {
            PlacedItems = false;
            randomSpawn();
            resetList();
        }
    }

    private void resetList()
    {
        SpawnPoints = constSpawnPoints;
        alreadyInUse.Clear();
    }
    private void randomSpawn()
    {
        for (int i = 0; i <= spawner.CurrentWave.Number-1; i++)
        {
            if (SpawnPoints.Count == 0)
                return;

            chosedSpawn = Random.Range(0, SpawnPoints.Count);
            chosedItem = Random.Range(0, Items.Count);

            Instantiate(Items[chosedItem], SpawnPoints[chosedSpawn].position, Quaternion.identity);
            alreadyInUse.Add(SpawnPoints[chosedSpawn]);
            SpawnPoints.Remove(SpawnPoints[chosedSpawn]);
        }
    }
}
