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

    private bool gaFinished = true;

    private GAController gaC;
    private GameObject player;
    private PlayerShooting pShooting;
    private PlayerHealth pHealth;

    // Use this for initialization
    void Start() {
        gaC = GetComponent<GAController>();
        player = GameObject.Find("Player");
        SmartZombies.Clear();
        RegularZombies.Clear();


        pShooting = player.GetComponentInChildren<PlayerShooting>();
        pHealth = player.GetComponent<PlayerHealth>();
        gaC.multipleGenerationEpoch(pShooting, pHealth);


    }

    // Update is called once per frame
    void Update() {

        if (!gaFinished && finishedSZombieSpawning && finishedRZombieSpawning)
        {
            StartCoroutine(generationEpoch());
        }


        //Check if all current spawned enemies are dead (when dead, they remove themself from the list and delete self
        if (SmartZombies.Count == 0 && RegularZombies.Count == 0 &&
            finishedSZombieSpawning && finishedRZombieSpawning && gaFinished)
        {
            gaFinished = false;
            waveNumber++;
            newWave(waveNumber);
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

                //Add zombie
                GameObject newZombie = Instantiate(SmartZombie, spawnLocations.GetChild(Random.Range(0, spawnLocations.childCount)).position, Quaternion.identity, enemyContainer);

                SmartZombie smartZombieCopy = gaC.SortedZombies[0];

                //Request generation data
                newZombie.GetComponent<EnemyHealth>().startingHealth = smartZombieCopy.attributes.health;
                newZombie.GetComponent<EnemyHealth>().currentHealth = smartZombieCopy.attributes.health;

                newZombie.GetComponent<EnemyMovement>().speed = smartZombieCopy.attributes.speed;

                newZombie.GetComponent<EnemyAttack>().useMelee = smartZombieCopy.useMelee;

                if (smartZombieCopy.useMelee)
                {
                    //modify attack speed, damage and collider range for MELEE
                    newZombie.GetComponent<EnemyAttack>().attackDamage = smartZombieCopy.attributes.meleeStrength;
                    newZombie.GetComponent<EnemyAttack>().timeBetweenAttacks = 1 / smartZombieCopy.attributes.meleeAttackRate;
                    newZombie.GetComponent<EnemyAttack>().range = smartZombieCopy.attributes.meleeRange;
                }
                else
                {
                    //modify attack speed, damage and collider range for RANGE
                    newZombie.GetComponent<EnemyAttack>().attackDamage = smartZombieCopy.attributes.rangeStrength;
                    newZombie.GetComponent<EnemyAttack>().timeBetweenAttacks = 1 / smartZombieCopy.attributes.rangeAttackRate;
                    newZombie.GetComponent<EnemyAttack>().range = smartZombieCopy.attributes.rangeRange;
                }

                //TODO: Put in "leadershipStr/Rng" for flocking and basic zombie decision making



                //Modify this zombie with generation data

                SmartZombies.Add(newZombie);

                yield return new WaitForSeconds(5);
            }

            finishedSZombieSpawning = true;
        }

        
    }

    IEnumerator generationEpoch()
    {
        pShooting = player.GetComponentInChildren<PlayerShooting>();
        pHealth = player.GetComponent<PlayerHealth>();
        gaC.multipleGenerationEpoch(pShooting, pHealth);
        gaFinished = true;
        yield return null;
    }
}
