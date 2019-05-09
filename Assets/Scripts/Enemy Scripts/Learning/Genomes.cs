using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genomes {
    public List<int> bits;
    public double fitness;

    public Genomes()
    {
        Initialize();
    }

    //Called upon first generation being populated, entered with random values
    public Genomes(int numBits)
    {
        Initialize();

        for (int i = 0; i < numBits; i++)
        {
            System.Random randomNumberGen = new System.Random(DateTime.Now.GetHashCode() * SystemInfo.processorFrequency.GetHashCode());

            bits.Add(randomNumberGen.Next(0, 2));
        }
    }

    private void Initialize()
    {
        fitness = 0;
        bits = new List<int>();
    }


}
