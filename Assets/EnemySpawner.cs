using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemyPrefab;
    public GameObject spawnPoint;
    public float secondsBetweenSpawns;
    float secondsSinceLastSpawn;
    public int enemiesToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastSpawn = 0;
        int randomEnemyIndex = Random.Range(0, References.levelGenerator.swarmerTypes.Count);
        enemyPrefab = References.levelGenerator.swarmerTypes[randomEnemyIndex];
    }
    private void OnEnable()
    {
        References.spawners.Add(this);
    }

    private void OnDisable()
    {
        References.spawners.Remove(this);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (References.alarmManager.AlarmHasSounded() && enemiesToSpawn > 0)
        {
            secondsSinceLastSpawn += Time.fixedDeltaTime;
            if (secondsSinceLastSpawn >= secondsBetweenSpawns)
            {
                GameObject newSpawn = Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                Rigidbody newSpawnBody = newSpawn.GetComponent<Rigidbody>();
                newSpawnBody.velocity = new Vector3(2, 0, 0);
                enemiesToSpawn--;
                secondsSinceLastSpawn = 0;
            }
        }
    }
}
