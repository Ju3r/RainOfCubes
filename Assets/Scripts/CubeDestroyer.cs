using System;
using System.Collections;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    private float _lifeTime;

    private float _minLifeTime = 2;
    private float _maxLifeTime = 7;

    private float _endTime = 0.1f;

    public event Action<Cube> Destroyed;
    
    public void StartDestroying(Cube cube)
    {
        SetRandomLifeTime();
        StartCoroutine(Destroying(cube));
    }

    private void SetRandomLifeTime()
    {
        _lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
    }

    private IEnumerator Destroying(Cube cube)
    {
        while (_lifeTime > _endTime)
        {
            _lifeTime -= Time.deltaTime;
            yield return null;
        }

        Destroyed?.Invoke(cube);
    }
}