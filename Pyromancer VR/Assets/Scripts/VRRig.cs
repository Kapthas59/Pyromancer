using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffsset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffsset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;
    public Vector3 headBodyOffset;
    public float turnSmoothness = 1f;
    

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position + transform.GetChild(0).gameObject.transform.position - headConstraint.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.forward, Vector3.up).normalized, Time.unscaledDeltaTime * turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
