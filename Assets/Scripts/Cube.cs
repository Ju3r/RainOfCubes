using System;
using UnityEngine;

[RequireComponent(typeof(CubeCollisionHandler), typeof(CubeColorChanger), typeof(CubeDestroyer))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    private CubeCollisionHandler _collisionHandler;
    private CubeColorChanger _colorChanger;
    private CubeDestroyer _destroyer;

    private bool _isPlatformCollided = false;

    private void Awake()
    {
        _destroyer = FindFirstObjectByType<CubeDestroyer>();

        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();

        _collisionHandler = GetComponent<CubeCollisionHandler>();
        _colorChanger = GetComponent<CubeColorChanger>();
        _destroyer = GetComponent<CubeDestroyer>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollidedPlatform += CollidedPlatform;
    }

    private void OnDisable()
    {
        _collisionHandler.CollidedPlatform -= CollidedPlatform;
    }

    public void SetColor(Color color)
    {
        if (_meshRenderer != null)
            _meshRenderer.material.color = color;
    }

    public void SetDefaultColor(Color color)
    {
        if (_meshRenderer != null)
            _meshRenderer.material.color = color;
    }

    public void ResetPlatformCollided()
    {
        _isPlatformCollided = false;
    }

    private void CollidedPlatform()
    {
        if (_isPlatformCollided == false)
        {
            _colorChanger.ChangeColor(this);
            _destroyer.StartDestroying(this);
        }

        _isPlatformCollided = true;
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }
    
    public CubeDestroyer GetDestroyer()
    {
        return _destroyer;
    }
}