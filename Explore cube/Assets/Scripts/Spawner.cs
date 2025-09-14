using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private int initialCount = 3;
    [SerializeField] private float spawnRadius = 0.5f;
    [SerializeField] private Vector3 spawnArea = new(5, 1, 5);

    private readonly List<Cube> cubes = new();

    private void Start()
    {
        for (int i = 0; i < initialCount; i++)
        {
            float minValueY = 0.5f;

            Vector3 position = transform.position + new Vector3(
                UnityEngine.Random.Range(-spawnArea.x, spawnArea.x),
                UnityEngine.Random.Range(minValueY, spawnArea.y + minValueY),
                UnityEngine.Random.Range(-spawnArea.z, spawnArea.z)
            );

            SpawnInitial(position);
        }
    }

    private void SpawnInitial(Vector3 position)
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Spawner: cubePrefab не назначен!");
            return;
        }

        Cube cube = Instantiate(cubePrefab, position, Quaternion.identity);

        cube.SetOriginalScale(cube.transform.localScale);
        cubes.Add(cube);
    }

    public List<Cube> SpawnChildren(Cube parent)
    {
        int minValueRandom = 2;
        int maxValueRandom = 7;
        float valueDivider = 0.5f;

        List<Cube> created = new();
        int count = Random.Range(minValueRandom, maxValueRandom);

        Vector3 parentScale = parent.transform.localScale;
        Vector3 childScale = parentScale * valueDivider;

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = parent.transform.position + Random.insideUnitSphere * spawnRadius;
            Cube child = Instantiate(cubePrefab, pos, Quaternion.identity);

            child.transform.localScale = childScale;

            child.SetOriginalScale(child.transform.localScale);

            child.SetSplitChance(parent.SplitChance * valueDivider);

            created.Add(child);
            cubes.Add(child);
        }

        cubes.Remove(parent);
        Destroy(parent.gameObject);

        return created;
    }
}
