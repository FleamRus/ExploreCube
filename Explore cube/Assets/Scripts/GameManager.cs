using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Initial Setup")]
    [SerializeField] private int _initialCubesCount = 5;
    [SerializeField] private Vector3 _initialCubeScale = Vector3.one;
    [SerializeField] private Vector3 _spawnArea = new(10f, 2f, 10f);

    [Header("Gizmos Settings")]
    [SerializeField] private bool _showSpawnAreaInEditor = true;
    [SerializeField] private Color _spawnAreaColor = new(1, 0, 0, 0.3f);

    private void Start()
    {
        InitializeGame();
    }

    private void OnDrawGizmos()
    {
        if (!_showSpawnAreaInEditor) return;

        Gizmos.color = _spawnAreaColor;
        Gizmos.DrawCube(transform.position + new Vector3(0, _spawnArea.y / 2, 0), _spawnArea);
    }

    private void InitializeGame()
    {
        CreateInitialCubes();
    }

    private void CreateInitialCubes()
    {
        for (int i = 0; i < _initialCubesCount; i++)
        {
            CreateInitialCube();
        }
    }

    private void CreateInitialCube()
    {
        Vector3 randomPosition = new(
            Random.Range(-_spawnArea.x / 2, _spawnArea.x / 2),
            Random.Range(_spawnArea.y / 2, _spawnArea.y),
            Random.Range(-_spawnArea.z / 2, _spawnArea.z / 2)
        );

        CubeCreator.InstanceCreator.CreateInitialCube(randomPosition, _initialCubeScale, 1f);
    }
}