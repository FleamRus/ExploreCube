using System.Collections.Generic;
using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    [Header("Creation Settings")]
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _spawnOffset = 0.5f;

    public static CubeCreator InstanceCreator { get; private set; }

    private void Awake()
    {
        if (InstanceCreator == null)
        {
            InstanceCreator = this;
        }
        else
        {
            Destroy(gameObject);
        }

        EnsurePrefabExists();
    }

    private void EnsurePrefabExists()
    {
        if (_cubePrefab == null)
        {
            CreateDefaultPrefab();
        }
    }

    private void CreateDefaultPrefab()
    {
        _cubePrefab = new GameObject("CubePrefab");
        _cubePrefab.AddComponent<BoxCollider>();
        _cubePrefab.AddComponent<MeshRenderer>();
        _cubePrefab.AddComponent<MeshFilter>().mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
        _cubePrefab.AddComponent<CubeController>();

        _cubePrefab.SetActive(false);
    }

    private GameObject CreateNewCube(CubeController parentCube)
    {
        GameObject newCube = Instantiate(_cubePrefab);

        Vector3 spawnPosition = parentCube.transform.position + GetRandomSpawnOffset();
        newCube.transform.position = spawnPosition;
        newCube.transform.localScale = parentCube.InitializeComponents() * 0.5f;

        SetupCubeComponents(newCube, parentCube.SplitChance * 0.5f);

        ColorChanger.InstanceColor.ApplyRandomColor(newCube);

        if (ColorChanger.InstanceColor != null)
        {
            ColorChanger.InstanceColor.ApplyRandomColor(newCube);
        }

        newCube.SetActive(true);
        return newCube;
    }

    private void SetupCubeComponents(GameObject cube, float splitChance)
    {
        if (!cube.TryGetComponent<Rigidbody>(out var rb)) rb = cube.AddComponent<Rigidbody>();
        rb.useGravity = true;

        if (cube.TryGetComponent<CubeController>(out var controller))
        {
            controller.SplitChance = splitChance;
        }
    }

    private Vector3 GetRandomSpawnOffset()
    {
        return Random.insideUnitSphere * _spawnOffset;
    }

    private void ApplyExplosionForce(List<GameObject> cubes, Vector3 explosionCenter)
    {
        foreach (GameObject cube in cubes)
        {
            if (cube.TryGetComponent<Rigidbody>(out var cubeRb))
            {
                cubeRb.AddExplosionForce(
                    _explosionForce,
                    explosionCenter,
                    _explosionRadius,
                    0f,
                    ForceMode.Impulse
                );
            }
        }
    }

    public GameObject CreateInitialCube(Vector3 position, Vector3 scale, float splitChance = 1f)
    {
        GameObject cube = Instantiate(_cubePrefab);
        cube.transform.position = position;
        cube.transform.localScale = scale;

        SetupCubeComponents(cube, splitChance);
        ColorChanger.InstanceColor.ApplyRandomColor(cube);

        cube.SetActive(true);
        return cube;
    }

    public void SplitCube(CubeController parentCube)
    {
        if (parentCube == null) return;

        int numberOfNewCubes = Random.Range(2, 7);
        List<GameObject> newCubes = new();

        for (int i = 0; i < numberOfNewCubes; i++)
        {
            GameObject newCube = CreateNewCube(parentCube);
            newCubes.Add(newCube);
        }

        ApplyExplosionForce(newCubes, parentCube.transform.position);

        parentCube.DestroyCube();
    }
}