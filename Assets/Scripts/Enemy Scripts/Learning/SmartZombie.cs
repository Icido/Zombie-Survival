using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartZombie {

    public SmartZombie(List<int> attributeValues)
    {
        //Convert attributeValues into workable "SmartZombie" values
        inputGenesToZGenes(attributeValues);
        ZGenesToAttributes(zGenes);

        if((attributes.meleeStrength * attributes.meleeAttackRate) < (attributes.rangeStrength * attributes.rangeAttackRate))
        {
            useMelee = false;
        }
        else
        {
            useMelee = true;
        }
    }

    public bool useMelee = true;

    public double fitness = 0;

    private const float maxGeneValue = 255f;

    public struct ZombieAttributes
    {
        public float meleeRange; //How close the zombie must be from the player before it can attack with melee
        public float meleeStrength; //How hard the zombie hits with melee
        public float meleeAttackRate; //How fast the zombie hits with melee (times per second)

        public float rangeRange; //How close the zombie can be from the player before it attack with range
        public float rangeStrength; //How hard the zombie hits with range
        public float rangeAttackRate; //How fast the zombie hits with range (times per second)

        public float speed; //How fast the zombie can run
        public float health; //How much damage the zombie can take

        public float leadershipStrength; //How many zombies this zombie can lead
        public float leadershipRange; //How close the zombies can be before they're influenced by this zombie
    }

    public ZombieAttributes attributes;

    public struct ZombieGenes
    {
        //HIGH +meleeRange -meleeSpeed | LOW -meleeRange +meleeSpeed
        public int armLength;

        //HIGH +meleeStrength -meleeSpeed | LOW -meleeStrength +meleeSpeed
        public int clawLength;

        //HIGH +rangeRange -rangeSpeed | LOW -rangeRange +rangeSpeed
        public int throatLength;

        //HIGH +rangeStrength -rangeSpeed | LOW -rangeStrength +rangeSpeed
        public int acidStrength;

        //HIGH +meleeRange -speed | LOW -meleeRange +speed
        public int legLength;

        //HIGH +rangeRange -health | LOW -rangeRange +health
        public int acidSize;

        //HIGH +health -speed | LOW -health +speed
        public int tankiness;

        //HIGH +leadershipStrength -health | LOW -leadershipStrength +health
        public int pheremoneStrength;

        //HIGH -leadershipStrength +leadershipRange | LOW +leadershipStrength -leadershipRange
        public int pheremoneRange;
    }

    private ZombieGenes zGenes;

    void inputGenesToZGenes(List<int> genes)
    {
        zGenes.armLength = genes[0];
        zGenes.clawLength = genes[1];
        zGenes.throatLength = genes[2];
        zGenes.acidStrength = genes[3];
        zGenes.legLength = genes[4];
        zGenes.acidSize = genes[5];
        zGenes.tankiness = genes[6];
        zGenes.pheremoneStrength = genes[7];
        zGenes.pheremoneRange = genes[8];

        return;
    }

    private void ZGenesToAttributes(ZombieGenes genes)
    {
        attributes.meleeRange = 0.5f + ((1f * genes.armLength) / maxGeneValue) + ((0.5f * genes.legLength) / maxGeneValue); //0.5-3.5
        attributes.meleeStrength = 2.0f + ((2f * genes.clawLength) / maxGeneValue); //2-6
        attributes.meleeAttackRate = 0.5f + ((1.25f * (maxGeneValue - genes.armLength)) / maxGeneValue) + ((1f * (maxGeneValue - genes.clawLength)) / maxGeneValue); //0.5-5

        attributes.rangeRange = 10f  + ((2.5f * genes.throatLength) / maxGeneValue) + ((1f * genes.acidSize) / maxGeneValue); //10-17
        attributes.rangeStrength = 1.5f + ((2f * genes.acidStrength) / maxGeneValue); //1.5-5.5
        attributes.rangeAttackRate = 0.5f + ((0.75f * (maxGeneValue - genes.throatLength)) / maxGeneValue) + ((0.5f * (maxGeneValue - genes.acidStrength)) / maxGeneValue); //0.5-3

        attributes.speed = 0.5f + ((1.5f * (maxGeneValue - genes.legLength)) / maxGeneValue) + ((1f * (maxGeneValue - genes.tankiness)) / maxGeneValue); //0.5-5.5
        attributes.health = 50f  + ((5f * (maxGeneValue - genes.acidSize)) / maxGeneValue) + ((12.5f * genes.tankiness) / maxGeneValue) + ((7.5f * (maxGeneValue - genes.pheremoneStrength)) / maxGeneValue); //50-100

        attributes.leadershipStrength = 0f + ((5f * genes.pheremoneStrength) / maxGeneValue) + ((2.5f * (maxGeneValue - genes.pheremoneRange)) / maxGeneValue); //0-15
        attributes.leadershipRange = 0.5f + ((12.25f * (genes.pheremoneRange)) / maxGeneValue); //0.5-25

        return;
    }









    private float playerAccuracy = 80f;
    private float playerDamage = 10f;
    private float playerRange = 25f;
    private float playerAttackRate = 2f;
    private float playerCurrentHealth = 100f;
    private float playerMaxHealth;

    private float zombieMeleeDamagePerSecond;
    private float zombieRangeDamagePerSecond;

    public double zombieTest()
    {
        double newFitness = 0;

        //Calculate how much damage per second from the converted values
        zombieMeleeDamagePerSecond = attributes.meleeStrength * attributes.meleeAttackRate; //Highest current possible mDPS: 30
        zombieRangeDamagePerSecond = attributes.rangeStrength * attributes.rangeAttackRate; //Highest current possible rDPS: 16.5

        float playerDamagePerSecond = playerDamage * playerAttackRate;

        float currentZombieHealth = attributes.health;
        playerMaxHealth = playerCurrentHealth;

        float startDistance = Random.Range(10, 30);

        bool isPlayerDead = false;
        bool prefersMelee = (zombieMeleeDamagePerSecond > zombieRangeDamagePerSecond) ? true : false;

        while (currentZombieHealth > 0)
        {

            //Checks if the player is in range of shooting the zombie
            if (startDistance > playerRange)
            {
                startDistance -= attributes.speed;
                continue;
            }

            //Takes damage from player (if the player hits)
            if (Random.Range(0, 100) <= (playerAccuracy - ((attributes.leadershipStrength * attributes.leadershipRange) / 10f)))
            {
                currentZombieHealth -= playerDamagePerSecond;
            }

            //If the zombie is not dead, deal damage to player (update player damage taken), else BREAK
            if(currentZombieHealth <= 0)
            {
                break;
            }
            else
            {
                if(prefersMelee)
                {
                    if(startDistance <= attributes.meleeRange)
                    {
                        playerCurrentHealth -= zombieMeleeDamagePerSecond;
                    }
                    else
                    {
                        //Move forward
                        startDistance -= attributes.speed;
                    }
                }
                else
                {
                    if (startDistance <= attributes.rangeRange)
                    {
                        playerCurrentHealth -= zombieRangeDamagePerSecond;
                    }
                    else
                    {
                        //Move forward
                        startDistance -= attributes.speed;
                    }
                }
            }
            
            //Check if player is dead, BREAK
            if (playerCurrentHealth <= 0)
            {
                isPlayerDead = true;
                break;
            }
        }


        //Check either player is dead:
        if (isPlayerDead)
        {
            newFitness = 1;
        }
        else
        {
            //REDO FITNESS TO TAKE INTO ACCOUNT DAMAGE DONE, NOT PERCENT OF CURRENT HEALTH DONE
            float percentageDamageToPlayer = (playerMaxHealth - playerCurrentHealth) / playerMaxHealth;
            newFitness = percentageDamageToPlayer;
        }

        if (fitness <= 0)
            fitness = 0.001f;

        fitness = newFitness;

        return fitness;

    }





}
