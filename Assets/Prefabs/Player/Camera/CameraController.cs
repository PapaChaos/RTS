using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public DeviceType deviceType;
    public PlayerInput playerInput;
    [SerializeField]
    bool debug = false;

    [SerializeField]
    UI_FactionInfo FI;
    [SerializeField]
    FactionResources FR;
    [SerializeField]
    Spawners HQ;
    [SerializeField]
    GameObject PlayerUnit;
    public Vector3 PressLoc;

    PressLocationParticleScript pressParticle;

    [SerializeField]
    public Camera camera;

    public enum ControllerSelected
	{
        Default,
        Joystick
	}
    ControllerSelected controllerSelected = ControllerSelected.Default;

    public float XLocationminimum = 0, XLocationMax = 1000, ZLocationminimum = 0, ZLocationMax = 1000;



    private void Awake()
	{
        pressParticle = GetComponent<PressLocationParticleScript>();

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

	private void Start()
	{
        FI = GetComponent<UI_FactionInfo>();
        FR = FI.factionResources;
        HQ = FR.GetComponent<Spawners>();

    }
	void Update()
    {
        PlayerUnit = HQ.playerUnit;

        //Camera Movement
        Vector3 pos = transform.position;
        pos += new Vector3(playerInput.actions["CameraMovement"].ReadValue<Vector2>().x*panSpeed, 0f, playerInput.actions["CameraMovement"].ReadValue<Vector2>().y * panSpeed);
        
        if(pos.x < XLocationminimum)
            pos.x = XLocationminimum;

        else if( pos.x > XLocationMax) 
            pos.x = XLocationMax;

        if (pos.z < ZLocationminimum)
            pos.z = ZLocationminimum;

        else if (pos.z > ZLocationMax)
            pos.z = ZLocationMax;


        transform.position = pos;



        if (PlayerUnit)
        {

            if (playerInput.actions["MousePress"].ReadValue<float>() > 0)
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(playerInput.actions["MousePointerLocation"].ReadValue<Vector2>());

                if (!EventSystem.current.IsPointerOverGameObject())
                {

                    if (Physics.Raycast(ray, out hit))
                    {
                        pressParticle.StartParticle(hit.point);
                        //Debug.DrawLine(camera.transform.position, hit.point, Color.red, 5f);
                        PlayerUnit.GetComponent<UnitController>().targetLocation = hit.point;
                        if (hit.transform.gameObject.GetComponent<ResourceNode>())
                        {
                            float dist = Vector3.Distance(PlayerUnit.transform.position, hit.transform.position);
                            if (dist < 15f)
                            {

                                BaseInfo GOI = GetComponent<BaseInfo>();
                                BaseInfo hitGOI = hit.transform.gameObject.GetComponent<BaseInfo>();
                                hitGOI.faction = GOI.faction;
                                hitGOI.updateMaterials();
                            }
                        }
                    }
                }
            }
            if (playerInput.actions["MousePress"].ReadValue<float>() == 0)
            {
                pressParticle.StopParticle();
            }
        }
    }
}
