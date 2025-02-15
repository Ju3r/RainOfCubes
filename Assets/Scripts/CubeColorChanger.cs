using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public class CubeColorChanger : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeColor()
    {
        float minRandomColorNumber = 0f;
        float maxRandomColorNumber = 1f;

        Color randomColor = new Color(
                                Random.Range(minRandomColorNumber, maxRandomColorNumber),
                                Random.Range(minRandomColorNumber, maxRandomColorNumber),
                                Random.Range(minRandomColorNumber, maxRandomColorNumber)
                                );

        SetColor(randomColor);
    }

    public void SetColor(Color color)
    {
        if (_meshRenderer != null)
            _meshRenderer.material.color = color;
    }
}