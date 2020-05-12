using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRestartPosition : MonoBehaviour
{
    GameLoopManager loopManager;

    CinemachineVirtualCamera virtualCamera;
    Vector3 initialPosition;
    Quaternion initialRotation;

    private void Awake()
    {
        loopManager = FindObjectOfType<GameLoopManager>();

        //virtualCamera = GetComponent<CinemachineVirtualCamera>();
        //initialPosition = transform.position;
        //initialRotation = transform.rotation;
    }

    private void OnEnable()
    {
        loopManager.RegisterOnLoseGameCallback(ResetCameraPosition);
    }

    private void OnDisable()
    {
        loopManager.UnregisterOnLoseGameCallback(ResetCameraPosition);
    }

    private void ResetCameraPosition()
    {
        virtualCamera.enabled = false;
        //transform.position = initialPosition;
        //transform.rotation = initialRotation;
        //Camera.main.transform.position = initialPosition;
        //Camera.main.transform.rotation = initialRotation;
        virtualCamera.enabled = true;
    }
}
