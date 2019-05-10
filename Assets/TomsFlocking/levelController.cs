using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour {

    public GameObject agentPrefab;
    public int agentCount;
    public List<GameObject> agents;

    public float spawnRadius;

	void Start () {

        spawnAgent(agentPrefab, agentCount);
	}

    void spawnAgent(GameObject prefab, int agentsToSpawn)
    {
        for (int i = 0; i < agentsToSpawn; i++)
        {
            GameObject tempAgent = Instantiate(prefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), //limits the range at which the agents may spawn
                                            0, Random.Range(-spawnRadius, spawnRadius)), Quaternion.identity);
            tempAgent.tag = "aiAgent";

            agents.Add(tempAgent);
        }
    }

    public List<GameObject> getNeighbours(GameObject agent, float neighbourRadius)
    {
        List<GameObject> localNeighbours = new List<GameObject>();

        foreach (GameObject m_agent in agents)
        {
            if (m_agent != agent)
            {
                if (Vector3.Distance(agent.transform.position, m_agent.transform.position) < neighbourRadius)
                {
                    localNeighbours.Add(m_agent);
                }
            }
        }

        return localNeighbours;
    }
}
