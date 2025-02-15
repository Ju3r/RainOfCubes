using UnityEngine;

[RequireComponent(typeof(CubeCollisionHandler), typeof(CubeColorChanger), typeof(CubeDestroyer))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private CubeCollisionHandler _collisionHandler;
    private CubeColorChanger _colorChanger;
    private CubeDestroyer _destroyer;

    private bool _isPlatformCollided = false;

    private void Awake()
    {
        _destroyer = GetComponent<CubeDestroyer>();

        _rigidbody = GetComponent<Rigidbody>();

        _collisionHandler = GetComponent<CubeCollisionHandler>();
        _colorChanger = GetComponent<CubeColorChanger>();
        _destroyer = GetComponent<CubeDestroyer>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollidedPlatform += OnCollidedPlatform;
    }

    private void OnDisable()
    {
        _collisionHandler.CollidedPlatform -= OnCollidedPlatform;
    }

    public void Init()
    {
        ResetPlatformCollided();

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        SetColor(Color.white);
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }
    
    public CubeDestroyer GetDestroyer()
    {
        return _destroyer;
    }

    public void SetColor(Color color)
    {
        _colorChanger.SetColor(color);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotationToZero()
    {
        transform.rotation = Quaternion.identity;
    }

    public void ResetPlatformCollided()
    {
        _isPlatformCollided = false;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnCollidedPlatform()
    {
        if (_isPlatformCollided == false)
        {
            _colorChanger.ChangeColor();
            _destroyer.StartDestroying(this);
        }

        _isPlatformCollided = true;
    }
}