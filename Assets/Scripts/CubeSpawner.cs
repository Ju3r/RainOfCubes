using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private Vector3 _startAreaPosition;
    [SerializeField] private Vector3 _endAreaPosition;

    [SerializeField] private List<Cube> _cubes = new List<Cube>();

    private float _delay = 1f;
    private bool _isSpawning = true;

    private ObjectPool<GameObject> _pool;

    private int _poolCapacity = 5;
    private int _poolMaxSize = 5;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Spawn(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => ActionOnDestroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void ActionOnDestroy(GameObject obj)
    {
        if (obj.TryGetComponent(out Cube cube))
            _cubes.Remove(cube);

        Destroy(obj.gameObject);
    }

    private GameObject Spawn()
    {
        GameObject gameObject = Instantiate(_prefab, GetRandomPosition(), Quaternion.identity);

        if (gameObject.TryGetComponent(out Cube cube))
        {
            _cubes.Add(cube);
            Subscription(cube);
        }

        return gameObject;
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private void OnEnable()
    {
        foreach (Cube cube in _cubes)
        {
            Subscription(cube);
        }
    }

    private void OnDisable()
    {
        foreach (Cube cube in _cubes)
        {
            Unsubscription(cube);
        }
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
        _pool.Release(cube.gameObject);
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.transform.position = GetRandomPosition();
        obj.transform.rotation = Quaternion.identity;

        if (obj.TryGetComponent(out Cube cube))
        {
            cube.ResetPlatformCollided();

            cube.GetRigidbody().velocity = Vector3.zero;
            cube.GetRigidbody().angularVelocity = Vector3.zero;

            cube.SetColor(Color.white);
        }

        obj.gameObject.SetActive(true);
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