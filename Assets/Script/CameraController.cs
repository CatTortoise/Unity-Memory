
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    [SerializeField] private CursorController cursorController;
    [SerializeField] private float panBordreThickness;
    [SerializeField] private float cameraMovmentSpeed;
    [SerializeField] private Rigidbody cameraRigidbody;
    [SerializeField] private float rotationSpeed;


    private Vector3 moveTo;
    private Vector3 cameraPosition;
    private Vector3 moveInDirection;

    private void Update()
    {
        cameraPosition = transform.position;
        moveInDirection = CheckedDirection();
    }

    private void FixedUpdate()
    {
        cameraPosition = transform.position;
        moveTo = (cameraPosition + moveInDirection * Time.fixedDeltaTime * cameraMovmentSpeed);
        moveTo = Vector3.Lerp(cameraPosition, moveTo, cameraMovmentSpeed);
        cameraRigidbody.MovePosition(moveTo);

    }

    private Vector3 CheckedDirection()
    {
        Vector3 direction = new(0, 0, 0);

        if (Input.GetKey(forward) || Input.mousePosition.y >= Screen.height - panBordreThickness)
        {
            direction.z = -1;
        }
        else if (Input.GetKey(backward) || Input.mousePosition.y <= panBordreThickness)
        {
            direction.z = 1;
        }
        if (Input.GetKey(right) || Input.mousePosition.x >= Screen.width - panBordreThickness)
        {
            direction.x = -1;
        }
        else if (Input.GetKey(left) || Input.mousePosition.x <= panBordreThickness)
        {
            direction.x = 1;
        }

        return direction;
    }
   
    
}