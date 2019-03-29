using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAController : MonoBehaviour {

    private int generationalGap = 10;
    private GeneticAlgorithm ga;

    // Use this for initialization
	void Start () {
        ga = new GeneticAlgorithm();
        ga.createStartPopulation();
    }

    public void multipleGenerationEpoch()
    {
        for (int i = 0; i < generationalGap; i++)
        {
            ga.epoch();
        }

        Debug.Log("Now at generation: " + ga.generation);
    }
}
