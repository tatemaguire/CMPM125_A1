using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
    }
    
    // Event callback for player input
    private void OnMove(InputValue action)
    {
        var movement = action.Get<Vector2>();
        _desiredAcceleration = movement.y;
    }
}
