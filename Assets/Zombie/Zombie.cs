using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    private enum ZombieState
    {
        Idle,
        Agro
    }

    [SerializeField] private float _wanderRangeMax = 6f;
    [SerializeField] private float _wanderRangeMin = 1.5f;
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _agrodistance = 20f;

    private float maxHealth = 1f;
    private float currentHealth;

    [SerializeField] private NavMeshAgent _agent;

    private Vector3 _spawnPoint;
    private ObjectPool<Zombie> _zombiePool;
    private ZombieSpawner _spawner;

    private ZombieState state;   // ← поле состояния
    private bool _isAlive = true; // ← нужно, иначе не компилится

    void Update()
    {
        if (!_isAlive)
            return;

        if (Vector3.Distance(_spawner.CarTransform.position, transform.position) >= 35)
        {
            Death();
            return;
        }

        if (state == ZombieState.Idle)
        {
            if (!_agent.pathPending && _agent.remainingDistance <= 0.02f)
            {
                Wander();
            }
            if (Vector3.Distance(_spawner.CarTransform.position, transform.position) <= _agrodistance)
            {
                state = ZombieState.Agro;
            }

        }
        else if (state == ZombieState.Agro) // ← было "StateMachineBehaviour"
        {
            _agent.speed = 3.5f;
            _agent.SetDestination(_spawner.CarTransform.position);
        }
    }

    public void Init(ObjectPool<Zombie> zombiePool, ZombieSpawner spawner, Vector3 spawnPoint)
    {
        _zombiePool = zombiePool;
        _spawnPoint = spawnPoint;
        _spawner = spawner;
        state = ZombieState.Idle;

        currentHealth = maxHealth;
        _healthBar.fillAmount = 1f;

        transform.position = _spawnPoint;

        gameObject.SetActive(true);

        _agent.enabled = true;
        _agent.Warp(_spawnPoint);

        Wander();
    }

    private void Wander()
    {
        Vector3 randomPoint;
        if (GetRandomPoint(_spawnPoint, _wanderRangeMax, out randomPoint))
        {
            _agent.SetDestination(randomPoint);
        }
        else
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Death");
        _isAlive = false; // ← добавлено, иначе Update продолжал бы работать
        _agent.enabled = false;
        _zombiePool.ReturnObject(this);
        gameObject.SetActive(false);
    }

    private bool GetRandomPoint(Vector3 center, float radius, out Vector3 randomPoint)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += center;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
            {
                if (Vector3.Distance(transform.position, hit.position) > _wanderRangeMin)
                {
                    randomPoint = hit.position;
                    return true;
                }
            }
        }

        randomPoint = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Death();
        }
        else if (other.CompareTag("Bullet")) // ← убрал лишнюю точку с запятой
        {
            currentHealth -= 0.5f;
            _healthBar.fillAmount = currentHealth / maxHealth; // ← нормализация
            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }
}
