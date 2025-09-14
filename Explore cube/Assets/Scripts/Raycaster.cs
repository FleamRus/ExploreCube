using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public event Action<Cube> CubeHit;

    private Camera cam;
    private LayerMask cubeLayerMask = ~0;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireRay();
        }
    }

    private void FireRay()
    {
        float maximumDistance = 100f;

        Ray raycast = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(raycast, out RaycastHit hit, maximumDistance, cubeLayerMask))
        {
            if (hit.collider.TryGetComponent<Cube>(out var cubeReferens))
            {
                CubeHit?.Invoke(cubeReferens);
            }
        }
    }
}
