using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RandomColorSetter : MonoBehaviour
{
    private Renderer _cechedRenderer;

    private void Awake()
    {
        _cechedRenderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _cechedRenderer.material.color = new Color(
            Random.value, Random.value, Random.value);
    }
}
