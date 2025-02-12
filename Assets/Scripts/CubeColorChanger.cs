using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    public Cube ChangeColor(Cube cube)
    {
        Cube modifiedCube = cube;

        float minRandomColorNumber = 0f;
        float maxRandomColorNumber = 1f;

        Color randomColor = new Color(
                                Random.Range(minRandomColorNumber, maxRandomColorNumber),
                                Random.Range(minRandomColorNumber, maxRandomColorNumber),
                                Random.Range(minRandomColorNumber, maxRandomColorNumber)
                                );

        cube.SetColor(randomColor);

        return cube;
    }
}