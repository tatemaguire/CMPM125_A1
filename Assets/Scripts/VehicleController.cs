using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    // Properties
    [SerializeField] private float impulse = 4f;
    [SerializeField] private float turnrate = 200f;
    
    // Private Fields
    private Vector2 _controlVector;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        // Apply acceleration and strafing forces
        Vector3 force = new(
            _controlVector.y * impulse,
            0,
            -_controlVector.x * impulse
        );
        _rigidbody.AddRelativeForce(force);
        
        // Apply rotation from mouse position
        float dx = (Mouse.current.position.x.value - Screen.width / 2) / turnrate;
        if (Mathf.Abs(dx) > 0.01f)
        {
            transform.Rotate(0, dx, 0);
        }
    }
    
    // Event callback for player move input
    // Gets called on press and on release, so it keeps a consistent controlVector
    private void OnMove(InputValue action)
    {
        _controlVector = action.Get<Vector2>();
    }
}
