using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;
using UnityEngine.AI;

public class PlayerFunction : MonoBehaviour
{
    private bool canTeleport;
    public float teleportSpeed = 0.001f;
    public float moveSpeed = 0.001f;
    public float teleportDistance = 3f;
    public Transform XROrigin;
    public Vector3 unpausedPosition;
    public Vector3 pausedPosition = new Vector3(100, 0, 100);
    public GameObject menuAreaPrefab;
    public GameObject menuAreaGameObject;
    public bool menuAreaActive = false;
    public UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual leftControllerRay;
    public UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual rightControllerRay;

    private UnityEngine.XR.InputDevice rightController;
    private UnityEngine.XR.InputDevice leftController;
    private VRRig vrRig;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        canTeleport = true;
        vrRig = GetComponent<VRRig>();
        unpausedPosition = XROrigin.position;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // Teleport
        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);
        
        if (rightHandedControllers.Count == 1)
        {
            rightController = rightHandedControllers[0];
            Vector2 joyStickValue;
            if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joyStickValue))
            {
                if(canTeleport && joyStickValue.magnitude > 0.95 && Time.timeScale == 1)
                {
                    
                    StartCoroutine(Teleport(joyStickValue));
                }
                StartCoroutine(TeleportReload(joyStickValue));
            }
            
        }
        else if (rightHandedControllers.Count > 1)
        {
            Debug.Log("Found more than one right hand!");
        }

        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);
        
        if (leftHandedControllers.Count == 1)
        {
            leftController = leftHandedControllers[0];
            bool menuButtonValue;
            if (leftController.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonValue))
            {
                if (menuButtonValue)
                {
                    if (!menuAreaActive) {
                        Time.timeScale = 0;
                        unpausedPosition = new Vector3(XROrigin.position.x, XROrigin.position.y, XROrigin.position.z);
                        
                        XROrigin.position = new Vector3(pausedPosition.x, pausedPosition.y, pausedPosition.z);
                        transform.position = new Vector3(pausedPosition.x, pausedPosition.y, pausedPosition.z);
                        XROrigin.position = new Vector3(pausedPosition.x, pausedPosition.y, pausedPosition.z);
                        transform.position = new Vector3(pausedPosition.x, pausedPosition.y, pausedPosition.z);
                    
                        menuAreaGameObject = Instantiate(menuAreaPrefab, XROrigin.position, XROrigin.rotation);
                        menuAreaActive = true;
                    }
                    
                    leftControllerRay.enabled = true;
                    rightControllerRay.enabled = true;
                    
                }
            }
            Vector2 joyStickValue;
            if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joyStickValue))
            {
                if(Time.timeScale == 1 && joyStickValue.magnitude > 0.05)
                {
                    Vector3 moveVector = new Vector3(joyStickValue.x, 0, joyStickValue.y);
                    moveVector = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * moveVector;
                    moveVector = moveVector.normalized;
                    Vector3 targetPosition = new Vector3(transform.position.x + moveVector.x, 0, transform.position.z + moveVector.z);
                    
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(targetPosition, out hit, 0.1f, 1)) {
                        float step = moveSpeed * Time.deltaTime;
                        XROrigin.position = Vector3.MoveTowards(XROrigin.position, targetPosition, moveSpeed);
                    }
                }
            }
            
        }
        else if (leftHandedControllers.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }
    }

    private IEnumerator Teleport(Vector2 joyStickValue)
    {
        canTeleport = false;
        
        Vector3 teleportVector = new Vector3(joyStickValue.x, 0, joyStickValue.y);
        teleportVector = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * teleportVector;
        teleportVector = teleportVector * teleportDistance;
        Vector3 targetPosition = new Vector3(transform.position.x + teleportVector.x, 0, transform.position.z + teleportVector.z);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 0.1f, 1)) {
            audioSource.Play();
            float step = teleportSpeed * Time.deltaTime;

            yield return new WaitForSeconds(step);

            while (Vector3.Distance(XROrigin.position, targetPosition) > 0.01)
            {
                XROrigin.position = Vector3.MoveTowards(XROrigin.position, targetPosition, step);
            }           
        }
    }

    private IEnumerator TeleportReload(Vector2 joyStickValue)
    {
        if (joyStickValue.magnitude < 0.95)
        {
            canTeleport = true;
        }

        yield return canTeleport;
    }
}
