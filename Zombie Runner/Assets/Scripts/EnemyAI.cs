using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    public LayerMask obstacleMask;

    NavMeshAgent navMeshAgent;

    new Renderer renderer;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();  
        renderer = GetComponent<Renderer>();      
    }

    // Update is called once per frame
    void Update()
    {
        bool isVisible = isTargetVisible(target);

        if (isVisible)
            navMeshAgent.SetDestination(transform.position); // Stay in place
        else
            navMeshAgent.SetDestination(target.position); // Move towards player
    }

    bool isTargetVisible(Transform target) 
    {
        if (!renderer.isVisible)
            return false;

        Vector3 normalVector = (target.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target.position);
        return !Physics.Raycast(transform.position, normalVector, distance, obstacleMask);
    }
}
