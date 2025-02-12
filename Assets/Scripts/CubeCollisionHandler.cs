using System;
using UnityEngine;

public class CubeCollisionHandler : MonoBehaviour
{
    public event Action CollidedPlatform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            CollidedPlatform?.Invoke();
        }
    }
}
