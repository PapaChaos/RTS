using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 150f;
    public DeviceType deviceType;
    public PlayerInput playerInput;
    [SerializeField]
    bool debug = false;

    public enum ControllerSelected
	{
        Default,
        Joystick
	}
    ControllerSelected controllerSelected = ControllerSelected.Default;

	private void Awake()
	{
        //playerInput = GetComponent<PlayerInput>();
        deviceType = SystemInfo.deviceType;
        if (debug)
        {
            if (deviceType == DeviceType.Desktop)
                Debug.LogWarning("Desktop");
            if (deviceType == DeviceType.Handheld)
                Debug.LogWarning("Handheld");
            if (deviceType == DeviceType.Console)
                Debug.LogWarning("Console");
            if (deviceType == DeviceType.Unknown)
                Debug.LogWarning("Unknown");

            Debug.LogWarning(controllerSelected);
        }
    }

    void Update()
    {
        //Camera Movement
        Vector3 pos = transform.position;
        pos += new Vector3(playerInput.actions["CameraMovement"].ReadValue<Vector2>().x, 0f, playerInput.actions["CameraMovement"].ReadValue<Vector2>().y);
        transform.position = pos;
    }
}
