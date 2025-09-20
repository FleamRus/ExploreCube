using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnCoordinator : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;

    private void OnEnable()
    {
        if (_raycaster != null)
            _raycaster.CubeHit += HandleCubeHit;
    }

    private void OnDisable()
    {
        if (_raycaster != null)
            _raycaster.CubeHit -= HandleCubeHit;
    }

    private void HandleCubeHit(Cube cube)
    {
        if (cube == null) 
            return;

        if (Random.value < cube.SplitChance)
        {
            List<Cube> children = _spawner.SpawnChildren(cube);
            _exploder.Explode(cube.transform.position, children);
        }
        else
        {
            Destroy(cube.gameObject);
        }
    }
}
