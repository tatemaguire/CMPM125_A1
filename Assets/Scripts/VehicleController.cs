using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    // Vehicle Properties
    [SerializeField] private float minAcceleration = 4f;
    [SerializeField] private float maxAcceleration = 8f;
    [SerializeField] private float turnrate = 600f;
    
    // References
    public CheckpointController target;
    public TextMeshProUGUI timeLabel;
    public TextMeshProUGUI lapLabel;
    
    // Private Fields
    private Vector2 _controlVector;
    private Rigidbody _rigidbody;
    private float _startTime;
    private int _currentLap = 0;

    private void Start()
    {
        // Get references
        _rigidbody = GetComponent<Rigidbody>();
        
        // Set target checkpoint color
        target.leftPole.materials[0].color = CheckpointController.TargetedColor;
        target.rightPole.materials[0].color = CheckpointController.TargetedColor;
        
        // Set Start Time
        _startTime = Time.time;
    }
    
    private void Update()
    {
        // Determine the component of the control vector pointing against velocity
        float dot = Vector3.Dot(_rigidbody.linearVelocity.normalized, GetSpatialControlVector().normalized);
        // Use it to interpolate acceleration impulse
        float impulse = Mathf.Lerp(maxAcceleration, minAcceleration, (dot + 1) / 2f);

        Vector3 force = new(_controlVector.y * impulse, 0, _controlVector.x * -impulse);
        _rigidbody.AddRelativeForce(force);
        
        // Debug.DrawRay(transform.position, transform.rotation * force, Color.yellowGreen, 0.5f, false);
        // Debug.Log(impulse);
        
        // Apply rotation from mouse position
        float dx = (Mouse.current.position.x.value - Screen.width / 2) / turnrate;
        if (Mathf.Abs(dx) > 0.01f)
        {
            transform.Rotate(0, dx, 0);
        }
        
        // Update text label
        timeLabel.text = $"Current time: {(Time.time - _startTime):F2} seconds";
    }
    
    // Event callback for player move input
    // Gets called on press and on release, so it keeps a consistent controlVector
    private void OnMove(InputValue action)
    {
        _controlVector = action.Get<Vector2>();
    }

    // Gets the control vector in 3D space according to vehicle's rotation
    // In the future, could be switched to camera's rotation
    private Vector3 GetSpatialControlVector()
    {
        Vector3 result = new(_controlVector.y, 0, -_controlVector.x);
        result = transform.rotation * result;
        return result;
    }
    
    // Event callback for starting a lap
    public void OnLapStart()
    {
        // Reset timer
        _startTime = Time.time;
        
        // Increment lap counter and display
        _currentLap++;
        lapLabel.text = $"Lap #{_currentLap}";
    }
}
