using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefab & spawn settings")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int initialCount = 5; 
    [SerializeField] private Vector3 spawnArea = new(5, 1, 5); 

    private List<CubeCustomizer> _trackedCubes = new();

    private void Start()
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Spawner: cubePrefab не назначен!");
            return;
        }

        for (int i = 0; i < initialCount; i++)
        {
            float minValueY = 0.5f;

            Vector3 positon = transform.position + new Vector3(
                UnityEngine.Random.Range(-spawnArea.x, spawnArea.x),
                UnityEngine.Random.Range(minValueY, spawnArea.y + minValueY),
                UnityEngine.Random.Range(-spawnArea.z, spawnArea.z)
            );

            SpawnInitial(positon);
        }
    }

    public CubeCustomizer SpawnInitial(Vector3 worldPosition)
    {
        var cubeReferens = Instantiate(cubePrefab, worldPosition, Quaternion.identity, null);

        if (!cubeReferens.TryGetComponent<CubeCustomizer>(out var cube))
        {
            Debug.LogError("cubePrefab не содержит компонента Cube!");
            Destroy(cubeReferens);
            return null;
        }

        cube.Initialize(this, Vector3.one, cube.SplitChance);
        cube.ApplyRandomColor();

        cube.Clicked += RemoveCube;
        _trackedCubes.Add(cube);
        return cube;
    }

    public List<CubeCustomizer> SpawnChildren(CubeCustomizer originalCube)
    {
        int miValue = 2;
        int maxVavue = 7;
        float valueDivider = 0.5f;

        if (originalCube == null) return new List<CubeCustomizer>();

        int countSpawnCube = UnityEngine.Random.Range(miValue, maxVavue);

        List<CubeCustomizer> createdCubes = new();

        Vector3 baseScale = originalCube.transform.localScale;
        Vector3 childScale = baseScale * valueDivider;
        float childSplitChance = originalCube.SplitChance * valueDivider;

        for (int i = 0; i < countSpawnCube; i++)
        {
            float spawnRadius = 1.0f;

            Vector3 offset = UnityEngine.Random.insideUnitSphere * spawnRadius;
            Vector3 spawnPos = originalCube.transform.position + offset;

            var cubeReferens = Instantiate(cubePrefab, spawnPos, UnityEngine.Random.rotation, null);

            if (!cubeReferens.TryGetComponent<CubeCustomizer>(out var cube))
            {
                Destroy(cubeReferens);
                continue;
            }

            cube.Initialize(this, childScale, childSplitChance);
            cube.ApplyRandomColor();

            cube.Clicked += RemoveCube;
            _trackedCubes.Add(cube);

            createdCubes.Add(cube);
        }

        return createdCubes;
    }

    private void RemoveCube(CubeCustomizer cube)
    {
        if (cube == null) return;

        cube.Clicked -= RemoveCube;
        _trackedCubes.Remove(cube);

        Destroy(cube.gameObject);
    }
}
