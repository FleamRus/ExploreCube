using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [Header("Color Settings")]
    [SerializeField] private Color[] _possibleColors;
    [SerializeField] private float _minColorValue = 0.2f;
    [SerializeField] private float _maxColorValue = 1f;

    public static ColorChanger InstanceColor { get; private set; }

    private void Awake()
    {
        if (InstanceColor == null)
        {
            InstanceColor = this;
        }

        InitializeDefaultColors();
    }

    private void InitializeDefaultColors()
    {
        if (_possibleColors == null || _possibleColors.Length == 0)
        {
            _possibleColors = new Color[]
            {
                Color.red,
                Color.green,
                Color.blue,
                Color.yellow,
                Color.cyan,
                Color.magenta,
            };
        }
    }

    private void ApplyColorToObject(GameObject target, Color color)
    {
        if (target == null) return;

        if (!target.TryGetComponent<Renderer>(out var renderer))
        {
            renderer = target.AddComponent<MeshRenderer>();
        }

        Material newMaterial = new(Shader.Find("Standard"))
        {
            color = color
        };

        renderer.material = newMaterial;
    }

    private Color GetRandomColor()
    {
        if (_possibleColors != null && _possibleColors.Length > 0 && Random.value > 0.5f)
        {
            return _possibleColors[Random.Range(0, _possibleColors.Length)];
        }

        return new Color(
            Random.Range(_minColorValue, _maxColorValue),
            Random.Range(_minColorValue, _maxColorValue),
            Random.Range(_minColorValue, _maxColorValue)
        );
    }

    public void ApplyRandomColor(GameObject target)
    {
        if (target == null) return;

        Color randomColor = GetRandomColor();
        ApplyColorToObject(target, randomColor);
    }
}