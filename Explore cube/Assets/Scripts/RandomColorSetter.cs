using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RandomColorSetter : MonoBehaviour
{
    private Renderer cechedRenderer;

    private void Awake()
    {
        cechedRenderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        cechedRenderer.material.color = new Color(
            Random.value, Random.value, Random.value);
    }
}
