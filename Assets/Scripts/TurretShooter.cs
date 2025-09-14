using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPoint;  

    [SerializeField] private float _spawnRate = 1.5f;
    private float _timer;                               

    void Start()
    {
        _timer = _spawnRate; 
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
