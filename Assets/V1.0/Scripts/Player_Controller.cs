using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class Player_Controller : MonoBehaviour
{
    public XRNode inputSource;
    public float speed = 1f;
    public float additionalHeight = 0.1f;
    private XROrigin rig;
    private Vector2 inputAxis;
    private CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    void FixedUpdate()
    {
        FollowHeadsetPosition();
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0f);
        Vector3 direction = headYaw * new Vector3(0f, 0f, inputAxis.y);
        controller.Move(direction * Time.fixedDeltaTime * speed);
    }

    public void FollowHeadsetPosition()
    {
        controller.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        controller.center = new Vector3(capsuleCenter.x, controller.height/2 + controller.skinWidth , capsuleCenter.z);

    }
}
