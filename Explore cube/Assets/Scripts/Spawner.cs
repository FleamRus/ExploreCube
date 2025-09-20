using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _initialCount = 3;
    [SerializeField] private float _spawnRadius = 0.5f;
    [SerializeField] private Vector3 _spawnArea = new(5, 1, 5);

    private readonly List<Cube> _cubes = new();

    private void Start()
    {
        for (int i = 0; i < _initialCount; i++)
        {
            float minValueY = 0.5f;

            Vector3 position = transform.position + new Vector3(
                UnityEngine.Random.Range(-_spawnArea.x, _spawnArea.x),
                UnityEngine.Random.Range(minValueY, _spawnArea.y + minValueY),
                UnityEngine.Random.Range(-_spawnArea.z, _spawnArea.z)
            );

            SpawnInitial(position);
        }
    }

    private void SpawnInitial(Vector3 position)
    {
        if (_cubePrefab == null)
                    return;
        

        Cube cube = Instantiate(_cubePrefab, position, Quaternion.identity);

        cube.SetOriginalScale(cube.transform.localScale);
        _cubes.Add(cube);
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
            Vector3 pos = parent.transform.position + Random.insideUnitSphere * _spawnRadius;
            Cube child = Instantiate(_cubePrefab, pos, Quaternion.identity);

            child.transform.localScale = childScale;

            child.SetOriginalScale(child.transform.localScale);

            child.SetSplitChance(parent.SplitChance * valueDivider);

            created.Add(child);
            _cubes.Add(child);
        }

        _cubes.Remove(parent);
        Destroy(parent.gameObject);

        return created;
    }
}
