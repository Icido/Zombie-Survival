using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm {

    public List<Genomes> genomes = new List<Genomes>();
    public List<Genomes> lastGenerationGenomes = new List<Genomes>();
    public List<int> bestZombie = new List<int>();
    public List<SmartZombie> currentBestZombies = new List<SmartZombie>();

    public int populationSize = 100; //100
    public double crossoverRate = 0.7f; //0.7f
    public double mutationRate = 0.001f; //0.001f
    public int chromosoneLength = 72;
    public int geneLength = 8;

    public int fittestGenome;
    public double bestFitnessScore;
    public double totalFitnessScore;

    public double globalBestFitnessScore = 0;
    public int generationOfGlobalBestFitnessScore;
    public int generationCountdown = -1;

    public int generation;

    public bool busy;
    public bool hasFoundPerfection;

    public void createStartPopulation()
    {
        genomes.Clear();
        
        for (int i = 0; i < populationSize; i++)
        {
            Genomes child = new Genomes(chromosoneLength);
            genomes.Add(child);
        }
    }

    public void updateFitnessScores()
    {
        fittestGenome = 0;
        bestFitnessScore = 0;
        totalFitnessScore = 0;

        for (int i = 0; i < populationSize; i++)
        {
            //Converts the genes of this individual into a list of attribute values
            List<int> attributeValues = decode(genomes[i].bits);

            SmartZombie zombie = new SmartZombie(attributeValues);
            
            genomes[i].fitness = zombie.zombieTest();

            totalFitnessScore += genomes[i].fitness;

            if(genomes[i].fitness > globalBestFitnessScore)
            {
                globalBestFitnessScore = genomes[i].fitness;
                bestZombie = attributeValues;
                generationOfGlobalBestFitnessScore = generation;

                //Debug.Log("New best Fitness! " + globalBestFitnessScore);

                if (globalBestFitnessScore < 1 && globalBestFitnessScore > 0.95)
                {
                    //Debug.Log("Countdown started...");
                    generationCountdown = generation + 100;
                }
            }

            if(generationCountdown == generation)
            {
                //Debug.Log("Near perfection!");
                //Debug.Log("Fitness value: " + globalBestFitnessScore);
                hasFoundPerfection = true;
                return;
            }

            if (genomes[i].fitness > bestFitnessScore)
            {
                bestFitnessScore = genomes[i].fitness;
                fittestGenome = i;

                //Check if (fitness = 1) || (fitness ~ 0.95 for 10 generations)
                if (genomes[i].fitness == 1)
                {
                    //Debug.Log("Perfection!");
                    hasFoundPerfection = true;
                    bestZombie = attributeValues;
                    return;
                }

            }
        }

        SmartZombie newZombie = new SmartZombie(decode(genomes[fittestGenome].bits));

        currentBestZombies.Add(newZombie);
    }

    public List<int> decode(List<int> bits)
    {
        List<int> attributeValues = new List<int>();

        for(int geneIndex = 0; geneIndex < bits.Count; geneIndex += geneLength)
        {
            List<int> gene = new List<int>();

            for(int bitIndex = 0; bitIndex < geneLength; bitIndex++)
            {
                gene.Add(bits[geneIndex + bitIndex]);
            }

            attributeValues.Add(geneToInt(gene));

        }
        return attributeValues;
    }

    public int geneToInt(List<int> gene)
    {
        int value = 0;
        int multiplier = 1;

        for (int i = gene.Count; i > 0; i--)
        {
            value += gene[i - 1] * multiplier;
            multiplier *= 2;
        }

        return value;
    }

    public Genomes rouletteWheelSelection()
    {
        double slice = Random.value * totalFitnessScore;
        double total = 0;
        int selectedGenome = 0;

        for (int i = 0; i < populationSize; i++)
        {
            total += genomes[i].fitness;

            if (total > slice)
            {
                selectedGenome = i;
                break;
            }
        }

        return genomes[selectedGenome];

    }

    public void mutate(List<int> bits)
    {
        for (int i = 0; i < bits.Count; i++)
        {
            if(Random.value < mutationRate)
            {
                bits[i] = bits[i] == 0 ? 1 : 0;
            }
        }
    }

    public void crossover(List<int> mother, List<int> father, List<int> child1, List<int> child2)
    {

        //If it's above crossover rate or parents are the same, copy through
        if(Random.value > crossoverRate || mother == father)
        {
            child1.AddRange(mother);
            child2.AddRange(father);
        }

        System.Random random = new System.Random();

        int crossoverPoint = random.Next(0, chromosoneLength - 1);

        for (int i = 0; i < crossoverPoint; i++)
        {
            child1.Add(father[i]);
            child2.Add(mother[i]);
        }

        for (int i = crossoverPoint; i < father.Count && i < mother.Count; i++)
        {
            child1.Add(mother[i]);
            child2.Add(father[i]);
        }
    }

    public void epoch()
    {
        busy = true;

        //Debug.Log("New Generation: " + generation + ", Pop size: " + populationSize);

        updateFitnessScores();

        if (!hasFoundPerfection)
        {
            int numberOfNewChildren = 0;

            List<Genomes> children = new List<Genomes>();

            while (numberOfNewChildren < populationSize)
            {
                Genomes mother = rouletteWheelSelection();
                Genomes father = rouletteWheelSelection();
                Genomes child1 = new Genomes();
                Genomes child2 = new Genomes();
                crossover(mother.bits, father.bits, child1.bits, child2.bits);
                mutate(child1.bits);
                mutate(child2.bits);
                children.Add(child1);
                children.Add(child2);

                numberOfNewChildren += 2;
            }


            lastGenerationGenomes.Clear();
            lastGenerationGenomes.AddRange(genomes);

            genomes = children;

            generation++;
        }

        busy = false;
    }

}
