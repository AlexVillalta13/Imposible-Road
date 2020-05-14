using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRestartPosition : MonoBehaviour
{
    GameLoopManager loopManager;

    CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        loopManager = FindObjectOfType<GameLoopManager>();

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
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

        virtualCamera.enabled = true;
    }
}
