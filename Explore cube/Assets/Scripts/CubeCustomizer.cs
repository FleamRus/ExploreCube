using System;
using UnityEngine;

public class CubeCustomizer : MonoBehaviour
{
    public event Action<CubeCustomizer> Clicked;

    public float SplitChance { get; private set; } = 1f;

    public Spawner CreatorSpawner { get; private set; }

    public void Initialize(Spawner creator, Vector3 localScale, float splitChance)
    {
        CreatorSpawner = creator;
        transform.localScale = localScale;
        SplitChance = splitChance;
    }

    public void NotifyClicked()
    {
        Clicked?.Invoke(this);
    }

    public void ApplyRandomColor()
    {
        if (TryGetComponent<Renderer>(out var renderer))
        {
            renderer.material = new Material(renderer.material);
            renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }
    }
}
