using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1f;
    private Vector3 _originalScale;

    public float SplitChance
    {
        get => _splitChance;
        set => _splitChance = value;
    }

    private void Start()
    {
        InitializeComponents();

        if (ColorChanger.InstanceColor != null)
        {
            ColorChanger.InstanceColor.ApplyRandomColor(gameObject);
        }
    }

    public Vector3 InitializeComponents()
    {
        _originalScale = transform.localScale;

        return _originalScale;
    }

    public void DestroyCube()
    {
        Destroy(gameObject);
    }

    public void ProcessCubeClick(CubeController cubeController)
    {
        if (cubeController == null) return;

        if (Random.value <= cubeController._splitChance)
        {
            CubeCreator.InstanceCreator.SplitCube(cubeController);
        }
        else
        {
            cubeController.DestroyCube();
        }
    }
}