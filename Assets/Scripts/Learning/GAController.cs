using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GAController : MonoBehaviour {

    private int generationalGap = 10;
    private GeneticAlgorithm ga;

    public List<SmartZombie> OutputZombies = new List<SmartZombie>();
    public List<SmartZombie> SortedZombies = new List<SmartZombie>();
    

    // Use this for initialization
	void Start () {
        ga = new GeneticAlgorithm();
        ga.createStartPopulation();
    }

    public void multipleGenerationEpoch(PlayerShooting shooting, PlayerHealth health)
    {
        if (ga.hasFoundPerfection)
        {
            //Output "best zombie" + possible mutations
        }
        else
        {
            ga.currentBestZombies.Clear();

            for (int i = 0; i < generationalGap; i++)
            {
                ga.epoch(shooting, health);
            }

            //Sort ga.currentBestZombies by fitness score
            OutputZombies.AddRange(ga.currentBestZombies);

            SortedZombies = OutputZombies.OrderByDescending(z => z.fitness).ToList();



            //Take the best of these generations and output with mutations (or repeat list without mutations)



            Debug.Log("Now at generation: " + ga.generation);
        }
    }
}
