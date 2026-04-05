using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 10f;
    
    private float _desiredAcceleration;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Vector3 force = new(_desiredAcceleration * accelerationForce, 0, 0);
        _rigidbody.AddRelativeForce(force);
        
        float dx = (Mouse.current.position.x.value - Screen.width / 2) / 200;
        if (Mathf.Abs(dx) > 0.01f)
        {
            transform.Rotate(0, dx, 0);
        }
    }
    
    // Event callback for player input
    private void OnMove(InputValue action)
    {
        var movement = action.Get<Vector2>();
        _desiredAcceleration = movement.y;
    }
}
