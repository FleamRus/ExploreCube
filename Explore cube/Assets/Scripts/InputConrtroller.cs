using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private CubeController _cubeController;

    public static InputController InstanceInput { get; private set; }

    private void Awake()
    {
        if (InstanceInput == null)
        {
            InstanceInput = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        int numberButtom = 0;
        if (Input.GetMouseButtonDown(numberButtom))
        {
            ProcessMouseClick();
        }
    }

    private void ProcessMouseClick()
    {
        LayerMask _cubeLayerMask = 1;
        float _maxRayDistance = 100f;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxRayDistance, _cubeLayerMask))
        {
            if (hit.collider.TryGetComponent<CubeController>(out var cubeController))
            {
                cubeController.ProcessCubeClick(cubeController);
            }
        }
    }
}