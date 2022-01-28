using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public string nextLevelName;
    public int alarmLevels;
    public List<GameObject> possibleChunkPrefabs;
    public List<GameObject> weaponPrefabs;
    public GameObject antiquePrefab;
    public GameObject guardPrefab;
    public List<GameObject> swarmerTypes;
    public float ratioOfAntiquePlinths;
    public int numberOfGuardsToCreate;
    public int numberOfSpawnersToCreate;
    public int widthInChunks;
    public int lengthInChunks;

    private void Awake()
    {
        References.levelGenerator = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < lengthInChunks; j++)
        {
            for (int i = 0; i < widthInChunks; i++)
            {
                // Get a random chunk type
                int randomChunkIndex = Random.Range(0, possibleChunkPrefabs.Count);
                GameObject randomChunkType = possibleChunkPrefabs[randomChunkIndex];
                float chunkOffset = System.Math.Abs(transform.position.x);
                Vector3 spawnPosition = transform.position + new Vector3(i * chunkOffset, 0, j * 32);
                Instantiate(randomChunkType, spawnPosition, Quaternion.identity);
            }
        }

        // Spawn antiques and weapons
        int numberOfThingsToPlace = References.plinths.Count;
        int numberOfAntiquesToPlace = Mathf.RoundToInt(numberOfThingsToPlace * ratioOfAntiquePlinths);

        foreach (PlinthBehaviour plinth in References.plinths)
        {
            GameObject thingToCreate;
            
            // Increase the chance of placing an Antique to 100% as more other items are placed
            float chanceOfAntique = numberOfAntiquesToPlace / numberOfThingsToPlace;

            // If Random.value == 0.9, and you check less than, you have a 90% chance of placing an antique
            if (Random.value < chanceOfAntique) // The bigger this is, the better the chance this passes
            {
                // Place an antique
                thingToCreate = antiquePrefab;
                numberOfAntiquesToPlace--;      // Reduce the number of antiques left
            }
            else
            {
                // Place a weapon
                int randomThingIndex = Random.Range(0, weaponPrefabs.Count);   // Get random weapon from list
                thingToCreate = weaponPrefabs[randomThingIndex];    // Get the type of thing at that index
            }

            numberOfThingsToPlace--;                            // Reduce the number of all things left
            GameObject newThing = Instantiate(thingToCreate);
            plinth.AssignItem(newThing);                        // Assign that thing to the plinth
        }

        // Spawn guards
        List<NavPoint> possibleSpots = new List<NavPoint>();
        float minDistanceFromPlayer = 12;
        foreach (NavPoint nav in References.navPoints)
        {
            // Is this starting navpoint far enough from the player to spawn a guard?
            if (Vector3.Distance(nav.transform.position, References.thePlayer.transform.position) >= minDistanceFromPlayer)
            {
                // Add that nav point to the possible spawn point spots list
                possibleSpots.Add(nav);
            }
        }

        for (int i = 0; i < numberOfGuardsToCreate; i++)
        {
            if (possibleSpots.Count == 0)
            {
                break;
            }
            int randomIndex = Random.Range(0, possibleSpots.Count);
            NavPoint spotToSpawnAt = possibleSpots[randomIndex];
            Instantiate(guardPrefab, spotToSpawnAt.transform.position, Quaternion.identity);
            possibleSpots.Remove(spotToSpawnAt);
        }

        // Cull spawners
        while (References.spawners.Count > numberOfSpawnersToCreate)
        {
            // Get the index of a random spawner from the list of spawners in References
            int randomIndex = Random.Range(0, References.spawners.Count);

            // Destroy that spawner's gameObject
            Destroy(References.spawners[randomIndex].gameObject);
        }

        References.alarmManager.SetUpLevel(alarmLevels);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
