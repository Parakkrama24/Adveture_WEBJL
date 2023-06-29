using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Unity.VisualScripting;

public class cameraSwtching : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private int prorityBoostValuve = 10;
    [SerializeField] private Canvas _zoomCanvas;

    private InputAction _aimAction;
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _aimAction = _playerInput.actions["Aim"];
        _virtualCamera=GetComponent<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        _zoomCanvas.enabled=false;
    }
    private void OnEnable()
    {
        _aimAction.performed += _ => startAim();
        _aimAction.canceled += _ => cancalAim();
    }

    private void startAim()
    {
        _virtualCamera.Priority += prorityBoostValuve;
        _zoomCanvas.enabled=true;
    }
    private void cancalAim()
    {
        _virtualCamera.Priority -= prorityBoostValuve;
        _zoomCanvas.enabled=false;
    }



    private void OnDisable()
    {
        _aimAction.performed -= _ => startAim();
        _aimAction.canceled -= _ => cancalAim();
    }

}
