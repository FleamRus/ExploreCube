using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1f;
    public float SplitChance => _splitChance;

    public Vector3 OriginalScale { get; private set; }

    private void Awake()
    {
        OriginalScale = transform.localScale;
    }

    public void SetSplitChance(float value)
    {
        _splitChance = Mathf.Clamp01(value);
    }

    public void SetOriginalScale(Vector3 scale)
    {
        OriginalScale = scale;
    }
}
