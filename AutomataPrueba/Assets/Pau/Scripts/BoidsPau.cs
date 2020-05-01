using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsPau : MonoBehaviour
{
    public GameObject agentPrefab;

    [SerializeField]
    int numBoids = 10;

    AgentPau[] agents;

    public float agentRadius = 4.0f;
    float separationWeight = 1.0f, cohesionWeight = 1.0f, alignmentWeight = 1.0f;

    private void Awake()
    {
        List<AgentPau> agentlist = new List<AgentPau>();

        for (int i = 0; i < numBoids; i++)
        {
            Vector3 position = Vector3.up * Random.Range(0, 10)
                + Vector3.right * Random.Range(0, 10) + Vector3.forward * Random.Range(0, 10);

            AgentPau newAgent = Instantiate(agentPrefab, transform.position + position, Quaternion.identity).GetComponent<AgentPau>();
            newAgent.radius = agentRadius;
            agentlist.Add(newAgent);
        }
        agents = agentlist.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (AgentPau a in agents)
        {
            a.velocity = a.vel;
            a.neightbours.Clear();
            a.checkNeightbours();
            calculateCohesion(a);
            calculateSeparation(a);
            calculateAlignment(a);
            a.updateAgent();
        }
    }

    void calculateSeparation(AgentPau a)
    {
        Vector3 separationVector = Vector3.zero;
        foreach (AgentPau neightbour in a.neightbours)
        {
            float distance = Vector3.Distance(a.transform.position, neightbour.transform.position);
            distance /= a.radius;
            distance = 1 - distance;
            separationVector += distance * (a.transform.position - neightbour.transform.position).normalized * separationWeight;
        }
        a.addForce(separationVector, AgentPau.DEBUGforceType.SEPARATION);
    }

    void calculateCohesion(AgentPau a)
    {
        Vector3 centralPosition = new Vector3();

        foreach (AgentPau neightbour in a.neightbours)
        {
            centralPosition += neightbour.transform.position;
        }
        centralPosition += a.transform.position;
        centralPosition /= a.neightbours.Count + 1;
        a.addForce((centralPosition - a.transform.position) * cohesionWeight, AgentPau.DEBUGforceType.COHESION);
    }

    void calculateAlignment(AgentPau a)
    {
        Vector3 dirVec = new Vector3();

        foreach (AgentPau neightbour in a.neightbours)
        {
            dirVec += neightbour.velocity;
        }

        dirVec += a.velocity;
        dirVec /= a.neightbours.Count + 1;
        a.addForce(dirVec, AgentPau.DEBUGforceType.ALIGNMENT);
    }
}
