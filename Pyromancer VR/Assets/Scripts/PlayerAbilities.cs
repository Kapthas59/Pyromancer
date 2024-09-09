using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

public class PlayerAbilities : MonoBehaviour
{
    public enum ControllerIndicator
    {
        Right,
        Left
    }
    public ControllerIndicator controllerIndicator;
    public Transform abilitySpawnPoint;
    //public Transform fireWallSpawnPoint;

    public GameObject fireBallPrefab;
    public GameObject fireStreamPrefab;
    public GameObject fireWallPrefab;

    public enum AbilityType
    {
        FireBall,
        FireStream,
        FireWall
    }
    public AbilityType abilityType;

    public AudioClip fireBallSpawn;
    public AudioClip fireStreamSpawn;
    public AudioClip fireStreamDespawn;
    public AudioClip fireWallSpawn;

    private UnityEngine.XR.InputDevice controller;
    private bool gripPressed = false;
    private bool readyAbility = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var Controllers = new List<UnityEngine.XR.InputDevice>();

        if (controllerIndicator == ControllerIndicator.Right)
        {
            var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, Controllers);
        }
        else
        {
            var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, Controllers);
        }

        if (Controllers.Count == 1)
        {
            controller = Controllers[0];
            controller.TryGetFeatureValue(CommonUsages.gripButton, out gripPressed);

            bool primaryPress = false;
            controller.TryGetFeatureValue(CommonUsages.primaryButton, out primaryPress);
            if (primaryPress)
            {
                abilityType = AbilityType.FireStream;
            }

            bool secondaryPress = false;
            controller.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryPress);
            if (secondaryPress)
            {
                abilityType = AbilityType.FireWall;
            }

            bool triggerPress = false;
            controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPress);
            if (triggerPress)
            {
                abilityType = AbilityType.FireBall;
            }
        }
        else if (Controllers.Count > 1)
        {
            Debug.Log("Found more than one " + controllerIndicator.ToString() + " hand!");
        }
    }

    private void OnTriggerEnter(Collider area)
    {
        string parentTag = "XROrigin";
        if (area.transform.parent != null) {
            parentTag = area.transform.parent.tag;
        }
        
        if (abilityType == AbilityType.FireBall && parentTag == "PlayerAbilityArea1")
        {
            if (area.tag == "PlayerReadyAbility")
            {
                readyAbility = true;
            }
            else if (area.tag == "PlayerActivateAbility")
            {
                if (gripPressed && readyAbility)
                {
                    var fireBall = Instantiate(fireBallPrefab, abilitySpawnPoint.position, abilitySpawnPoint.rotation);
                    fireBall.GetComponent<Rigidbody>().velocity = abilitySpawnPoint.forward * fireBall.GetComponent<FireBall>().ballSpeed;
                    audioSource.clip = fireBallSpawn;
                    audioSource.Play();
                }
                readyAbility = false;
            }
        }
        else if (abilityType == AbilityType.FireStream && parentTag == "PlayerAbilityArea1")
        {
            if (area.tag == "PlayerReadyAbility")
            {
                if (transform.childCount > 1) {
                    var fireStream = transform.GetChild(1);
                    if (fireStream != null)
                    {
                        Destroy(fireStream.gameObject);
                        audioSource.clip = fireStreamDespawn;
                        audioSource.Play();
                    }
                }
                
                readyAbility = true;
            }
            else if (area.tag == "PlayerActivateAbility")
            {
                if (gripPressed && readyAbility)
                {
                    audioSource.clip = fireStreamSpawn;
                    audioSource.Play();
                    var tempRotation = abilitySpawnPoint.rotation;
                    //tempRotation.eulerAngles = new Vector3(tempRotation.eulerAngles.x - 90, tempRotation.eulerAngles.y, tempRotation.eulerAngles.z);
                    tempRotation.eulerAngles = new Vector3(tempRotation.eulerAngles.x - 90, tempRotation.eulerAngles.z, tempRotation.eulerAngles.y);
                    var fireStream = Instantiate(fireStreamPrefab, abilitySpawnPoint.position, tempRotation);
                    fireStream.transform.SetParent(transform);
                }
                readyAbility = false;
            }
        }
        else if (abilityType == AbilityType.FireWall && parentTag == "PlayerAbilityArea2")
        {
            if (area.tag == "PlayerReadyAbility")
            {
                readyAbility = true;
            }
            else if (area.tag == "PlayerActivateAbility")
            {
                if (gripPressed && readyAbility)
                {
                    var fireWall = Instantiate(fireWallPrefab, abilitySpawnPoint.position, Quaternion.identity);
                    audioSource.clip = fireWallSpawn;
                    audioSource.Play();
                }
                readyAbility = false;
            }
        }
    }
}
