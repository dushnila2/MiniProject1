using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Vector3 _spawnPoint;
    private ObjectPool<Zombie> _zombiepool;
    [SerializeField] private float _wanderRangeMin = 1.5f;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _spawnPoint = transform.position;
        _agent.SetDestination(GetRandomPoint(_spawnPoint, _wanderRangeMin));
    }



    void Update()
    {
        if (_agent.remainingDistance <= 0.02f)
        {
            _agent.SetDestination(GetRandomPoint(_spawnPoint, _wanderRangeMin));
        }

    }
    Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        Vector3 randomDirection;
        do
        {
            randomDirection = Random.insideUnitSphere * radius;
            randomDirection += center;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                if (Vector3.Distance(transform.position, hit.position) > _wanderRangeMin)
                {
                    return hit.position;
                }
            }
        } while (true);
    }
}
