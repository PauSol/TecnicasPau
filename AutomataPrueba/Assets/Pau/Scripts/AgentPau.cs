﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPau : MonoBehaviour
{
    public Vector3 velocity = Vector3.right;
    public Vector3 separationForce, cohesionForce, alignmentForce;
    [SerializeField]
    public Vector3 vel = Vector3.right;

    public List<AgentPau> neightbours;

    public float radius = 2.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + separationForce);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + cohesionForce);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + alignmentForce);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + velocity);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Awake()
    {
        neightbours = new List<AgentPau>();
    }

    public enum DEBUGforceType
    {
        SEPARATION,
        COHESION,
        ALIGNMENT
    }

    public void addForce(Vector3 f, DEBUGforceType type)
    {
        if (type == DEBUGforceType.SEPARATION)
            separationForce = f;
        else if (type == DEBUGforceType.COHESION)
            cohesionForce = f;
        else if (type == DEBUGforceType.ALIGNMENT)
            alignmentForce = f;

        velocity += f;
    }

    public void addForce(Vector3 f)
    {
        velocity += f;
    }

    public void updateAgent()
    {
        transform.position += velocity * Time.deltaTime;
        Debug.Log(vel);
    }

    public void checkNeightbours()
    {
        Collider[] checks = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in checks)
        {
            neightbours.Add(c.GetComponent<AgentPau>());
        }

    }
}
