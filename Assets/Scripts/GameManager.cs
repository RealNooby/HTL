using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemy enemyPrefab;

    public Transform[] spawnPoints;

    public float spawnIntervals;
    private float spawnTimer;
	public Player player;

    public static GameManager instance;
	// Use this for initialization
	void Awake ()
    {
        instance = this;
	}

    private void Start()
    {

        //Spawn enemies here first?
    }

    public void SpawnEnemies(int numOfEnemies = 1)
    {
        for(int i = 0; i < numOfEnemies; i++)
        {
            Enemy _enemy = Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
            _enemy.Init();
        }
        //why is there numofenemies there in the function
        
        //i want to link this spawners to the game manager so i can randomise the spawning from 3 different spawners
        //you only need the spawner positions then right?

    }

    public Vector3 GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0,spawnPoints.Length)].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < spawnIntervals)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            spawnTimer = 0;
            SpawnEnemies();
        }
	}
}
