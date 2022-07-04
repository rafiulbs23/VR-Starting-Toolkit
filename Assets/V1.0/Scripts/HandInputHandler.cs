using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandInputHandler : MonoBehaviour
{
    public bool ShowHands;
    public List<GameObject> controllerPrefabs;
    public InputDeviceCharacteristics AssignedCharacteristics;
    public GameObject HandModelPrefab;

    private InputDevice targetDevice;
    private List<InputDevice> devices;
    private GameObject spawnedHand;
    private GameObject spawnedController;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void UpdateHandAnimation()
    {
        //For Trigger Button Press
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            animator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            animator.SetFloat("Trigger", 0f);
        }


        //For Grip Button Press
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            animator.SetFloat("Grip", gripValue);
        }
        else
        {
            animator.SetFloat("Grip", 0f);
        }


    }

    void Initialize()
    {
        devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(AssignedCharacteristics, devices);
        //InputDevices.GetDevices(devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("No Target Controller Found");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHand = Instantiate(HandModelPrefab, transform);
            animator = spawnedHand.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            Initialize();
        }
        else
        {
            spawnedHand.SetActive(ShowHands);
            spawnedController.SetActive(!ShowHands);
            if (ShowHands)
            {
                UpdateHandAnimation();
            }
        }
        

        //if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        //    Debug.Log("Primary Button Pressed");
    }
}
