using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GAController : MonoBehaviour {

    private int generationalGap = 10;
    private GeneticAlgorithm ga;

    public List<Genomes> SortedGenomes = new List<Genomes>();

    // Use this for initialization
	void Start () {
        ga = new GeneticAlgorithm();
        ga.createStartPopulation();
    }

    public void multipleGenerationEpoch()
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
                ga.epoch();
            }

            //Sort ga.currentBestZombies by fitness score

            SortedGenomes = ga.currentBestZombies.OrderByDescending(z => z.fitness).ToList();

            //Take the best of these generations and output with mutations (or repeat list without mutations)

            

            Debug.Log("Now at generation: " + ga.generation);
        }
    }
}
