using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform rootTransform;
    public Transform pivotTransform;
    public Transform zoomTransform;
    [Space]
    public Vector3 startPosition = new Vector3(10, 0, 10);
    public Vector3 startRotation = new Vector3(60, 0, 0);
    public Vector3 startZoom = new Vector3(0, 0, 5);
    [Space]
    public float moveSpeed = 10f;
    public float mouseMoveFactor = 1f;
    [Space]
    public float rotationSpeed = 60f;
    public float mouseRotationFactor = 2f;
    [Space]
    public float zoomSpeed = 10f;
    private float mouseZoom = 0f;
    private float mouseZoomSpeed = 2.0f;
    public float mouseZoomFactor = 15f;
    public float mouseZoomGravity = 0.4f;
    public float mouseZoomGravityMin = 0.3f;
    public Vector2 zoomLimits = new Vector2(5, 10);
    [Space]
    public float speedUpFactor = 1.5f;

    public Vector3 boundariesMin = new Vector3(0, 0, 0);
    public Vector3 boundariesMax = new Vector3(100, 0, 100);

    public float ZoomLevel
    {
        get { return (-zoomTransform.localPosition.z) / zoomLimits.x; }
    }

    private void Start()
    {
        rootTransform.localPosition = startPosition;
        pivotTransform.localEulerAngles = startRotation;
        zoomTransform.localPosition = -startZoom;
    }

    void Update()
    {
        MoveCamera();
        LimitCameraMovement();
        RotateCamera();
        ZoomCamera();
        ZoomCameraMouse();
    }


    private void MoveCamera()
    {
        //get move input
        if (Input.GetButton("CameraControlToggle")) return;
        Vector3 p = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (p.magnitude == 0) return;

        //add movement to transform
        p *= (1 + speedUpFactor * Input.GetAxis("CameraSpeedUp"));
        p *= moveSpeed * Time.deltaTime * ZoomLevel;
        p = Quaternion.AngleAxis(pivotTransform.localEulerAngles.y, Vector3.up) * p;
        rootTransform.localPosition += p;
    }


    private void MoveCameraMouse()
    {
        //get move input
        Vector3 p = new Vector3();
        if (Input.GetMouseButton(2))
            p -= new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")) * mouseMoveFactor;
        if (p.magnitude == 0) return;

        //add movement to transform
        p *= (1 + speedUpFactor * Input.GetAxis("CameraSpeedUp"));
        p *= moveSpeed * Time.deltaTime * ZoomLevel;
        rootTransform.localPosition += p;
    }


    private void LimitCameraMovement()
    {
        rootTransform.localPosition = new Vector3(
            Mathf.Clamp(rootTransform.localPosition.x, boundariesMin.x, boundariesMax.x), 0,
            Mathf.Clamp(rootTransform.localPosition.z, boundariesMin.z, boundariesMax.z));
        zoomTransform.localPosition = new Vector3(0, 0, Mathf.Clamp(zoomTransform.localPosition.z, -zoomLimits.y, -zoomLimits.x));
    }


    private void RotateCamera()
    {
        //get rotation input
        Vector3 p = new Vector3(0, -Input.GetAxis("CameraRotationX"), 0);

        if (Input.GetButton("CameraControlToggle"))
            p += new Vector3(0, Input.GetAxis("Horizontal"), 0);
        if (Input.GetMouseButton(1))
            p += new Vector3(0, Input.GetAxis("Mouse X"), 0) * mouseRotationFactor;
        if (p.magnitude == 0) return;

        //add rotation to transform
        p *= (1 + speedUpFactor * Input.GetAxis("CameraSpeedUp"));
        p *= rotationSpeed * Time.deltaTime;
        pivotTransform.localEulerAngles += p;
    }


    private void ZoomCamera()
    {
        //get zoom input
        Vector3 p = new Vector3(0, 0, -Input.GetAxis("Zoom"));
        if (Input.GetButton("CameraControlToggle"))
            p += new Vector3(0, 0, -Input.GetAxis("Vertical"));

        //add zoom to transform
        p *= (1 + speedUpFactor * Input.GetAxis("CameraSpeedUp"));
        p *= zoomSpeed * Time.deltaTime;
        zoomTransform.localPosition -= p;
    }


    private void ZoomCameraMouse()
    {
        mouseZoom += -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * (1 + speedUpFactor) * mouseZoomFactor;
        mouseZoom = Mathf.Min(mouseZoomSpeed, Mathf.Max(-mouseZoomSpeed, mouseZoom));
        if (Mathf.Abs(mouseZoom) < mouseZoomGravityMin * Time.deltaTime)
            mouseZoom = 0f;
        else
            mouseZoom += -Mathf.Sign(mouseZoom) * Mathf.Max(mouseZoom * mouseZoomGravity, mouseZoomGravityMin) * Time.deltaTime;
        zoomTransform.localPosition -= new Vector3(0, 0, mouseZoom);
    }
}
