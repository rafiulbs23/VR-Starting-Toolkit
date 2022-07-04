using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Locomotion_Controller : MonoBehaviour
{
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;

    public InputHelpers.Button teleportInteractionButton;

    public float activationThreshold = 0.2f;
    

    // Update is called once per frame
    void Update()
    {
        if (leftTeleportRay)
        {
            leftTeleportRay.gameObject.SetActive(CheckIfActivated(leftTeleportRay));
        }
        if (rightTeleportRay)
        {
            rightTeleportRay.gameObject.SetActive(CheckIfActivated(rightTeleportRay));
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportInteractionButton, out bool isActivated,
            activationThreshold);
        return isActivated;
    }
}
