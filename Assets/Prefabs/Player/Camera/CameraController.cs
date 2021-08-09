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
        Vector3 pos = transform.position;
        pos += new Vector3(playerInput.actions["CameraMovement"].ReadValue<Vector2>().x, 0f, playerInput.actions["CameraMovement"].ReadValue<Vector2>().y);
        /*if (deviceType == DeviceType.Desktop)
        {

            if(Input.GetAxis("Vertical") != 0)
			{
                pos.z += Input.GetAxis("Vertical")*(panSpeed * Time.deltaTime);
            }*/
            /*
            if (Input.GetKey("w"))
            {
                pos.z += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s"))
            {
                pos.z -= panSpeed * Time.deltaTime;
            }*/
           /* if (Input.GetAxis("Horizontal") != 0)
            {
                pos.x += Input.GetAxis("Horizontal")*(panSpeed * Time.deltaTime);
            }*/

           // pos += new Vector3(playerInput.actions["CameraMovement"].ReadValue<Vector2>().x, 0f, playerInput.actions["CameraMovement"].ReadValue<Vector2>().y);
            /*
            if (Input.GetKey("a"))
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d"))
            {
                pos.x += panSpeed * Time.deltaTime;
            }*/
       /* }
        if (deviceType == DeviceType.Handheld)
        {*/
            //pos += new Vector3(playerInput.actions[""].ReadValue<Vector2>().x, 0f, playerInput.actions["CameraMovement"].ReadValue<Vector2>().y);

            //Touch touch = Input.GetTouch(0);
            //Vector2 touchstartpos;
            /*if (Input.touchCount == 1)
            {
                touchstartpos = new Vector2(0, 0);
                if (touch.phase == TouchPhase.Began)
                    touchstartpos = touch.position;

                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
                {
                    Vector2 touchEndPosition = touch.position;
                    //string direction;
                    pos.x = touchEndPosition.x - touchstartpos.x;
                    pos.z = touchEndPosition.y - touchstartpos.y;

                    /*
                    if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                    {
                        direction = “Tapped”;
                    }

                    else if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        direction = x > 0 ? “Right” : “Left”;
                    }

                    else
                    {
                        direction = y > 0 ? “Up” : “Down”;
                    }
                }
            }*/
        //}
        transform.position = pos;
    }
}
