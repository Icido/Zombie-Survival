using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;
    public GameObject currentObj;
    public GameObject Player;
    public NavMeshAgent navMeshAgent;
    public State stayState;
    public bool canAttack = true;
    public float attackTimer = 5;
    public GameObject waveManager;

    public int health = 50;
	// Use this for initialization
	void Start ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentObj = this.gameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        waveManager = GameObject.Find("WaveManager");
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentState.updateState(this);
        if (canAttack == false)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                canAttack = true;
                attackTimer = 5;
            }
        }
	}

    public void stateTransition(State nextState)
    {
        if (nextState != stayState)
        {
            currentState = nextState;
            stayState = nextState;
        }
    }
}
