using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    // References
    public CheckpointController next;
    public MeshRenderer leftPole;
    public MeshRenderer rightPole;
    
    // Private fields
    private static Color _defaultColor;
    public static readonly Color TargetedColor = Color.red;

    private void Start()
    {
        _defaultColor = leftPole.materials[0].color;
    }

    private void OnTriggerEnter(Collider other)
    {
        VehicleController vehicle = other.gameObject.GetComponent<VehicleController>();
        if (vehicle == null) return;
        if (vehicle.target == this)
        {
            // Update vehicle's target checkpoint
            vehicle.target = next;
                
            // Update colors
            next.leftPole.materials[0].color = TargetedColor;
            next.rightPole.materials[0].color = TargetedColor;
            leftPole.materials[0].color = _defaultColor;
            rightPole.materials[0].color = _defaultColor;
        }
    }
}
