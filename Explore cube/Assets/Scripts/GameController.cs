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

    private void HandleCubeHit(Cube cube)
    {
        if (cube == null) return;

        if (Random.value < cube.SplitChance)
        {
            List<Cube> children = spawner.SpawnChildren(cube);
            exploder.Explode(cube.transform.position, children);
        }
        else
        {
            Destroy(cube.gameObject);
        }
    }
}
