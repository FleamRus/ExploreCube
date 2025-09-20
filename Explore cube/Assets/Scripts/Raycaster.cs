using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public event Action<Cube> CubeHit;

    private Camera _camera;
    private LayerMask _cubeLayerMask = ~0;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        int numberMouseKey = 0;

        if (Input.GetMouseButtonDown(numberMouseKey))
        {
            FireRay();
        }
    }

    private void FireRay()
    {
        float maximumDistance = 100f;

        Ray raycast = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(raycast, out RaycastHit hit, maximumDistance, _cubeLayerMask))
        {
            if (hit.collider.TryGetComponent<Cube>(out var cubeReferens))
            {
                CubeHit?.Invoke(cubeReferens);
            }
        }
    }
}
