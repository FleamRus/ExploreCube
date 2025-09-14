using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float splitChance = 1f;
    public float SplitChance => splitChance;

    public Vector3 OriginalScale { get; private set; }

    private void Awake()
    {
        OriginalScale = transform.localScale;
    }

    public void SetSplitChance(float value)
    {
        splitChance = Mathf.Clamp01(value);
    }

    public void SetOriginalScale(Vector3 scale)
    {
        OriginalScale = scale;
    }
}
