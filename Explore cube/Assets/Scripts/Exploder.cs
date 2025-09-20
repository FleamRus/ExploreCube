using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [Header("Explosion settings")]
    [SerializeField] private float _explosionForce = 200f;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _upwardsModifier = 0.5f;

    public void Explode(Vector3 center, List<Cube> affectedCubes)
    {
        if (affectedCubes == null || affectedCubes.Count == 0) 
            return;

        foreach (var cube in affectedCubes)
        {
            if (cube == null) 
                continue;

            if (!cube.TryGetComponent<Rigidbody>(out var rigidbody)) 
                continue;

            rigidbody.AddExplosionForce(_explosionForce, center, _explosionRadius, _upwardsModifier, ForceMode.Impulse);
        }
    }
}
