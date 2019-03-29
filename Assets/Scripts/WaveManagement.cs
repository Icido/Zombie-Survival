using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagement : MonoBehaviour {

    [SerializeField]
    private int waveNumber = 0;

    private List<GameObject> SmartZombies = new List<GameObject>();
    private List<GameObject> RegularZombies = new List<GameObject>();

    [SerializeField]
    private int numSZombies;

    [SerializeField]
    private int numRZombies;

    public GameObject RegularZombie;

    public GameObject SmartZombie;

    private bool allDead = true;

    private bool finishedSZombieSpawning = false;
    private bool finishedRZombieSpawning = false;

    private GAController gaC;

    // Use this for initialization
    void Start() {
        gaC = GetComponent<GAController>();
        SmartZombies.Clear();
        RegularZombies.Clear();
    }

    // Update is called once per frame
    void Update() {

        //Check if all current spawned enemies are dead (when dead, they remove themself from the list and delete self
        if (SmartZombies.Count == 0 && RegularZombies.Count == 0 &&
            finishedSZombieSpawning && finishedRZombieSpawning)
        {
            allDead = true;
            gaC.multipleGenerationEpoch();
            waveNumber++;
        }

        //If all are dead, instantiate a new wave
        if (allDead)
        {
            newWave(waveNumber);
            allDead = false;
        }
    }

    void newWave(int waveNumber)
    {
        numRZombies = Mathf.RoundToInt(Mathf.Pow(Mathf.RoundToInt(waveNumber * 1.5f), 1.5f));
        numSZombies = Mathf.RoundToInt(Mathf.Pow(waveNumber, 1.5f));

        Transform RTransform = GameObject.Find("Regular Zombies").transform;
        Transform STransform = GameObject.Find("Smart Zombies").transform;

        Transform SpawnLocations = GameObject.Find("Spawn Locations").transform;

        finishedSZombieSpawning = false;
        finishedRZombieSpawning = false;


        StartCoroutine(spawnZombie(true, numRZombies, SpawnLocations, RTransform));

        StartCoroutine(spawnZombie(false, numSZombies, SpawnLocations, STransform));

    }

    public void zombieDeath(GameObject zombie)
    {
        if(SmartZombies.Contains(zombie))
        {
            SmartZombies.Remove(zombie);
            return;
        }
        else if(RegularZombies.Contains(zombie))
        {
            RegularZombies.Remove(zombie);
            return;
        }

        Debug.LogError("zombieDeath function called incorrectly!", this);

    }

    IEnumerator spawnZombie(bool isRegular, int numberOfEnemies, Transform spawnLocations, Transform enemyContainer)
    {
        if(isRegular)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                RegularZombies.Add(Instantiate(RegularZombie, spawnLocations.GetChild(Random.Range(0, spawnLocations.childCount)).position, Quaternion.identity, enemyContainer));

                yield return new WaitForSeconds(1);
            }

            finishedRZombieSpawning = true;
        }
        else
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                SmartZombies.Add(Instantiate(SmartZombie, spawnLocations.GetChild(Random.Range(0, spawnLocations.childCount)).position, Quaternion.identity, enemyContainer));

                yield return new WaitForSeconds(5);
            }

            finishedSZombieSpawning = true;
        }

        
    }


}
