using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    [SerializeField] private Vector3 _startAreaPosition;
    [SerializeField] private Vector3 _endAreaPosition;

    private float _delay = 1f;
    private bool _isSpawning = true;

    private ObjectPool<Cube> _pool;

    private int _poolCapacity = 5;
    private int _poolMaxSize = 5;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Spawn(),
            actionOnGet: (cube) => ModifyOnGet(cube),
            actionOnRelease: (cube) => cube.Deactivate(),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private void Destroy(Cube cube)
    {
        Unsubscription(cube);

        Destroy((Object)cube);
    }

    private Cube Spawn()
    {
        Cube cube = Instantiate(_prefab, GetRandomPosition(), Quaternion.identity);

        Subscription(cube);

        return cube;
    }

    private void Subscription(Cube cube)
    {
        cube.GetDestroyer().Destroyed += ReleaseInPool;
    }

    private void Unsubscription(Cube cube)
    {
        cube.GetDestroyer().Destroyed -= ReleaseInPool;
    }

    private IEnumerator Spawning() {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_isSpawning)
        {
            _pool.Get();
            yield return wait;
        }
    }

    private void ReleaseInPool(Cube cube)
    {
        _pool.Release(cube);
    }

    private void ModifyOnGet(Cube cube)
    {
        cube.Init();
        cube.SetPosition(GetRandomPosition());
        cube.SetRotationToZero();
        cube.Activate();
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3();

        randomPosition.x = Random.Range(_startAreaPosition.x, _endAreaPosition.x);
        randomPosition.y = Random.Range(_startAreaPosition.y, _endAreaPosition.y);
        randomPosition.z = Random.Range(_startAreaPosition.z, _endAreaPosition.z);

        return randomPosition;
    }
}