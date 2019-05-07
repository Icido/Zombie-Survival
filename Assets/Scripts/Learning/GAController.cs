using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading;

public class GAController {

    private int generationalGap = 10;
    private GeneticAlgorithm ga;

    public List<SmartZombie> OutputZombies = new List<SmartZombie>();
    public List<SmartZombie> SortedZombies = new List<SmartZombie>();

    public bool isEpochPaused = false;

    private EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
    

    // Use this for initialization
    public void initialEpoch() {
        ga = new GeneticAlgorithm();
        ga.createStartPopulation();
    }

    public void epochLoop()
    {
        while(true)
        {
            if(isEpochPaused)
            {
                ewh.WaitOne();
            }
            else
            {
                if (ga.hasFoundPerfection)
                {
                    //Output "best zombie" + possible mutations
                    break;
                }
                else
                {
                    ga.currentBestZombies.Clear();

                    ga.epoch();

                    //Sort ga.currentBestZombies by fitness score
                    OutputZombies.AddRange(ga.currentBestZombies);

                    SortedZombies = OutputZombies.OrderByDescending(z => z.fitness).ToList();

                    //Take the best of these generations and output with mutations (or repeat list without mutations)

                    Debug.Log("Now at generation: " + ga.generation);
                }
            }
        }

        Debug.Log("Found perfection, now stopping...");
    }

    public void pauseEpoch(bool isPaused)
    {
        isEpochPaused = isPaused;

        if(!isEpochPaused)
        {
            ewh.Set();
        }
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
            OutputZombies.AddRange(ga.currentBestZombies);

            SortedZombies = OutputZombies.OrderByDescending(z => z.fitness).ToList();



            //Take the best of these generations and output with mutations (or repeat list without mutations)



            Debug.Log("Now at generation: " + ga.generation);
        }
    }
}
