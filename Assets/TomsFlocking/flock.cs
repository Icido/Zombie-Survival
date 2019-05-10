using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject leader;
    public int neighbourRadius;

    private GameObject[] agents;
    private float playerSpeed = 3f;
    private levelController lvlScript;

    public float alignmentWeight = 1;
    public float cohesionWeight = 1;
    public float separationWeight = 1;

    Vector3 pos = Vector3.zero;
    //List<GameObject> currentNeighbours;


    Vector3 randomVec;

    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("aiAgent");
        lvlScript = gameManager.GetComponent<levelController>();

        randomVec = new Vector3(Random.Range(-lvlScript.spawnRadius, lvlScript.spawnRadius),
                                0.5f,
                                Random.Range(-lvlScript.spawnRadius, lvlScript.spawnRadius)) * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {

        Flock();

    }

    void Flock()
    {
        pos = (alignment() * alignmentWeight) + (cohesion() * cohesionWeight) + (separation() * separationWeight);
        pos.Normalize();
        pos *= playerSpeed;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, pos, playerSpeed * Time.deltaTime * 5, 1));
        transform.position += pos * playerSpeed * Time.deltaTime;

        Debug.Log(separation());
    }

    Vector3 alignment()
    {
        Vector3 temp = Vector3.zero;
        float neighbourCount = 0;

        foreach (GameObject agent in agents)
        {
            //DONT CHECK AGAINST SELF
            if (agent != this.gameObject)
            {
                if (Vector3.Distance(transform.position, agent.transform.position) < neighbourRadius)
                {
                    temp += agent.transform.forward;
                    neighbourCount++;
                }
            }
        }
        if (neighbourCount == 0)
        {
            return temp;
        }

        temp /= neighbourCount;
        temp.Normalize();
        temp.y = 0;
        return temp;

    }

    Vector3 cohesion()
    {
        Vector3 temp = Vector3.zero;
        float neighbourCount = 0;

        foreach (GameObject agent in agents)
        {
            //DONT CHECK AGAINST SELF
            if (agent != this.gameObject)
            {
                if (Vector3.Distance(transform.position, agent.transform.position) < neighbourRadius)
                {
                    temp += agent.transform.position;
                    neighbourCount++;
                }
            }
        }
        if (neighbourCount == 0)
        {
            return temp;
        }
        else
        {
            temp /= neighbourCount;
            temp = new Vector3(temp.x - transform.position.x, 0, temp.z - transform.position.z);
            temp.Normalize();
            temp.y = 0;
            return temp;
        }
    }

    Vector3 separation()
    {
        Vector3 temp = Vector3.zero;
        float neighbourCount = 0;

        foreach (GameObject agent in agents)
        {
            //DONT CHECK AGAINST SELF
            if (agent != this.gameObject)
            {
                if (Vector3.Distance(transform.position, agent.transform.position) < 2f)
                {
                    temp.x += agent.transform.position.x - transform.position.x;
                    temp.z += agent.transform.position.z - transform.position.z;

                    //temp.x += transform.position.x - agent.transform.position.x;
                    //temp.z += transform.position.z - agent.transform.position.z;
                    neighbourCount++;
                }
            }
        }
        if (neighbourCount == 0)
        {
            return temp;
        }
        else
        {
            temp /= neighbourCount;
            temp *= -1;
            temp.Normalize();
            temp.y = 0;
            return temp;
        }
    }

    List<GameObject> getNeighbours()
    {
        List <GameObject> temp = new List<GameObject>();

        foreach (GameObject agent in agents)
        {
            if (agent != this.gameObject)
            {
                if (Vector3.Distance(transform.position, agent.transform.position) < neighbourRadius)
                {
                    temp.Add(agent);
                }
            }
        }

        return temp;
    }
}
