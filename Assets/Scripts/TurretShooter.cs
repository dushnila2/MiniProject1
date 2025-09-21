using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _spawnPoint;  

    [SerializeField] private float _spawnRate = 1.5f;
    private float _timer;
    private ObjectPool<Bullet> _pool;

    void Start()
    {
        _timer = _spawnRate; 
        _pool = new ObjectPool<Bullet>(_bulletPrefab);
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            SpawnBullet();
            _timer = _spawnRate;
        }
    }

    private void SpawnBullet()
    {
        Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
    }
}
