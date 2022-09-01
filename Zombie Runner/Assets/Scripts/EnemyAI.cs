using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [System.Serializable]
    public class TrackingSettings {
        [Min(0)]
        public float trackingRange;
        public Transform target;
        public Transform targetViewpoint;
        public LayerMask obstacleMask;
    }
    [SerializeField] public TrackingSettings trackingSettings;


    private BoxCollider viewCollider;
    private NavMeshAgent navMeshAgent;
    private new Renderer renderer;


    void Start()
    {
        viewCollider = GetComponent<BoxCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();  
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        
        bool isVisible = IsTargetVisible(trackingSettings.targetViewpoint);

        
        if (!isVisible && ShouldChaseTarget())
        {
            navMeshAgent.SetDestination(trackingSettings.target.position); // Move towards player
        }
        else
        {
            navMeshAgent.SetDestination(transform.position); // Stay in place
        }            
    }

    private bool IsTargetVisible(Transform _target) 
    {
        if (!renderer.isVisible)
            return false;


        Vector3[] colliderCorners = new Vector3[8];
        for (int i = 0; i < colliderCorners.Length ; ++i)
        {
            Vector3 size = viewCollider.size;
            size.Scale (new Vector3((i & 1) == 0 ? 1: -1, (i & 2) == 0 ? 1 : -1, (i & 4) == 0 ? 1 : -1));

            Vector3 corner = viewCollider.transform.TransformPoint(viewCollider.center + size * .5f);
            colliderCorners[i] = corner;
            Debug.DrawLine(transform.position, corner);
        }  
        
        foreach (Vector3 corner in colliderCorners)
        {
            Vector3 normalVector = (_target.transform.position - corner).normalized;
            float distance = Vector3.Distance(corner, _target.position);
            
            if (!Physics.Raycast(corner, normalVector, distance, trackingSettings.obstacleMask))
                return true;
        }
        return false;
    }

    private bool ShouldChaseTarget() {
        float distanceToTarget = Vector3.Distance(trackingSettings.target.position, transform.position);
        return distanceToTarget <= trackingSettings.trackingRange;
    }
}
