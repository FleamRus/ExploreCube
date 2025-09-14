using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Raycaster raycaster;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Exploder exploder;

    private void OnEnable()
    {
        if (raycaster != null)
            raycaster.CubeHit += HandleCubeHit;
    }
    
    private void OnDisable()
    {
        if (raycaster != null)
            raycaster.CubeHit -= HandleCubeHit;
    }

    private void Start()
    {
        if (raycaster == null) Debug.LogWarning("GameController: raycaster не назначен");
        if (spawner == null) Debug.LogWarning("GameController: spawner не назначен");
        if (exploder == null) Debug.LogWarning("GameController: exploder не назначен");
    }

    private void HandleCubeHit(CubeCustomizer cube)
    {
        if (cube == null) return;

        float chance = cube.SplitChance;
        float random = Random.value;
        bool willSplit = random < chance;

        if (willSplit)
        {
            List<CubeCustomizer> createdCubes = spawner.SpawnChildren(cube);

            exploder.Explode(cube.transform.position, createdCubes);
        }
    }
}
